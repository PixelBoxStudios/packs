using UnityEngine;
using System;

public class TestGUI : MonoBehaviour 
{
	public static TestGUI Instance;
	public delegate void GUIState();
	public GUIState CurrentState;
	private bool _ingame;

    private Vector2 _scrollViewVector = Vector2.zero;

    private string[] _selStrings;
    private string _saveFileInfoText;

    private int _selectedSaveIndex;
    private int _amountOfElements;

    private bool _showDeletePrompt;
    private bool _showOverwritePrompt;
    private bool _showSavingPrompt;
    private bool _showNewSaveWindow;

    private string _newSaveName = "";

    private string _menuOption;

    private bool _refreshSavesList;

    public SaveFileInfo[] Saves;

    private string _errorMessage;


    private GUIStyle _textStyle = new GUIStyle { fontSize = 26, normal = { textColor = Color.white } };
    private bool _showSaveCompletedText;
    private DateTime _startTime;
    private DateTime _endTime;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        UniSave.OnLoadingFailed += UniSave_OnLoadingFailed;
        UniSave.OnSavingCompleted += UniSave_OnSavingCompleted;
    }


    void Start () 
	{
        if (Application.loadedLevelName == "MainMenu")
        {
            CurrentState = MainMenu;
        }
            
        else
        {
            CurrentState = InGame;
        }
	}
	
	void Update () 
	{
        if (_ingame && CurrentState != MainMenu && CurrentState != ErrorPrompt  && Input.GetKeyDown(KeyCode.Escape))
		{
			CurrentState = MainMenu;
		}
	}
	
	private void InGame()
	{
		_ingame = true;

        var mouseLook = FindObjectsOfType(typeof(MouseLook));

        foreach (MouseLook comp in mouseLook)
        {
            comp.enabled = true;
        }
	}
	
	private void NotInGame()
	{
		_ingame = false;
		CurrentState = MainMenu;
	}
	
	private void MainMenu()
	{
        GUI.BeginGroup(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 50, 320, 425));

		if (!_ingame)
		{
			if (GUI.Button(new Rect(0, 0, 200, 25), "New Game"))
			{
				Application.LoadLevel("TestLevel");
				CurrentState = InGame;
			}
			
            if (GUI.Button(new Rect(0, 35, 200, 25), "Load Game"))
            {
                _menuOption = "Load";
                _refreshSavesList = true;
                CurrentState = LoadSaveMenu;
            }
			
			if (GUI.Button(new Rect(0, 70, 200, 25), "Quit"))
			{
				Application.Quit();
			}
		}

		else
		{
		    var mouseLook = FindObjectsOfType(typeof(MouseLook));

            foreach (MouseLook comp in mouseLook)
            {
                comp.enabled = false;
            }

            if (GUI.Button(new Rect(0, 0, 200, 25), "Back to Game"))
			{
				CurrentState = InGame;
			}

            if (GUI.Button(new Rect(0, 35, 200, 25), "Save Game"))
            {
                _menuOption = "Save";
                _refreshSavesList = true;
                CurrentState = LoadSaveMenu;
            }

            if (GUI.Button(new Rect(0, 70, 200, 25), "Load Game"))
			{
			    _menuOption = "Load";
                _refreshSavesList = true;
				CurrentState = LoadSaveMenu;
			}

            if (GUI.Button(new Rect(0, 105, 200, 25), "Quit to Main Menu"))
			{
				Application.LoadLevel("MainMenu");
				CurrentState = NotInGame;
			}
		}

        GUI.EndGroup();
	}
	
	private void LoadSaveMenu()
	{
        if (_refreshSavesList)
        {
            Saves = UniSave.GetSaves();
            _refreshSavesList = false;
        }

	    int count = Saves.Length;
        int i = 0;

        _selStrings = new string[count];

        if (Saves.Length > 0)
        {
           _saveFileInfoText = Environment.NewLine + Saves[_selectedSaveIndex].Name + Environment.NewLine +
                               Saves[_selectedSaveIndex].LevelName + Environment.NewLine +
                               Saves[_selectedSaveIndex].Date;
        }

        else
        {
            _saveFileInfoText = "";
        }

        GUI.BeginGroup(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 297, 300, 75));
        GUI.Box(new Rect(0, 0, 300, 75), _saveFileInfoText);
        GUI.EndGroup();

        GUI.BeginGroup(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 212, 320, 425));

        GUI.Box(new Rect(0, 0, 300, 370), "");

        _scrollViewVector = GUI.BeginScrollView(new Rect(0, 10, 320, 350), _scrollViewVector, new Rect(0, 0, 300, 360 + ((_amountOfElements - 5) * 70)), false, false);

        _amountOfElements = 0;

        foreach (SaveFileInfo save in Saves)
        {
            _selStrings[i] = Environment.NewLine + save.Name + Environment.NewLine + Environment.NewLine;

            _amountOfElements++;

            i++;
        }
        
        GUILayout.BeginArea(new Rect(10, 0, 280, 350 + ((_amountOfElements - 5) * 70)));
        _selectedSaveIndex = GUILayout.SelectionGrid(_selectedSaveIndex, _selStrings, 1);
        GUILayout.EndArea();
        
        GUI.EndScrollView();

        if (_menuOption == "Load")
        {
            if (GUI.Button(new Rect(0, 400, 75, 25), "Load"))
            {
                if (Saves.Length > 0)
                {
                    UniSave.Load(Saves[_selectedSaveIndex].Name);
                    CurrentState = InGame;
                }
            }

            if (GUI.Button(new Rect(300 - 75, 400, 75, 25), "Delete"))
            {
                if (Saves.Length > 0)
                {
                    if (!_showDeletePrompt)
                    {
                        _showDeletePrompt = true;
                    }
                }
            }
        }

        if (_showDeletePrompt)
        {
            GUILayout.Window(0, new Rect(Screen.width / 2 - (250 + 500), Screen.height / 2 - 100, 500, 200), DeletePrompt, "Are you sure you want to delete \"" + Saves[_selectedSaveIndex].Name + "\"?");
        }

        else if (_menuOption == "Save")
	    {
	        if (GUI.Button(new Rect(0, 400, 75, 25), "New Save"))
            {
                if (!_showNewSaveWindow)
                {
                    _showNewSaveWindow = true;
                }
            }

            if (GUI.Button(new Rect(300 - 75, 400, 75, 25), "Overwrite"))
            {
                if (Saves.Length > 0)
                {
                    if (!_showOverwritePrompt)
                    {
                        _showOverwritePrompt = true;
                    }
                }
            }
	    }

        if (_showNewSaveWindow)
        {
            _showOverwritePrompt = false;
            GUILayout.Window(0, new Rect(Screen.width / 2 - (250 + 500), Screen.height / 2 - 100, 500, 200), NewSaveWindow, "");
        }

        if (_showOverwritePrompt)
        {
            _showNewSaveWindow = false;
            GUILayout.Window(0, new Rect(Screen.width / 2 - (250 + 500), Screen.height / 2 - 100, 500, 200), OverwriteWindow, "Are you sure you want to overwrite \"" + Saves[_selectedSaveIndex].Name + "\"?");
        }

	    GUI.EndGroup();

        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + (212.5f + 20), 75, 25), "Back"))
        {
            CurrentState = MainMenu;
        }
	}

    private void DeletePrompt(int windowID)
    {
        if (GUILayout.Button("Yes"))
        {
            UniSave.Delete(Saves[_selectedSaveIndex].Name);

            if (_selectedSaveIndex > 0)
            {
                _selectedSaveIndex -= 1;
            }

            _refreshSavesList = true;
            _showDeletePrompt = false;
        }

        else if (GUILayout.Button("No"))
        {
            _showDeletePrompt = false;
        }
    }

    private void OverwriteWindow(int windowID)
    {
        if (GUILayout.Button("Yes"))
        {
            UniSave.Save(Saves[_selectedSaveIndex].Name);
            _showOverwritePrompt = false;
        }

        else if (GUILayout.Button("No"))
        {
            _showOverwritePrompt = false;
        }
    }

    private void NewSaveWindow(int windowID)
    {
        _newSaveName = GUILayout.TextField(_newSaveName, 200);

        if (GUILayout.Button("Save"))
        {
            if (!String.IsNullOrEmpty(_newSaveName))
            {
                UniSave.Save(_newSaveName);
                _newSaveName = "";
                _showNewSaveWindow = false;
                //_showSavingPrompt = true;
            }
        }

        if (GUILayout.Button("Cancel"))
        {
            _showNewSaveWindow = false;
        }
    }

    private void ErrorPrompt()
    {
        GUI.Box(new Rect(0, Screen.height / 2 - 100, Screen.width, 50), "Loading failed:" + Environment.NewLine + _errorMessage);

        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 75, 100, 50), "Back"))
        {
            Destroy(Loading.Instance.gameObject);
            Application.LoadLevel("Menu");
            _ingame = false;
            CurrentState = MainMenu;
        }
    }
	
	void OnGUI()
	{
		CurrentState();

        while (UniSave.IsSaving)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 20, 100, 40), "Saving...", _textStyle);
        }

        if (_showSaveCompletedText)
        {
            TimeSpan duration = DateTime.Now - _startTime;

            if (duration.Seconds < 4)
            {
                GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height / 2 - 20, 150, 40), "Game Saved", _textStyle);
                _refreshSavesList = true;
            }

            else
            {
                _showSaveCompletedText = false;
            }
        }
	}

    private void UniSave_OnLoadingFailed(Exception exception)
    {
        _errorMessage = exception.Message;
        CurrentState = ErrorPrompt;
    }

    private void UniSave_OnSavingCompleted()
    {
        _startTime = DateTime.Now;
        _showSaveCompletedText = true;
    }
}
