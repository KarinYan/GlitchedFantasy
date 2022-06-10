using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Platformer.Core.Simulation;
using UnityEngine.SceneManagement;  
using UnityEngine.UI;

namespace Platformer.Mechanics
{
    //Clase de administra la zona de los signos del juego
    public class SignZone : MonoBehaviour
    {
        public GameObject infoMenuUI;
        public static bool gameIsPaused = false;
        private bool triggerEntered = false;        
        private bool interactButtonPressed = false; 
        
        //Función que se ejecuta en cada frame del juego y que activa o desactiva el menú de información, pausando o reanudando el juego según corresponda
        //si se cumplen las condiciones definidas
        void Update()
        {
            if ((Input.GetKeyDown (KeyCode.E) || interactButtonPressed == true) && triggerEntered == true) 
            {
                interactButtonPressed = false; 

                if (gameIsPaused == false)
                {
                    infoMenuUI.SetActive(true);
                    Time.timeScale = 0f;
                    gameIsPaused = true;
                }
                else {
                    infoMenuUI.SetActive(false);
                    Time.timeScale = 1f;
                    gameIsPaused = false;
                    triggerEntered = false; 
                }
            }
        }        

        //Función que desactiva el menú de información reanudando el juego
        public void ExitSignZone()
        {
            infoMenuUI.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
            triggerEntered = false; 
        }

        //Función que activa un valor para condición del Update
        public void OnClick()
        {
            if (triggerEntered == true)
            {
                interactButtonPressed = true;  
            }            
        }

        //Función que activa el cambio de estado al entrar el jugador en la zona
        private void OnTriggerEnter2D(Collider2D collision)
        {
            PlayerManager player = collision.GetComponent<PlayerManager>();
            triggerEntered = true;            
        }

        //Función que desactiva el cambio de estado al salir el jugador de la zona
        private void OnTriggerExit2D(Collider2D collision)
        {
            PlayerManager player = collision.GetComponent<PlayerManager>();
            triggerEntered = false;                      
        }
    }
}