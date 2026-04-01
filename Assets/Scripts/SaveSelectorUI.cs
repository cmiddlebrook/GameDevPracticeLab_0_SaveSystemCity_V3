using UnityEngine;
using UnityEngine.UI;

public class SaveSelectorUI : MonoBehaviour
{
    private SaveSlotUI[] _saveSlots;
    private int _selectedSlotIndex = 0;

    private void Awake()
    {
        _saveSlots = GetComponentsInChildren<SaveSlotUI>();
        for (int i = 0; i < _saveSlots.Length; i++)
        {
            Button button = _saveSlots[i].Button;
            int index = i; // Capture the current value of i for the lambda
            button.onClick.AddListener(() => OnClick_SaveSlot(_saveSlots[index], index));

            Debug.Log($"Added listener to button {i}");
        }

    }

    private void OnClick_SaveSlot(SaveSlotUI saveSlot, int slotIndex)
    {
        Debug.Log($"Clicked on slot {slotIndex}");
        _selectedSlotIndex = slotIndex;
        SetSlotOutlineColours();
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
