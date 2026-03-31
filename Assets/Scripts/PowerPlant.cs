using UnityEngine;

public class PowerPlant : MonoBehaviour
{

    public BuildingType BuildingType => BuildingType.PowerPlant;

    private float _timer;


    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            _timer = .5f;

            ResourceManager.Instance.AddPower();
        }
    }

}