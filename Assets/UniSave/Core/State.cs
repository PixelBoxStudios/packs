using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
[AddComponentMenu("UniSave/State")]
public class State : MonoBehaviour
{
    // List of supported components
    public List<Component> ComponentList;

    // Components in popuplist
    public string[] PopupList;

    // Selected index in the component list
    public int SelectionIndex;

    // Indices of components selected in the inspector
	public List<int> List = new List<int>();

    [SerializeField] 
    private bool _isSpawnedAtRuntime;

    // Components retrieved by GetComponents
    private Component[] _retrieveComponents;

    private bool _isQuitting;

    public bool IsSpawnedAtRuntime { get { return _isSpawnedAtRuntime; } set { _isSpawnedAtRuntime = value; } }
    public string UniqueName { get; set; }

    public void Awake()
	{	
		if (!Application.isPlaying)
		{
			CheckComponents();
		}
		else
		{
			if (!IsSpawnedAtRuntime)
			{
				UniqueName = UniSave.GenerateUniqueGameObjectName(gameObject);
			}
		}
	}
	
	public void CheckComponents()
	{
		int i = 0;

		if (List.Count < 1)
		    List.Add(SelectionIndex); // Default selection of index value 0
		
		ComponentList = new List<Component>();
		_retrieveComponents = GetComponents(typeof(Component));

        foreach (Component component in _retrieveComponents)
		{
			if (UniSave.IsComponentSupported(component) && component.GetType() != typeof(State))
			{
				ComponentList.Add(component);
			}
			else
			{
				//Debug.LogWarning(component.GetType().Name + " is not supported.");
			}
		}
		
		PopupList = new string[ComponentList.Count];
		
		foreach (Component component in ComponentList)
		{		
			PopupList[i] = component.GetType().Name;
			i++;
		}
	}
	
	public void OnApplicationQuit()
	{
		_isQuitting = true;
	}
	
	public void OnDestroy()
	{
		if (Application.isPlaying && !Application.isLoadingLevel && !_isQuitting && !IsSpawnedAtRuntime)
		{
            UniSave.AddToDestroyedObjectsList(UniqueName);
		    //Debug.Log("Deleted: " + uniqueName);
		}
	}
	
	public void Save()
	{
        if (IsSpawnedAtRuntime)
        {
            ComponentList = new List<Component>();
            _retrieveComponents = GetComponents(typeof (Component));

            foreach (Component component in _retrieveComponents)
            {
                if (UniSave.IsComponentSupported(component))
                {
                    ComponentList.Add(component);
                }
                else
                {
                    //Debug.LogWarning(component.GetType().Name + " is not supported.");
                }
            }
        }

        if (IsSpawnedAtRuntime && transform.parent != null)
            return;

        UniSave.CreateGameObject(this);
	}
}