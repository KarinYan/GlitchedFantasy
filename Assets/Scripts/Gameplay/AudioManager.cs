using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Platformer.Core.Simulation;


namespace Platformer.Mechanics
{
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
        
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayStepAudio()
        {  
            GetComponent<AudioSource>().volume  = 0.1f;         
            audioSource.PlayOneShot(step);                       
        }

        public void PlayLandAudio()
        {   
            GetComponent<AudioSource>().volume  = 0.2f;         
            audioSource.PlayOneShot(land, 0.5f);            
        }

        public void PlayJumpAudio()
        {   
            GetComponent<AudioSource>().volume  = 0.2f;          
            audioSource.PlayOneShot(jumping);
        }

        public void PlayHurtAudio()
        {            
            audioSource.PlayOneShot(hurt, 0.7f);
        }

        public void PlayShootAudio()
        {            
            GetComponent<AudioSource>().volume  = 0.08f;
            audioSource.PlayOneShot(shoot);
        }

        public void PlayDeathAudio()
        {            
            GetComponent<AudioSource>().volume  = 0.3f;
            audioSource.PlayOneShot(dead, 0.7f);
        }

        public void PlayHealthCollectAudio()
        {            
            audioSource.PlayOneShot(collectPotions, 0.7f);
        }

        public void PlayCoreCollectAudio()
        {            
            audioSource.PlayOneShot(collectCores, 0.7f);
        }


    }
}
