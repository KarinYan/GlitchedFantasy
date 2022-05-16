using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Platformer.Core.Simulation;
using UnityEngine.SceneManagement; 

namespace Platformer.Mechanics
{
    //Clase que controla las acciones del menú de pausa
    public class PauseMenu : MonoBehaviour
    {
        public static bool gameIsPaused = false;
        public PlayerManager playerHealth;
        public GameObject pauseMenuUI;

        //Función que se ejecuta en cada frame del juego y que activa o desactiva el menú de pausa si se apreta la tecla definida y se cumple la condición establecida
        void Update ()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && playerHealth.health > 0)
            {
                if (gameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        //Función que cierra el menú de pausa y reactiva el juego
        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
        }

        //Función que pausa el juego y activa el menú de pausa
        public void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused = true;
        }

        //Función que reinicia la escena activa
        public void LoadGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1f;
        }

        //Función que devuelve al jugador al menú principal
        public void ReturnMainMenu()
        {
            SceneManager.LoadScene(0);
        }
    } 
}
