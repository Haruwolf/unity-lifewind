using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlant
{
    GameObject SeedGameObject { get; set; }
    GameObject SproutGameObject { get; set; }
    GameObject TreeGameObject { get; set; }
    float WaterLevel { get; set; }
    int WaterLevelMax { get; set; }
    int SproutWaterLevel { get; set; }
    int TreeWaterLevel { get; set; }

}
