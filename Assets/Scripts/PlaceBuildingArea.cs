using UnityEngine;

public class PlaceBuildingArea : MonoBehaviour
{


    [SerializeField] private GameObject baseVisualGameObject;
    [SerializeField] private GameObject baseSelectedGameObject;


    private Transform buildingTransform;


    public bool IsEmpty()
    {
        return buildingTransform == null;
    }

    public void AddBuilding(Transform buildingTransform)
    {
        this.buildingTransform = buildingTransform;
        baseVisualGameObject.SetActive(false);
    }

    public void ShowSelected()
    {
        baseSelectedGameObject.SetActive(true);
    }

    public void HideSelected()
    {
        baseSelectedGameObject.SetActive(false);
    }

}