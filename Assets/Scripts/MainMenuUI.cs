using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {



    [SerializeField] private Button continueButton;



    private void Start() {
        continueButton.onClick.AddListener(() => {
            Continue();
        });
    }

    private void Continue() {
        // Load latest save
    }

}