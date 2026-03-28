using TMPro;
using UnityEngine;

public class ResourceManagerUI : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI moneyTextMesh;
    [SerializeField] private TextMeshProUGUI powerTextMesh;
    [SerializeField] private TextMeshProUGUI populationTextMesh;



    private void Update()
    {
        moneyTextMesh.text = ResourceManager.Instance.GetMoney().ToString();
        powerTextMesh.text = ResourceManager.Instance.GetPower().ToString();
        populationTextMesh.text = ResourceManager.Instance.GetPopulation().ToString();
    }

}