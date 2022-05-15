using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

namespace Platformer.Mechanics
{
    public class GameOverMenu : MonoBehaviour
    {
        public static bool gameIsPaused = false;
        public static bool gameOverOpen = false;

        public GameObject gameOverMenu;
        public GameObject pauseMenu;

        public void ReloadGame()
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
