using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

namespace Platformer.Mechanics
{
    //Clase que controla las acciones del menú principal
    public class MainMenu : MonoBehaviour
    {
        //Función que vuelve a cargar el juego desde la primera pantalla
        public void NewGame()
        {
            SceneManager.LoadScene(1);
            Time.timeScale = 1f;
        }

        //Función que carga la escena guardada
        public void LoadGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Time.timeScale = 1f;
        }

        //Función que finaliza el juego
        public void QuitGame()
        {
            Debug.Log("QUIT!");
            Application.Quit();
        }
    } 
}