using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int weedsOnScreen;
    public int sproutsOnScreen;
    public int treesOnScreen;
    public float fillBar;
    public Image crystalFilled;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        if (instance != null && instance != this)
            Destroy(this);
    }


    private void Update()
    {
        fillBar = sproutsOnScreen * 0.15f + treesOnScreen * 0.25f - weedsOnScreen * 0.10f;
        crystalFilled.fillAmount = Mathf.Clamp(fillBar, 0, 1);
    }
}
