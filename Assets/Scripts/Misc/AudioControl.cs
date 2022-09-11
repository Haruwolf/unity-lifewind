using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{

    public static AudioControl instance;
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
