using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSoundManager : MonoBehaviour
{
    public AudioClip ventoCarregando;
    public AudioClip ventoMovimentando;

    private void OnEnable()
    {
        Wind.SoundEvent += PlayWindSound;

    }

    private void OnDisable()
    {
        Wind.SoundEvent -= PlayWindSound;

    }

    void PlayWindSound(AudioSource src, string soundName)
    {
        if (src != null)
        {
            switch (soundName)
            {
                case Wind.ventoCarregando:
                    if (src.clip != ventoCarregando)
                    {
                        src.clip = ventoCarregando;
                        src.Play();
                        src.loop = true;
                    }
                    break;

                case Wind.ventoSolto:
                        src.clip = ventoMovimentando;
                        src.Play();
                        src.loop = true;
                    break;

                case Wind.quebrarLoop:
                        src.loop = false;
                    break;



            }
        }
    }
}
