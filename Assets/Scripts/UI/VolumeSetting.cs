using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Platformer.Mechanics
{
    //Clase encargada de actualizar el volumen del juego
    public class VolumeSetting : MonoBehaviour
    {
        public AudioMixer audioMixer;
        public Slider slider;

        //Función que inicializa el valor del volumen
        void Start()
        {
            slider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        }

        //Función que define y guarda el valor del volumen en el AudioMixer y PlayerPrefs
        public void SetLevel()
        {
            float sliderValue = slider.value;
            audioMixer.SetFloat("volume", Mathf.Log10(sliderValue) * 20);
            PlayerPrefs.SetFloat("MusicVolume", sliderValue);
        }
    }
}