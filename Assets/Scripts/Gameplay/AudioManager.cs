using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Platformer.Core.Simulation;


namespace Platformer.Mechanics
{
    //Clase que administra el sonido del juego
    public class AudioManager : MonoBehaviour
    {
        public AudioSource audioSource;
        public AudioClip step;
        public AudioClip land;
        public AudioClip jumping;
        public AudioClip hurt;
        public AudioClip dead;
        public AudioClip collectPotions;
        public AudioClip collectCores;
        public AudioClip shoot;        
        
        //Función que inicializa el sonido
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        //Función que reproduce el sonido para pasos
        public void PlayStepAudio()
        {          
            audioSource.PlayOneShot(step, 0.1f);                       
        }

        //Función que reproduce el sonido de aterrizaje
        public void PlayLandAudio()
        {         
            audioSource.PlayOneShot(land, 0.2f);            
        }

        //Función que reproduce el sonido de salto
        public void PlayJumpAudio()
        {      
            audioSource.PlayOneShot(jumping, 0.2f);
        }

        //Función que reproduce el sonido de daño
        public void PlayHurtAudio()
        {            
            audioSource.PlayOneShot(hurt, 0.2f);
        }

        //Función que reproduce el sonido de disparo
        public void PlayShootAudio()
        {            
            audioSource.PlayOneShot(shoot,0.08f);
        }

        //Función que reproduce el sonido de muerte
        public void PlayDeathAudio()
        {            
            audioSource.PlayOneShot(dead, 0.3f);
        }

        //Función que reproduce el sonido de colección de vidas
        public void PlayHealthCollectAudio()
        {            
            audioSource.PlayOneShot(collectPotions, 0.7f);
        }

        //Función que reproduce el sonido de colección de núcleos
        public void PlayCoreCollectAudio()
        {            
            audioSource.PlayOneShot(collectCores, 0.7f);
        }
    }
}
