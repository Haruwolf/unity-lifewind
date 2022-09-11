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

    public float weedBar;

    public GameObject crystal;

    public bool tut1 = false;
    public bool tut2 = false;
    public bool tut3 = false;
    public bool tut4 = false;
    public bool tut5 = false;
    public bool tut6 = false;

    GameObject[] gameObjects;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        if (instance != null && instance != this)
            Destroy(this);

       


    }


    private void Update()
    {
        weedBar = Mathf.Clamp(1 + weedsOnScreen * 0.2f , 1, 3);
        fillBar = Mathf.Clamp((sproutsOnScreen * 0.15f + treesOnScreen * 0.25f)+1, 1, 5);
        //Mathf.Clamp(fillBar, 1, 3);
        //crystalFilled.fillAmount = Mathf.Clamp(fillBar, 1, 3);

        crystal.transform.localScale = new Vector3(fillBar, fillBar, fillBar);
        if (fillBar >= 5)
            SceneManager.LoadScene(3);

        if(Input.anyKey)
        {
            gameObjects = GameObject.FindGameObjectsWithTag("Plant");
            Debug.Log(gameObjects.Length);
        }
    }

    public void checkPlants()
    {
        if (totalPlants <= 0)
            SceneManager.LoadScene(4);
    }
}
