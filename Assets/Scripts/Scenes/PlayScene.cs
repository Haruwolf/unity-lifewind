using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayScene : MonoBehaviour
{
    public void goNextScene()
    {
        SceneManager.LoadScene(1);
    }
}
