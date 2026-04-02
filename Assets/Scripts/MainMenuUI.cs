using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuUI : MonoBehaviour
{

    [SerializeField] private Button _startButton;
    [SerializeField] private TextMeshProUGUI _startButtonText;


    private void Start()
    {
        
        _startButtonText.text = SaveSystem.Instance.NumSavesFound > 0 ? "CONTINUE SAVED GAME" : "START NEW GAME";
        _startButton.onClick.AddListener(() =>
        {
            StartGame();
        });
    }


    private void StartGame()
    {

        SceneManager.LoadScene("GameScene");
    }

}