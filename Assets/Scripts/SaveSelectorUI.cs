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
            _saveSlots[i].SetSlotIndex(i);
            Button button = _saveSlots[i].Button;
            // copy the current value of i to a local variable to avoid the modified closure issue
            int index = i; 
            button.onClick.AddListener(() => OnClick_SaveSlot(_saveSlots[index], index));
        }
    }

    private void Start()
    {
        SetSlotOutlineColours();
    }

    private void OnClick_SaveSlot(SaveSlotUI saveSlot, int slotIndex)
    {
        _selectedSlotIndex = SaveSystem.Instance.SetSaveSlot(slotIndex);         
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
