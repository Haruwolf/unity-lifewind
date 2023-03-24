using System;
using UnityEngine;
using UnityEngine.Events;

public class PlantController : MonoBehaviour
{ 
    private Plant m_Plant;

    [HideInInspector] 
    public UnityEvent onPlantCreated;

    [SerializeField]
    [Tooltip("Adicione aqui os prefabs de semente, muda e árvore respectivamente.")]
    private GameObject seedGameObject, seedPlantedGameObject, sproutGameObject, treeGameObject, plantGameObject;

    [SerializeField]
    [Header("Status da planta")]
    [Tooltip("Altere aqui qual o status atual da planta durante o jogo para semente, muda ou árvore.")]
    private Plant.PlantStates plantStates = Plant.PlantStates.SeedNotPlanted;

    [SerializeField]
    [Range(0, 100)]
    [Tooltip("Nível atual da água da planta, não precisa ser alterado, está no inspector para própositos de assistir a velocidade em que o nível de água sobe durante o jogo.")]
    private int plantWaterLevel = 0;

    [SerializeField]
    [Range(30, 100)]
    [Tooltip("Altere aqui qual o nível máximo de água da planta.")]
    private int waterLevelMax = 50;

    [SerializeField]
    [Range(1, 98)]
    [Tooltip("Nível de água necessário para a semente brotar para muda.")]
    private int sproutWaterLevel = 15;

    [SerializeField]
    [Range(2, 99)]
    [Tooltip("Nível de água necessário para a muda brotar para árvore")]
    private int treeWaterLevel = 30;

    private void OnEnable()
    {
        CreatePlantObject();
        AddTotalPlants();
    }

    private void CreatePlantObject()
    {
        m_Plant = new Plant(
            plantState: this.plantStates,
            seed: seedGameObject,
            seedPlanted: seedPlantedGameObject,
            sprout: sproutGameObject,
            tree: treeGameObject,
            plant: plantGameObject,
            wLevel: plantWaterLevel,
            wLevelMax: waterLevelMax,
            wSproutLevel: sproutWaterLevel,
            wTreeLevel: treeWaterLevel
        );
        
        onPlantCreated?.Invoke();
    }

    public Plant GetPlantObject()
    {
        return m_Plant;
    }

    private void AddTotalPlants()
    {
        GameManager.instance.totalPlants += 1;
    }

    private void Update()
    {
        
        Debug.Log(gameObject.name + " "+ m_Plant.GetPlantState());
        Debug.Log(gameObject.name + " "+ m_Plant.WaterLevel);
    }
}



