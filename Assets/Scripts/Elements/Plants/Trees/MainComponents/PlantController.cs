using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

[AddComponentMenu(nameof(Carry))]
public class PlantController : MonoBehaviour
{ 
    private Plant plant;

    [HideInInspector] 
    public UnityEvent OnPlantCreated;

    [SerializeField]
    [Tooltip("Adicione aqui os prefabs de semente, muda e árvore respectivamente.")]
    private GameObject seedGameObject, seedPlantedGameObject, sproutGameObject, treeGameObject, plantGameObject;

    [SerializeField]
    [Header("Status da planta")]
    [Tooltip("Altere aqui qual o status atual da planta durante o jogo para semente, muda ou árvore.")]
    private Plant.PlantStates plantStates;

    [SerializeField]
    [Range(0, 100)]
    [Tooltip("Nível atual da água da planta, não precisa ser alterado, está no inspector para própositos de assistir a velocidade em que o nível de água sobe durante o jogo.")]
    private int plantWaterLevel;

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

    [Space(10)]
    [SerializeField]
    [Range(0,3)]
    [Tooltip("Altere aqui a margem de posicionamento vertical da planta.")]
    private float offsetPlantPositionY = 0.4f;

    private void OnEnable()
    {
        CreatePlantObject();
        AddTotalPlants();
    }

    public void CreatePlantObject()
    {
        plant = new Plant(
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

        OnPlantCreated?.Invoke();
    }

    public Plant GetPlantObject()
    {
        return plant;
    }

    public void AddTotalPlants()
    {
        GameManager.instance.totalPlants += 1;
    }

}



