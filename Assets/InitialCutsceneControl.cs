using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialCutsceneControl : MonoBehaviour
{
    private void Start()
    {
        
        Invoke("nextLayout", 11);
    }

    private void nextLayout()
    {
        SceneManager.LoadSceneAsync(2);
    }
    
}
