using UnityEngine;

public class ResourceManager : MonoBehaviour
{


    public static ResourceManager Instance { get; private set; }


    private int _powerAmount;
    private int _money;
    private int _population;


    private void Awake()
    {
        Instance = this;
    }

    public int GetPowerAmount()
    {
        return _powerAmount;
    }

    public int GetMoney()
    {
        return _money;
    }

    public int GetPopulation()
    {
        return _population;
    }

    public void AddMoney()
    {
        _money++;
    }

    public void AddPower()
    {
        _powerAmount++;
    }

    public void AddPopulation()
    {
        _population++;
    }

}