using UnityEngine;

public class House : MonoBehaviour {

    public BuildingType BuildingType => BuildingType.House;

    private void Start() {
        ResourceManager.Instance.AddPopulation();
        ResourceManager.Instance.AddPopulation();
        ResourceManager.Instance.AddPopulation();
    }

}