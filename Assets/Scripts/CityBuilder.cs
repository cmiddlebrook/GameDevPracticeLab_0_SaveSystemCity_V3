using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CityBuilder : MonoBehaviour
{
    public static CityBuilder Instance { get; private set; }

    [SerializeField] private LayerMask _placeBuildingAreaLayerMask;
    [SerializeField] private Transform _officeBuildingPrefab;
    [SerializeField] private Transform _housePrefab;
    [SerializeField] private Transform _powerPlantPrefab;

    private Action _deselectPlaceBuildingAreaAction;
    private Transform _activeBuildingPrefab;
    private BuildingType _activeBuildingType;
    private List<BuildingData> _placedBuildings = new();

    private Dictionary<Vector3, PlaceBuildingArea> _buildingAreas = new();

    private void Awake()
    {
        Instance = this;

        _activeBuildingPrefab = _housePrefab;
        _activeBuildingType = BuildingType.House;
    }

    private void Start()
    {
        PlaceBuildingArea[] buildingAreas = FindObjectsByType<PlaceBuildingArea>(FindObjectsSortMode.None);
        foreach (PlaceBuildingArea buildingArea in buildingAreas)
        {
            _buildingAreas[buildingArea.transform.position] = buildingArea;
        }

        if (SaveSystem.Instance.LoadSavedGame())
        {
            SaveSystem.Instance.EnableAutoSave(5);
            SaveFileData data = SaveSystem.Instance.SaveFileData;
            LoadBuildingsFromSave(data.PlacedBuildings);
        }
    }


    private void Update()
    {
        _deselectPlaceBuildingAreaAction?.Invoke();

        if (EventSystem.current.IsPointerOverGameObject())
        {
            // Mouse over UI
            return;
        }

        RaycastHit[] raycastHitArray = Physics.RaycastAll(Camera.main.ScreenPointToRay(Mouse.current.position.value), float.MaxValue, _placeBuildingAreaLayerMask);

        foreach (RaycastHit raycastHit in raycastHitArray)
        {
            if (raycastHit.transform.TryGetComponent(out PlaceBuildingArea placeBuildingArea))
            {
                _deselectPlaceBuildingAreaAction += placeBuildingArea.HideSelected;
                placeBuildingArea.ShowSelected();

                if (placeBuildingArea.IsEmpty() && Mouse.current.leftButton.wasPressedThisFrame)
                {
                    Transform placedTransform = Instantiate(_activeBuildingPrefab, placeBuildingArea.transform);
                    placedTransform.localPosition = Vector3.zero;
                    placedTransform.localEulerAngles = new Vector3(0, UnityEngine.Random.Range(0, 4) * 90, 0);

                    BuildingData placedBuilding = placeBuildingArea.AddBuilding(placedTransform, _activeBuildingType);
                    _placedBuildings.Add(placedBuilding);
                }
            }
        }
    }

    public List<BuildingData> GetPlacedBuildings()
    {
        return _placedBuildings;
    }

    public void SetActiveBuildingPrefabHouse()
    {
        _activeBuildingPrefab = _housePrefab;
        _activeBuildingType = BuildingType.House;
    }

    public void SetActiveBuildingPrefabOfficeBuilding()
    {
        _activeBuildingPrefab = _officeBuildingPrefab;
        _activeBuildingType = BuildingType.OfficeBuilding;
    }

    public void SetActiveBuildingPrefabPowerPlant()
    {
        _activeBuildingPrefab = _powerPlantPrefab;
        _activeBuildingType = BuildingType.PowerPlant;
    }

    private void LoadBuildingsFromSave(List<BuildingData> buildings)
    {
        foreach (BuildingData building in buildings)
        {
            Vector3 position = building.Position;
            PlaceBuildingArea placeBuildingArea = _buildingAreas[position];

            SelectBuildingPrefab(building.BuildingType);
            Transform buildingTransform = Instantiate(_activeBuildingPrefab, placeBuildingArea.transform);
            buildingTransform.localPosition = Vector3.zero;
            buildingTransform.localEulerAngles = building.Rotation;

            BuildingData placedBuilding = placeBuildingArea.AddBuilding(buildingTransform, building.BuildingType);
            _placedBuildings.Add(placedBuilding);
        }
    }

    private void SelectBuildingPrefab(BuildingType buildingType)
    {
        switch (buildingType)
        {
            case BuildingType.House:
                SetActiveBuildingPrefabHouse();
                break;
            case BuildingType.OfficeBuilding:
                SetActiveBuildingPrefabOfficeBuilding();
                break;
            case BuildingType.PowerPlant:
                SetActiveBuildingPrefabPowerPlant();
                break;
        }
    }


}