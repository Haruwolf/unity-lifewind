using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindWaterBlocks : MonoBehaviour
{
    public List<GameObject> waterBlocks = new List<GameObject>();
    List<GameObject> availableBlocks = new List<GameObject>();

    // Start is called before the first frame update
    //void Start()
    //{
    //    Invoke(nameof(checkCloudTiles), 5);
    //}

    void checkCloudTiles()
    {
        foreach(GameObject wBlocks in waterBlocks)
        {
            if(wBlocks.GetComponent<CloudMaker>().canSpawnCloud == true)
                availableBlocks.Add(wBlocks);
        }

        if (availableBlocks.Count > 0)
            chooseBlock(availableBlocks);

        else
            return;
    }

    void chooseBlock(List<GameObject> availableBlocks)
    {
        int indexChoosed = Random.Range(0, availableBlocks.Count);
        GameObject wBlocks = availableBlocks[indexChoosed];
        wBlocks.GetComponent<CloudMaker>().canSpawnCloud = false;
        //wBlocks.GetComponent<BlockStates>().spawnCloud();
        availableBlocks.Clear();
        Invoke(nameof(checkCloudTiles), 7);
    }


}
