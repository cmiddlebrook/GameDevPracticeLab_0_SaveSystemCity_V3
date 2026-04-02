using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSelectorUI : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private TextMeshProUGUI _startButtonText;

    private SaveSlotUI[] _saveSlots;
    private int _selectedSlotIndex = 0;

    private void Awake()
    {
        _startButton.onClick.AddListener(() =>
        {
            if (!SaveSystem.Instance.LoadSavedGame(_selectedSlotIndex))
            {
                Debug.LogError($"Failed to load save slot {_selectedSlotIndex}");
                Application.Quit();
            }

            SceneManager.LoadScene("GameScene");
        });

        _saveSlots = GetComponentsInChildren<SaveSlotUI>();
        for (int i = 0; i < _saveSlots.Length; i++)
        {
            Button button = _saveSlots[i].Button;
            // copy the current value of i to a local variable to avoid the modified closure issue
            int index = i; 
            button.onClick.AddListener(() => OnClick_SaveSlot(_saveSlots[index], index));
        }
    }

    private void Start()
    {
        SetSlotOutlineColours();
        SetStartButtonText();
    }

    private void OnClick_SaveSlot(SaveSlotUI saveSlot, int slotIndex)
    {
        _selectedSlotIndex = slotIndex;
        SetSlotOutlineColours();
        SetStartButtonText();
    }

    private void SetStartButtonText()
    {
        _startButtonText.text = SaveSystem.Instance.HasSaveFile(_selectedSlotIndex) ? "CONTINUE SAVED GAME" : "START NEW GAME";
    }


    private void SetSlotOutlineColours()
    {
        for (int i = 0; i < _saveSlots.Length; i++)
        {
            _saveSlots[i].Outline.color = Color.white;
        }

        _saveSlots[_selectedSlotIndex].Outline.color = Color.green;
    }

}
