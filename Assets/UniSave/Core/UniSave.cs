/*
 * UniSave.cs v1.0
 * Author: Maikel "MajinMHT" Tjin
 * Email: tjin.maikel@gmail.com
 * 
 * 
 * This script contains the core of UniSave.
 * 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using ProtoBuf.Meta;
using System.Threading;

public static partial class UniSave
{
    public delegate void OperationFailedHandler(Exception exception);
    public delegate void OperationCompletedHandler();

    public static event OperationFailedHandler OnSavingFailed;
    public static event OperationFailedHandler OnLoadingFailed;

    public static event OperationCompletedHandler OnSavingCompleted;
    public static event OperationCompletedHandler OnLoadingCompleted;

    /// <summary>
    /// Enables UniSave's debug logging.
    /// </summary>
    public static bool EnableLogging = false;

    // These can be changed.

        // Folder name and save file extension
        private const string SaveFolder = "Saves";
        private const string SaveFileExtension = ".dat";
        private const string SaveFileInfoExtension = ".info";

        private const string CryptoKey = "euWSPxcFdNX4lsph";
        private const string CryptoIV = "f8LEgmUIAsFtwC1l";

    //////////////////////////////////////////////////////////////

    private static readonly List<GameObjectSerializer> GameObjects;
    private static readonly List<string> SavedUniqueGameObjectName;
    private static readonly RuntimeTypeModel Model;

    private static SaveData _serializationData;
	private static SaveData _deserializedData;

	private static List<string> _savedUniqueGameObjectNameResults;
    private static readonly List<string> DestroyedObjectNames;
	private static List<string> _destroyedObjectsResults;
    private static List<GameObjectSerializer> _gameObjectsResults;

    private static string _filePath;
    private static string _saveFileInfoPath;
    private static SaveFileInfo _fileInfo;

    private static readonly RijndaelManaged Crypto;

    private static Thread _serializationThread;

    private static byte[] _saveFileCache;

    private static string _currentSaveName;
    private static string _memoryFileName;

    public static bool IsSaving { get; private set; }

    static UniSave()
	{
        _serializationData = new SaveData();
		GameObjects = new List<GameObjectSerializer>();
		_deserializedData = new SaveData();
		SavedUniqueGameObjectName = new List<string>();
		_savedUniqueGameObjectNameResults = new List<string>();
		
		DestroyedObjectNames = new List<string>();
		_destroyedObjectsResults = new List<string>();

        Model = TypeModel.Create();
        MetaType type = Model.Add(typeof(System.Object), true);

        int i = 1;

        foreach (Type componentType in SupportedComponents.Select(component => Type.GetType(component.Value)))
        {
            type.AddSubType(i, componentType);
            i++;
        }

        Crypto = new RijndaelManaged
        {
            Key = Encoding.ASCII.GetBytes(CryptoKey),
            IV = Encoding.ASCII.GetBytes(CryptoIV)
        };
	}

    /// <summary>
    /// Returns a save file list from the save folder.
    /// </summary>
    /// <returns></returns>
    public static SaveFileInfo[] GetSaves()
    {
        DirectoryInfo directoryInfo;
        SaveFileInfo[] saves;

        switch (Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.WindowsPlayer:

                directoryInfo = new DirectoryInfo(SaveFolder);

                if (!Directory.Exists(SaveFolder))
                {
                    Directory.CreateDirectory(SaveFolder);
                }

                break;

            case RuntimePlatform.Android:
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.OSXPlayer:

                directoryInfo = new DirectoryInfo(Application.persistentDataPath + Path.DirectorySeparatorChar + SaveFolder);

                if (!Directory.Exists(Application.persistentDataPath + Path.DirectorySeparatorChar + SaveFolder))
                {
                    Directory.CreateDirectory(Application.persistentDataPath + Path.DirectorySeparatorChar + SaveFolder);
                }

                break;

            default:

                directoryInfo = null;
                break;
        }

        if (directoryInfo == null)
        {
            Debug.LogError("Couldn't retrieve a list of save files, because no supported platform is found.");
            saves = new SaveFileInfo[0];
            return saves;
        }

        var files = directoryInfo.GetFiles("*" + SaveFileExtension).Where(f => File.Exists(SaveFolder + Path.DirectorySeparatorChar + f.Name.Replace(SaveFileExtension, "") + SaveFileInfoExtension)).ToArray();
        files = files.OrderByDescending(f => f.LastWriteTimeUtc).ToArray();

        saves = new SaveFileInfo[files.Count()];
        int i = 0;

        foreach (var file in files)
        {
            using (FileStream fileStream = File.OpenRead(SaveFolder + Path.DirectorySeparatorChar + file.Name.Replace(".dat", ".info")))
            {
                var saveFileInfo = new SaveFileInfo(ProtoBuf.Serializer.Deserialize<SaveFileInfo>(fileStream));

                saves[i] = saveFileInfo;
            }

            i++;
        }

        return saves;
    }
	
    /// <summary>
    /// Saves data to a file.
    /// </summary>
    /// <param name="saveName">Name of the save file.</param>
	public static void Save(string saveName)
	{
        if (String.IsNullOrEmpty(saveName))
        {
            Debug.LogError("The name of the save file is invalid.");
            return;
        }

        if (_serializationThread != null && _serializationThread.IsAlive) return;

        IsSaving = true;

        _currentSaveName = saveName;

        switch (Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.WindowsPlayer:

                _filePath = SaveFolder + Path.DirectorySeparatorChar + saveName + SaveFileExtension;
                _saveFileInfoPath = SaveFolder + Path.DirectorySeparatorChar + saveName + SaveFileInfoExtension;

                if (!Directory.Exists(SaveFolder))
                {
                    Directory.CreateDirectory(SaveFolder);
                }

                break;

            case RuntimePlatform.Android:
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.OSXPlayer:

                _filePath = Application.persistentDataPath + Path.DirectorySeparatorChar + SaveFolder + Path.DirectorySeparatorChar + saveName + SaveFileExtension;
                _saveFileInfoPath = Application.persistentDataPath + Path.DirectorySeparatorChar + SaveFolder + Path.DirectorySeparatorChar + saveName + SaveFileInfoExtension;

                if (!Directory.Exists(Application.persistentDataPath + Path.DirectorySeparatorChar + SaveFolder))
                {
                    Directory.CreateDirectory(Application.persistentDataPath + Path.DirectorySeparatorChar + SaveFolder);
                }

                break;
        }

        if (_serializationData == null)
            _serializationData = new SaveData();

	    var states = UnityEngine.Object.FindObjectsOfType(typeof (State)) as State[];

	    if (states != null)
	        foreach (var obj in states)
	        {
	            obj.Save();
	        }

	    _serializationData.LevelName = Application.loadedLevelName;
        _serializationData.UniqueGameObjectNames = SavedUniqueGameObjectName;
        _serializationData.GameObjects = GameObjects;
        _serializationData.DestroyedObjectNames = DestroyedObjectNames;

        _fileInfo = new SaveFileInfo(saveName, Application.loadedLevel, Application.loadedLevelName, DateTime.UtcNow.ToString("M/d/yyyy hh:mm tt"));

        _serializationThread = new Thread(Serialize);
        _serializationThread.Start();
	}

    private static void Serialize()
    {
        ICryptoTransform encryptor = Crypto.CreateEncryptor(Crypto.Key, Crypto.IV);

        DateTime startTime = DateTime.Now;

        try
        {
            using (FileStream fileStream = File.Create(_filePath))
            using (var cryptoStream = new CryptoStream(fileStream, encryptor, CryptoStreamMode.Write))
            {
                Model.Serialize(cryptoStream, _serializationData);
            }
        }

        catch (Exception exception)
        {
            Debug.LogError("Saving \"" + _currentSaveName + "\" failed. " + exception.Message);

            if (OnSavingFailed != null)
            {
                OnSavingFailed.Invoke(exception);
            }

            return;
        }

        using (var memoryStream = new MemoryStream())
        {
            Model.Serialize(memoryStream, _serializationData);
            _saveFileCache = memoryStream.ToArray();
        }

        Log(LogType.Log, "File \"" + _currentSaveName + "\" saved to memory.");

        if (OnSavingCompleted != null)
        {
            OnSavingCompleted.Invoke();
        }

        _memoryFileName = _currentSaveName;

        using (FileStream fileStream = File.Create(_saveFileInfoPath))
        {
            ProtoBuf.Serializer.Serialize(fileStream, _fileInfo);
        }

        IsSaving = false;

        DateTime endTime = DateTime.Now;

        TimeSpan duration = endTime - startTime;

        Log(LogType.Log, "Saved \"" + _currentSaveName + "\" successfully.");
        Log(LogType.Log, "Save time: " + duration);

        _serializationData = null;
        GameObjects.Clear();
        SavedUniqueGameObjectName.Clear();
        DestroyedObjectNames.Clear();
    }

    /// <summary>
    /// Warning: This method should only be called by the Loading scene.
    /// </summary>
    /// <returns></returns>
    public static IEnumerator LoadAfterLoadingScreen()
    {
        if (_filePath == null)
        {
            yield break;
        }
        
        DateTime startTime = DateTime.Now;

        try
        {
            if (_saveFileCache != null && _currentSaveName == _memoryFileName)
            {
                Log(LogType.Log, "File \"" + _currentSaveName + "\" is in memory. Loading from memory.");

                using (var memoryStream = new MemoryStream(_saveFileCache))
                {
                    memoryStream.Position = 0;
                    _deserializedData = (SaveData) Model.Deserialize(memoryStream, _deserializedData, typeof (SaveData));
                }
            }

            else
            {
                Log(LogType.Log, "File \"" + _currentSaveName + "\" is not in memory. Loading from disk.");

                ICryptoTransform decryptor = Crypto.CreateDecryptor(Crypto.Key, Crypto.IV);

                using (FileStream fileStream = File.OpenRead(_filePath))
                using (var cryptoStream = new CryptoStream(fileStream, decryptor, CryptoStreamMode.Read))
                {
                    _deserializedData = (SaveData)Model.Deserialize(cryptoStream, _deserializedData, typeof(SaveData));
                }

                using (var memoryStream = new MemoryStream())
                {
                    Model.Serialize(memoryStream, _deserializedData);
                    _saveFileCache = memoryStream.ToArray();
                }

                _memoryFileName = _currentSaveName;

                Log(LogType.Log, "File \"" + _currentSaveName + "\" saved to memory.");
            }
        }

        catch (Exception exception)
        {
            Debug.LogError("Failed loading \"" + _currentSaveName + "\". " + exception.Message);

            if (OnLoadingFailed != null)
            {
                OnLoadingFailed.Invoke(exception);
            }

            yield break;
        }

        Application.LoadLevel(_deserializedData.LevelName);

        while (Application.isLoadingLevel)
        {
            yield return null;
        }

        DestroyedGameObjects();
        DefaultGameObjects();
        RuntimeGameObjects();

        DateTime endTime = DateTime.Now;
        TimeSpan duration = endTime - startTime;

        Log(LogType.Log, "Loaded \"" + _currentSaveName + "\" successfully.");
        Log(LogType.Log, "Load time: " + duration);

        if (OnLoadingCompleted != null)
        {
            OnLoadingCompleted.Invoke();
        }

        _deserializedData = null;
    }

    /// <summary>
    /// Deletes a specified save file.
    /// </summary>
    /// <param name="saveName">Name of the save file.</param>
    public static void Delete(string saveName)
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.WindowsPlayer:

                _filePath = SaveFolder + Path.DirectorySeparatorChar + saveName + SaveFileExtension;
                _saveFileInfoPath = SaveFolder + Path.DirectorySeparatorChar + saveName + SaveFileInfoExtension;
                break;

            case RuntimePlatform.Android:
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.OSXPlayer:

                _filePath = Application.persistentDataPath + Path.DirectorySeparatorChar + SaveFolder + Path.DirectorySeparatorChar + saveName + SaveFileExtension;
                _saveFileInfoPath = Application.persistentDataPath + Path.DirectorySeparatorChar + SaveFolder + Path.DirectorySeparatorChar + saveName + SaveFileInfoExtension;
                break;
        }

        File.Delete(_filePath);
        File.Delete(_saveFileInfoPath);
    }

    private static void DestroyedGameObjects()
    {
        GameObject.Destroy(Loading.Instance.gameObject);

        DestroyedObjectNames.Clear();

        //Iterate over deleted Default Scene Objects
        _destroyedObjectsResults = _deserializedData.DestroyedObjectNames;

        //Destroy 'Default' GameObjects that were destroyed prior to saving
        var objectsToDestroy = from destroyedObjectName in _destroyedObjectsResults
                               from obj in GameObject.FindObjectsOfType(typeof(State)) as State[]
                               where !obj.IsSpawnedAtRuntime && destroyedObjectName == obj.UniqueName
                               select obj.gameObject;

        foreach (var obj in objectsToDestroy)
        {
            GameObject.Destroy(obj);
        }

        _savedUniqueGameObjectNameResults = _deserializedData.UniqueGameObjectNames;
    }

    private static void DefaultGameObjects()
    {
        var defaultObjects = from obj in GameObject.FindObjectsOfType(typeof(State)) as State[]
                             where !obj.IsSpawnedAtRuntime
                             select obj;

        var defaultObjectNames = defaultObjects.ToDictionary(obj => obj.UniqueName, obj => obj.gameObject);

        //
        _gameObjectsResults = _deserializedData.GameObjects;

        //Edit 'Default' GameObjects that were saved
        var objectToEdit = from goUniqueName in defaultObjectNames
                           where _savedUniqueGameObjectNameResults.Contains(goUniqueName.Key)
                           from gameObject in _gameObjectsResults
                           where gameObject.UniqueName == goUniqueName.Key
                           from object objectComponent in gameObject.Components
                           select new { objectComponent, SerializedData = gameObject, Key = defaultObjectNames[goUniqueName.Key] };

        foreach (var obj in objectToEdit)
        {
            obj.Key.name = obj.SerializedData.Name;
            obj.Key.hideFlags = (HideFlags)obj.SerializedData.HideFlags;
            obj.Key.isStatic = obj.SerializedData.IsStatic;
            obj.Key.layer = obj.SerializedData.Layer;
            obj.Key.active = obj.SerializedData.Active;
            obj.Key.tag = obj.SerializedData.Tag;

            Assembly.GetExecutingAssembly().CreateInstance(SupportedComponents[GetOriginalComponentName(obj.objectComponent.GetType().Name)], false, BindingFlags.CreateInstance, null, new[] { obj.Key, obj.objectComponent }, null, null);
        }
    }

    private static void RuntimeGameObjects()
    {
        //Create 'Runtime' created GameObjects that were saved
        var runtimeObjects = from gameObject in _gameObjectsResults
                   from component in gameObject.Components
                   where component is StateSerializer
                   let state = (StateSerializer)component
                   where state.IsSpawnedAtRuntime
                   select gameObject;

        foreach (var gameObject in runtimeObjects)
        {
            var newGameObject = new GameObject
            {
                name = gameObject.Name,
                hideFlags = (HideFlags)gameObject.HideFlags,
                isStatic = gameObject.IsStatic,
                layer = gameObject.Layer,
                active = gameObject.Active,
                tag = gameObject.Tag
            };

            foreach (var component in gameObject.Components)
            {
                Assembly.GetExecutingAssembly().CreateInstance(SupportedComponents[GetOriginalComponentName(component.GetType().Name)], false, BindingFlags.CreateInstance, null, new[] { newGameObject, component }, null, null);
            }

            CreateChildren(gameObject, newGameObject);
        }
    }

    private static void CreateChildren(GameObjectSerializer gameObject, GameObject newGameObject)
    {
        foreach (var child in gameObject.Children)
        {
            var childGameObject = new GameObject
            {
                name = child.Name,
                hideFlags = (HideFlags)child.HideFlags,
                isStatic = child.IsStatic,
                layer = child.Layer,
                active = child.Active,
                tag = child.Tag
            };

            foreach (var component in child.Components)
            {
                Assembly.GetExecutingAssembly().CreateInstance(SupportedComponents[GetOriginalComponentName(component.GetType().Name)], false, BindingFlags.CreateInstance, null, new[] { childGameObject, component }, null, null);
            }

            childGameObject.transform.parent = newGameObject.transform;
            childGameObject.transform.localPosition = (Vector3) child.LocalPosition;
            childGameObject.transform.localRotation = (Quaternion) child.LocalRotation;
            childGameObject.transform.localScale = (Vector3) child.LocalScale;

            if (child.Children.Count > 0)
                CreateChildren(child, childGameObject);
        }
    }

	/// <summary>
	/// Loads data from a save file.
	/// </summary>
	/// <param name="saveName">The name of the save file to be loaded.</param>
	public static void Load(string saveName)
	{
	    _currentSaveName = saveName;

	    switch (Application.platform)
	    {
	        case RuntimePlatform.WindowsEditor:
	        case RuntimePlatform.WindowsPlayer:

                _filePath = SaveFolder + Path.DirectorySeparatorChar + saveName + SaveFileExtension;
	            break;

	        case RuntimePlatform.Android:
	        case RuntimePlatform.OSXEditor:
	        case RuntimePlatform.OSXPlayer:

                _filePath = Application.persistentDataPath + Path.DirectorySeparatorChar + SaveFolder + Path.DirectorySeparatorChar + saveName + SaveFileExtension;
	            break;
	    }

	    Application.LoadLevel("Loading");
	}

    /// <summary>
    /// Generates a unique name for a GameObject by using it's name, position, and rotation. *SHOULD ONLY BE CALLED BY THE STATE COMPONENT*
    /// </summary>
    /// <param name="gameObject">The GameObject that will receive a unique name.</param>
    /// <returns>The uniquely generated name.</returns>
    public static string GenerateUniqueGameObjectName(GameObject gameObject)
    {
        return gameObject.name + "_" +
               gameObject.transform.position + "_" +
               gameObject.transform.rotation;
    }
	
    /// <summary>
    /// Creates a serializable version of a GameObject and its components. *SHOULD ONLY BE CALLED BY THE STATE COMPONENT*
    /// </summary>
    /// <param name="state">The StateComponent attached to the corresponding GameObject.</param>
	public static void CreateGameObject(State state)
	{
	    var gameObjectSerializer = new GameObjectSerializer
	    {
	        Name = state.gameObject.name,
	        HideFlags = (HideFlagsSerializer) state.gameObject.hideFlags,
	        IsStatic = state.gameObject.isStatic,
	        Layer = state.gameObject.layer,
	        Active = state.gameObject.active,
	        Tag = state.gameObject.tag,
	        UniqueName = state.UniqueName
	    };

	    if (!state.IsSpawnedAtRuntime)
		{	
			//If GameObject is a Default GameObject, save unique name to file
			SavedUniqueGameObjectName.Add(state.UniqueName);
		
			//Create serializable components and add to file
		    var components = from i in state.List
                             let componentName = state.PopupList[i]
                             select Assembly.GetExecutingAssembly().CreateInstance(SupportedComponents[componentName], false, BindingFlags.CreateInstance, null, new System.Object[] { state.gameObject }, null, null);

            foreach (var component in components)
            {
                gameObjectSerializer.Components.Add(component);
            }
		}
		
		//If GameObject is a Spawned GameObject, get every component, create serializable version and add to file
		else if (state.IsSpawnedAtRuntime)
		{
            var components = from component in state.ComponentList
                             let componentName = component.GetType().Name
                             select Assembly.GetExecutingAssembly().CreateInstance(SupportedComponents[componentName], false, BindingFlags.CreateInstance, null, new System.Object[] { state.gameObject }, null, null);

            foreach (var component in components)
            {
                gameObjectSerializer.Components.Add(component);
            }

		    GetChildren(state, gameObjectSerializer);
		}
		
		GameObjects.Add(gameObjectSerializer);
	}

    /// <summary>
    /// Tells UniSave that a GameObject containing a StateComponent is being deleted, and should be added to the list of deleted objects so UniSave remembers it. 
    /// *SHOULD ONLY BE CALLED BY THE STATE COMPONENT*
    /// </summary>
    /// <param name="uniqueName">The name of the GameObject that is being deleted. </param>
    public static void AddToDestroyedObjectsList(string uniqueName)
    {
        DestroyedObjectNames.Add(uniqueName);
    }

    private static void GetChildren(State state, GameObjectSerializer parentGameObject)
    {
        foreach (Transform child in state.transform)
        {
            var childState = child.GetComponent<State>();

            if (childState == null)
            {
                Debug.LogError("State component not found on child.");
                continue;
            }

            var childGameObject = new GameObjectSerializer
            {
                Name = child.gameObject.name,
                HideFlags = (HideFlagsSerializer) child.gameObject.hideFlags,
                IsStatic = child.gameObject.isStatic,
                Layer = child.gameObject.layer,
                Active = child.gameObject.active,
                Tag = child.gameObject.tag,
                UniqueName = childState.UniqueName,
                LocalPosition = (Vector3Serializer) child.localPosition,
                LocalScale = (Vector3Serializer) child.localScale,
                LocalRotation = (QuaternionSerializer) child.localRotation
            };

            var currentChild = child;

            var childComponents = from component in childState.ComponentList
                                  let componentName = component.GetType().Name
                                  select Assembly.GetExecutingAssembly().CreateInstance(SupportedComponents[componentName], false, BindingFlags.CreateInstance, null, new System.Object[] {currentChild.gameObject}, null, null);

            foreach (var component in childComponents)
            {
                childGameObject.Components.Add(component);
            }

            parentGameObject.Children.Add(childGameObject);

            if (child.childCount > 0)
            {
                GetChildren(childState, childGameObject);
            }
        }
    }

    /// <summary>
    /// Loads a resource for a UniSave component. Returns null and a log warning if the component was not found.
    /// </summary>
    /// <param name="resourceName">Name of the resource to locate.</param>
    /// <returns></returns>
    public static object TryLoadResource(string resourceName)
    {
        var filteredResourceName = resourceName.Replace(" (Instance)", "");
        var resource = Resources.Load(filteredResourceName);

        if (resource == null)
        {
            switch (filteredResourceName)
            {
                case "Arial":
                case "Font Material":
                case "Default":

                    break;

                case "Default-Diffuse":

                    return GetBuiltInResource(typeof (Material), "Default-Diffuse");

                case "Default-Particle":

                    return GetBuiltInResource(typeof(Material), "Default-Particle");

                default:

                    Debug.LogWarning("Asset \"" + filteredResourceName + "\" could not be found. Are you sure that the Asset has been added to the Resources folder?");
                    return null;
            }
        }

        return resource;
    }

    private static object GetBuiltInResource(Type type, string name)
    {
        return Resources.FindObjectsOfTypeAll(type).FirstOrDefault(material => material.name == name);
    }

    private static void Log(LogType type, object message)
    {
        if (!EnableLogging) return;

        switch (type)
        {
            case LogType.Log:
                Debug.Log(message);
                break;
                
            case LogType.Warning:
                Debug.LogWarning(message);
                break;

            case LogType.Error:
                Debug.LogError(message);
                break;
        }
    }
}
