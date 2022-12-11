using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSoundManager : MonoBehaviour
{
    public AudioClip ventoCarregando;
    public AudioClip ventoMovimentando;

    private void OnEnable()
    {
        WindManager.SoundEvent += PlayWindSound;

    }

    private void OnDisable()
    {
        WindManager.SoundEvent -= PlayWindSound;

    }

    void PlayWindSound(AudioSource src, string soundName)
    {
        if (src != null)
        {
            switch (soundName)
            {
                case WindManager.ventoCarregando:
                    if (src.clip != ventoCarregando)
                    {
                        src.clip = ventoCarregando;
                        src.Play();
                        src.loop = true;
                    }
                    break;

                case WindManager.ventoSolto:
                        src.clip = ventoMovimentando;
                        src.Play();
                        src.loop = true;
                    break;

                case WindManager.quebrarLoop:
                        src.loop = false;
                    break;



            }
        }
    }
}
