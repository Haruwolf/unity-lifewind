using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool nextLayout = false;
    public static GameManager instance;
    public int totalPlants;
    public int weedsOnScreen;
    public int sproutsOnScreen;
    public int treesOnScreen;
    public float fillBar;
    public Image crystalFilled;
    public int totalTrees;
    public Text treesText;
    public float weedBar;

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
        
        weedBar = Mathf.Clamp(1 + weedsOnScreen * 0.2f , 1, totalTrees);
        fillBar = Mathf.Clamp(treesOnScreen, 0, totalTrees);
        
        //fillBar = Mathf.Clamp((sproutsOnScreen * 0.15f + treesOnScreen * 0.25f)+1, 1, 5);
        //Mathf.Clamp(fillBar, 1, 3);
        //crystalFilled.fillAmount = Mathf.Clamp(fillBar, 1, 3);

        treesText.text = $"{treesOnScreen.ToString()} / {Mathf.Round(totalTrees).ToString()}";
        if (fillBar >= totalTrees && nextLayout == false)
        {
            nextLayout = true;
            gameObject.GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Victory");
            gameObject.GetComponent<AudioSource>().Play();
            Invoke("changeScene",3);
            

        }

        //if(Input.anyKey)
        //{
        //    gameObjects = GameObject.FindGameObjectsWithTag("Plant");
        //    Debug.Log(gameObjects.Length);
        //}


    }

    void changeScene()
    {
        SceneManager.LoadScene(sceneIndex + 1);
    }


    public void checkPlants()
    {
        if (totalPlants <= 0)
            SceneManager.LoadScene("LifeWind_GameOver");
    }
}
