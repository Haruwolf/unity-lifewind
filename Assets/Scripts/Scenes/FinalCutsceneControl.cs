using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalCutsceneControl : MonoBehaviour
{
    private void Start()
    {
        
        Invoke("nextLayout", 8);
    }

    private void nextLayout()
    {
        SceneManager.LoadScene("Menu");
    }
    
}
