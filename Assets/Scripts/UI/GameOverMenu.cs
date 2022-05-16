using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

namespace Platformer.Mechanics
{
    //Clase que controla las acciones del menú de Game Over
    public class GameOverMenu : MonoBehaviour
    {
        public static bool gameIsPaused = false;
        public static bool gameOverOpen = false;

        public GameObject gameOverMenu;
        public GameObject pauseMenu;

        //Función que reinicia la escena activa
        public void ReloadGame()
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
