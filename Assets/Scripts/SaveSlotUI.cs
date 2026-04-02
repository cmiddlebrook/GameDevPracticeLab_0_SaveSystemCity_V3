using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveSlotUI : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _outline;
    [SerializeField] private Image _screenshot;
    [SerializeField] private TextMeshProUGUI _slotText;
    [SerializeField] private TextMeshProUGUI _dateText;

    private int _slotIndex;
    private Image _noSaveImage;

    public Button Button => _button;
    public Image Outline => _outline;
    public Image Screenshot => _screenshot;

    private void Start()
    {
        _noSaveImage = _screenshot;

        Sprite screenshotImage = SaveSystem.Instance.GetScreenshot(_slotIndex);
        _screenshot.sprite = (screenshotImage == null) ? _noSaveImage.sprite : screenshotImage;
        _dateText.text = SaveSystem.Instance.GetDateSaved(_slotIndex);
    }

    public void SetSlotIndex(int slotIndex)
    {
        _slotIndex = slotIndex;
    }



}
