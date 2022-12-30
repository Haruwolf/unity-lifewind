using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSoundManager : MonoBehaviour
{
    //O Script tá inscrevendo a função de som no evento do script de nuvem.
    //Quando o evento for chamado no outro script, ele vai passar pra função o audiosource e qual o nome do som que deve tocar.
    //Aí nesse script ele vai pegar qual clipe pra tocar.
    public AudioClip chuvaCaindo;
    public AudioClip criandoNuvem;

    private void OnEnable()
    {
        Cloud.SoundEvent += PlayCloudSound;

    }

    private void OnDisable()
    {
        Cloud.SoundEvent -= PlayCloudSound;

    }

    void PlayCloudSound(AudioSource src, string soundName)
    {
        if (src != null)
        {
            switch (soundName)
            {
                case Cloud.chuvaCaindo:
                    if (src.clip != chuvaCaindo)
                    {
                        src.clip = chuvaCaindo;
                        src.Play();
                        src.loop = true;
                    }
                    break;

                case Cloud.criandoNuvem:
                    if (src.clip != chuvaCaindo)
                    {
                        src.clip = criandoNuvem;
                        src.Play();
                        src.loop = true;
                    }
                    break;

            }
        }
    }
}
