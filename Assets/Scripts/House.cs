using UnityEngine;

public class House : MonoBehaviour {



    private void Start() {
        ResourceManager.Instance.AddPopulation();
        ResourceManager.Instance.AddPopulation();
        ResourceManager.Instance.AddPopulation();
    }

}