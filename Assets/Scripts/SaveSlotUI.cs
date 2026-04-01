using UnityEngine;
using UnityEngine.UI;

public class SaveSlotUI : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _outline;
    [SerializeField] private Image _screenshot;

    public Button Button => _button;
    public Image Outline => _outline;
    public Image Screenshot => _screenshot;

}
