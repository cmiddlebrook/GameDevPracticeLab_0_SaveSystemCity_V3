using UnityEngine;
using UnityEngine.UI;

public class SelectBuildingUI : MonoBehaviour
{


    [SerializeField] private Button houseButton;
    [SerializeField] private Button officeBuildingButton;
    [SerializeField] private Button powerPlantButton;
    [SerializeField] private SelectBuildingUI_Button houseUIButton;
    [SerializeField] private SelectBuildingUI_Button officeBuildingUIButton;
    [SerializeField] private SelectBuildingUI_Button powerPlantUIButton;



    private void Awake()
    {
        houseButton.onClick.AddListener(() =>
        {
            houseUIButton.ShowSelected();
            officeBuildingUIButton.HideSelected();
            powerPlantUIButton.HideSelected();
            CityBuilder.Instance.SetActiveBuildingPrefabHouse();
        });
        officeBuildingButton.onClick.AddListener(() =>
        {
            houseUIButton.HideSelected();
            officeBuildingUIButton.ShowSelected();
            powerPlantUIButton.HideSelected();
            CityBuilder.Instance.SetActiveBuildingPrefabOfficeBuilding();
        });
        powerPlantButton.onClick.AddListener(() =>
        {
            houseUIButton.HideSelected();
            officeBuildingUIButton.HideSelected();
            powerPlantUIButton.ShowSelected();
            CityBuilder.Instance.SetActiveBuildingPrefabPowerPlant();
        });
    }

    private void Start()
    {
        houseUIButton.ShowSelected();
        officeBuildingUIButton.HideSelected();
        powerPlantUIButton.HideSelected();

        CityBuilder.Instance.SetActiveBuildingPrefabHouse();
    }


}