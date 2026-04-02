using System.IO;
using Unity.Mathematics;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private int _saveInterval;

    private string _saveFileName = "citybuildersave";
    private SaveFileData _saveFileData = new();
    private float _saveTimer = 0f;
    private int _manualSaveSlots = 3;
    private int _currentSlotIndex = 0;


    public static SaveSystem Instance;
    public SaveFileData SaveFileData => _saveFileData;
    public int NumSavesFound => 0;
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

        DisableAutoSave();
        SetSaveSlot(0);
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
        if (!HasSaveFile())
        {
            Debug.Log("No save file found");
            return true; // No save file found, but not an error, so the return value is still true 
        }

        return ParseSaveFile();
    }


    public string GetDateSaved(int slotIndex)
    {
        return HasSaveFile() ? File.GetLastWriteTime(GetSaveFilePath(slotIndex)).ToString("g") : "---";
    }


    public Sprite GetScreenshot(int slotIndex)
    {
        string screenshotPath = GetScreenshotPath(slotIndex);
        Texture2D texture = new Texture2D(1, 1);
        Sprite sprite = null;

        if (File.Exists(screenshotPath))
        {
            byte[] fileData = File.ReadAllBytes(screenshotPath);
            texture.LoadImage(fileData);
            sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }

        return sprite;
    }

    private string GetSaveFilePath(int slotIndex = -1)
    {
        int i = (slotIndex == -1) ? _currentSlotIndex : slotIndex;
        return Path.Combine(Application.persistentDataPath, $"{_saveFileName}{_currentSlotIndex}.json");
    }

    private string GetScreenshotPath(int slotIndex = -1)
    {
        int i = (slotIndex == -1) ? _currentSlotIndex : slotIndex;
        return Path.Combine(Application.persistentDataPath, $"{_saveFileName}{i}.png");
    }

    private bool HasSaveFile()
    {
        return File.Exists(GetSaveFilePath());
    }

    private bool ParseSaveFile()
    {
        try
        {
            string fileText = File.ReadAllText(GetSaveFilePath());
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
        File.WriteAllText(GetSaveFilePath(), fileText);
        ScreenCapture.CaptureScreenshot(GetScreenshotPath());

        Debug.Log($"Game saved in slot {_currentSlotIndex}");
        return true;
    }

    public int SetSaveSlot(int slotIndex)
    {
        _currentSlotIndex = math.clamp(slotIndex, 0, _manualSaveSlots); ;
        Debug.Log($"Save slot set to: {_currentSlotIndex}");
        return _currentSlotIndex;
    }



}

