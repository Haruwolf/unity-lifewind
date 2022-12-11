using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    Block block;

    [Rename("Bloco plantável")]
    public bool plantable;

    [Rename("Tipo de bloco")]
    public Block.blockTypes blockType;

    [Range(0, 300)]
    public int nivelMaximoAgua;

    // Start is called before the first frame update
    void Start()
    {
        block = new Block(plantable: true, blockType: Block.blockTypes.Grass, wLevel: nivelMaximoAgua, wLevelMax: nivelMaximoAgua);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
