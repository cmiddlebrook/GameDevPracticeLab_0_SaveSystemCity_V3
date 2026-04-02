using System.IO;
using Unity.Mathematics;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private int _autosaveInterval;
    private string _saveFileName = "citybuildersave";
    private SaveFileData _saveFileData = new();
    private float _saveTimer = 0f;
    private int _manualSaveSlots = 3;
    private int _currentSaveSlotIndex = 0;


    public static SaveSystem Instance;
    public SaveFileData SaveFileData => _saveFileData;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    private void Update()
    {
        if (_autosaveInterval > 0)
        {
            _saveTimer -= Time.deltaTime;
            if (_saveTimer <= 0f)
            {
                _saveTimer = _autosaveInterval;
                SaveGame(_currentSaveSlotIndex);
            }
        }

    }


    public bool LoadSavedGame(int slotIndex)
    {
        _currentSaveSlotIndex = GetSafeSlotIndex(slotIndex);
        if (!HasSaveFile(_currentSaveSlotIndex))
        {
            Debug.Log($"No save file found in slot {slotIndex}");
            return true; // No save file found, but not an error, so the return value is still true 
        }


        return ParseSaveFile(_currentSaveSlotIndex);
    }


    public string GetDateSaved(int slotIndex)
    {
        return HasSaveFile(slotIndex) ? File.GetLastWriteTime(GetSaveFilePath(slotIndex)).ToString("g") : "---";
    }

    public bool HasSaveFile(int slotIndex)
    {
        return File.Exists(GetSaveFilePath(slotIndex));
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

    public void StartAutosaves(int interval)
    {
        if (interval <= 0)
        {
            Debug.LogWarning($"Invalid autosave interval: {interval}. Must be greater than 0");
            return;
        }

        Debug.Log($"Starting autosaves with an interval of {interval} seconds");
        _autosaveInterval = interval;
        _saveTimer = interval;
        SaveGame(_currentSaveSlotIndex);
    }

    public void StopAutosaves()
    {
        _autosaveInterval = 0;
    }


    private string GetSaveFilePath(int slotIndex)
    {
        int i = GetSafeSlotIndex(slotIndex);
        return Path.Combine(Application.persistentDataPath, $"{_saveFileName}{i}.json");
    }

    private string GetScreenshotPath(int slotIndex)
    {
        int i = GetSafeSlotIndex(slotIndex);
        return Path.Combine(Application.persistentDataPath, $"{_saveFileName}{i}.png");
    }


    private bool ParseSaveFile(int slotIndex)
    {
        try
        {
            string fileText = File.ReadAllText(GetSaveFilePath(slotIndex));
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


    private bool SaveGame(int slotIndex)
    {
        _saveFileData.Money = ResourceManager.Instance.GetMoney();
        _saveFileData.Power = ResourceManager.Instance.GetPower();
        _saveFileData.Population = ResourceManager.Instance.GetPopulation();
        _saveFileData.PlacedBuildings = CityBuilder.Instance.GetPlacedBuildings();

        string fileText = JsonUtility.ToJson(_saveFileData, true);
        File.WriteAllText(GetSaveFilePath(slotIndex), fileText);
        ScreenCapture.CaptureScreenshot(GetScreenshotPath(slotIndex));

        Debug.Log($"Game saved in slot {slotIndex}");
        return true;
    }

    private int GetSafeSlotIndex(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex > _manualSaveSlots)
        {
            Debug.LogWarning($"Invalid save slot index: {slotIndex}. Valid range is 0 to {_manualSaveSlots}.");
            Debug.LogWarning($"Defaulting to slot index 0 - AutoSave slot");
        }
        
        return Mathf.Clamp(slotIndex, 0, _manualSaveSlots);
    }




}

