using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private int _saveInterval;

    private string _saveFileName = "citybuildersave.json";
    private string _filePath;
    private SaveFileData _saveFileData = new();
    private float _saveTimer = 0f;


    public static SaveSystem Instance;
    public SaveFileData SaveFileData => _saveFileData;
    public bool SavedGameFound => File.Exists(_filePath);
    public bool AutoSaveEnabled;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        _filePath = Path.Combine(Application.persistentDataPath, _saveFileName);
        Debug.Log($"Save file path: {_filePath}");
        DisableAutoSave();

    }


    private void Update()
    {
        if (AutoSaveEnabled)
        {
            _saveTimer -= Time.deltaTime;
            if (_saveTimer <= 0f)
            {
                _saveTimer = _saveInterval;
                SaveGame();
            }
        }
    }


    public void DisableAutoSave()
    {
        AutoSaveEnabled = false;
        Debug.Log("Auto save disabled");
    }

    public void EnableAutoSave(int saveInterval)
    {
        _saveInterval = saveInterval;
        AutoSaveEnabled = true;
        Debug.Log($"Auto save enabled, every {saveInterval} seconds");
    }

    public bool LoadSavedGame()
    {
        if (!SavedGameFound) 
        {
            Debug.Log("No save file found");
            return true; // No save file found, but not an error, so the return value is still true 
        }

        return ParseSaveFile();
    }

    private bool ParseSaveFile()
    {
        try
        {
            string fileText = File.ReadAllText(_filePath);
            _saveFileData = JsonUtility.FromJson<SaveFileData>(fileText);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Error parsing save file: {e.Message}");
            return false;
        }

        if (_saveFileData == null) return false;

        return true;
    }


    private bool SaveGame()
    {
        _saveFileData.Money = ResourceManager.Instance.GetMoney();
        _saveFileData.Power = ResourceManager.Instance.GetPower();
        _saveFileData.Population = ResourceManager.Instance.GetPopulation();
        _saveFileData.PlacedBuildings = CityBuilder.Instance.GetPlacedBuildings();

        string fileText = JsonUtility.ToJson(_saveFileData, true);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, _saveFileName), fileText);
        Debug.Log("Game saved to file");

        return true;
    }



}

