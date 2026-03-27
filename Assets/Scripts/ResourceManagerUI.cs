using TMPro;
using UnityEngine;

public class ResourceManagerUI : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI populationTextMesh;
    [SerializeField] private TextMeshProUGUI moneyTextMesh;
    [SerializeField] private TextMeshProUGUI powerAmountTextMesh;



    private void Update()
    {
        populationTextMesh.text = ResourceManager.Instance.GetPopulation().ToString();
        moneyTextMesh.text = ResourceManager.Instance.GetMoney().ToString();
        powerAmountTextMesh.text = ResourceManager.Instance.GetPowerAmount().ToString();
    }

}