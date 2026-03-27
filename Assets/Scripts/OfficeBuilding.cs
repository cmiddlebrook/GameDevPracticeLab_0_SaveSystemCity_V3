using UnityEngine;

public class OfficeBuilding : MonoBehaviour
{


    private float _timer;


    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            _timer = .2f;

            ResourceManager.Instance.AddMoney();
        }
    }

}