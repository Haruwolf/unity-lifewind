using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int totalPlants;
    public int weedsOnScreen;
    public int sproutsOnScreen;
    public int treesOnScreen;
    public float fillBar;
    public Image crystalFilled;
    public int totalBlocks;
    public Text treesText;
    public float weedBar;

    public GameObject crystal;

    public bool tut1 = false;
    public bool tut2 = false;
    public bool tut3 = false;
    public bool tut4 = false;
    public bool tut5 = false;
    public bool tut6 = false;

    GameObject[] gameObjects;

    int sceneIndex;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        if (instance != null && instance != this)
            Destroy(this);


        fillBar = 0;

        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(sceneIndex);

        //Fase1
        if (sceneIndex != 2)
        {
            tut1 = true;
            tut2 = true;
            tut3 = true;
            tut4 = true;
            tut5 = true;
            tut6 = true;

        }

    }


    private void Update()
    {
        
        weedBar = Mathf.Clamp(1 + weedsOnScreen * 0.2f , 1, totalBlocks);
        fillBar = Mathf.Clamp(treesOnScreen, 1, totalBlocks);
        
        //fillBar = Mathf.Clamp((sproutsOnScreen * 0.15f + treesOnScreen * 0.25f)+1, 1, 5);
        //Mathf.Clamp(fillBar, 1, 3);
        //crystalFilled.fillAmount = Mathf.Clamp(fillBar, 1, 3);

        crystal.transform.localScale = new Vector3(fillBar, fillBar, fillBar);
        treesText.text = $"{treesOnScreen.ToString()} / {Mathf.Round(totalBlocks / 2).ToString()}";
        if (fillBar >= Mathf.Round(totalBlocks / 2))
        {
            SceneManager.LoadScene(sceneIndex + 1);

        }

        //if(Input.anyKey)
        //{
        //    gameObjects = GameObject.FindGameObjectsWithTag("Plant");
        //    Debug.Log(gameObjects.Length);
        //}


    }

    //void ChangeScene()
    //{
        
    //}


    public void checkPlants()
    {
        if (totalPlants <= 0)
            SceneManager.LoadScene("LifeWind_GameOver");
    }
}
