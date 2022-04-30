using System;
using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;
using UnityEngine.SceneManagement; 

namespace Platformer.Mechanics
{
    public class PauseMenu : MonoBehaviour
    {

        public static bool gameIsPaused = false;
        public PlayerManager playerHealth;

        public GameObject pauseMenuUI;

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


        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
        }

        public void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused = true;
        }

        public void LoadGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1f;
        }

        public void ReturnMainMenu()
        {
            SceneManager.LoadScene(0);
        }
    } 
}
