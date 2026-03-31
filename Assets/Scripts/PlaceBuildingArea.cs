using UnityEngine;

public class PlaceBuildingArea : MonoBehaviour
{


    [SerializeField] private GameObject _baseVisualGameObject;
    [SerializeField] private GameObject _baseSelectedGameObject;


    //private Transform _buildingTransform;
    private BuildingData _buildingData;


    public bool IsEmpty()
    {
        return _buildingData == null;
    }

    public BuildingData AddBuilding(Transform buildingTransform, BuildingType type)
    {
        _baseVisualGameObject.SetActive(false);

        _buildingData = new BuildingData
        {
            BuildingType = type,
            Position = buildingTransform.position,
            Rotation = buildingTransform.localEulerAngles,
        };

        return _buildingData;
    }

    public void ShowSelected()
    {
        _baseSelectedGameObject.SetActive(true);
    }

    public void HideSelected()
    {
        _baseSelectedGameObject.SetActive(false);
    }

}
