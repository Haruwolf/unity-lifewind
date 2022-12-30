using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSingleton : MonoBehaviour
{

    public static AudioSingleton instance;
    public AudioSource audioSource;

    public void Awake()
    {
        if (instance == null)
            instance = this;

        if (instance != null && instance != this)
            Destroy(instance);

        audioSource = GetComponent<AudioSource>();
    }

}
