using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CityBuilder : MonoBehaviour
{


    public static CityBuilder Instance { get; private set; }


    [SerializeField] private LayerMask placeBuildingAreaLayerMask;
    [SerializeField] private Transform officeBuildingPrefab;
    [SerializeField] private Transform housePrefab;
    [SerializeField] private Transform powerPlantPrefab;



    private Action deselectPlaceBuildingAreaAction;
    private Transform activeBuildingPrefab;


    private void Awake()
    {
        Instance = this;

        activeBuildingPrefab = housePrefab;
    }


    private void Update()
    {
        deselectPlaceBuildingAreaAction?.Invoke();

        if (EventSystem.current.IsPointerOverGameObject())
        {
            // Mouse over UI
            return;
        }

        RaycastHit[] raycastHitArray = Physics.RaycastAll(Camera.main.ScreenPointToRay(Mouse.current.position.value), float.MaxValue, placeBuildingAreaLayerMask);

        foreach (RaycastHit raycastHit in raycastHitArray)
        {
            if (raycastHit.transform.TryGetComponent(out PlaceBuildingArea placeBuildingArea))
            {
                deselectPlaceBuildingAreaAction += placeBuildingArea.HideSelected;
                placeBuildingArea.ShowSelected();

                if (placeBuildingArea.IsEmpty() && Mouse.current.leftButton.wasPressedThisFrame)
                {
                    Transform placedTransform = Instantiate(activeBuildingPrefab, placeBuildingArea.transform);
                    placedTransform.localPosition = Vector3.zero;
                    placedTransform.localEulerAngles = new Vector3(0, UnityEngine.Random.Range(0, 4) * 90, 0);
                    placeBuildingArea.AddBuilding(placedTransform);
                }
            }
        }
    }

    public void SetActiveBuildingPrefabHouse()
    {
        activeBuildingPrefab = housePrefab;
    }

    public void SetActiveBuildingPrefabOfficeBuilding()
    {
        activeBuildingPrefab = officeBuildingPrefab;
    }

    public void SetActiveBuildingPrefabPowerPlant()
    {
        activeBuildingPrefab = powerPlantPrefab;
    }

}