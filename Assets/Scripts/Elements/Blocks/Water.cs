using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [NonSerialized]
    public Block block;

    [Rename("Bloco plantável")]
    public bool plantable;

    [Rename("Tipo de bloco")]
    public Block.blockTypes blockType;

    [Range(0, 300)]
    public int waterMaxLevel;

    [Range(0, 5)]
    public int waterDropRate;

    [Range(0, 5)]
    public int waterRechargeRate;

    [Range(0, 5)]
    public int waterRechargeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        block = new Block(plantable: false, blockType: Block.blockTypes.Water, wLevel: waterMaxLevel, wLevelMax: waterMaxLevel);
    }

    public void createCloudUsingWater(Cloud cloud, GameObject cloudBlock)
    {
        block.WaterLevel -= waterDropRate;
        cloud.FillCloud(cloudBlock);

    }

    public void rechargeRiver()
    {
        if (block.WaterLevel < waterMaxLevel)
        {
            block.WaterLevel += waterRechargeRate;
            Invoke(nameof(rechargeRiver), waterRechargeSpeed);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
