
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveFileData 
{
    public int Money;
    public int Power;
    public List<BuildingData> PlacedBuildings = new();
}
