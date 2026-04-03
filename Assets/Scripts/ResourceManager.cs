using UnityEngine;

public class ResourceManager : MonoBehaviour
{


    public static ResourceManager Instance { get; private set; }


    private int _power;
    private int _money;
    private int _population;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SaveFileData data = SaveSystem.Instance.SaveFileData;

        _money = data.Money;
        _power = data.Power;

        Debug.Log("Resources loaded from save file");
    }

    public void AddMoney()
    {
        _money++;
    }

    public void AddPower()
    {
        _power++;
    }

    public void AddPopulation()
    {
        _population++;
    }

    public int GetPower()
    {
        return _power;
    }

    public int GetMoney()
    {
        return _money;
    }

    public int GetPopulation()
    {
        return _population;
    }

    public void SetResources(int money, int power)
    {
        _money = money;
        _power = power;
    }

}