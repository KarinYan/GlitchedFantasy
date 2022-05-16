using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Platformer.Mechanics
{
    //Clase encargada de actualizar el volumen del juego
    public class VolumeSetting : MonoBehaviour
    {
        public AudioMixer audioMixer;

        //Función que actualiza el volumen del juego con el valor indicado
        public void SetVolume(float volume)
        {
            audioMixer.SetFloat("volume", volume);
        }
    }
}