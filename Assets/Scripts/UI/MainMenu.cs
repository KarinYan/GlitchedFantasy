using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

namespace Platformer.Mechanics
{
    //Clase que controla las acciones del menú principal
    public class MainMenu : MonoBehaviour
    {
        private int lastScene;
        
        //Función que vuelve a cargar el juego desde la primera pantalla
        public void NewGame()
        {
            SceneManager.LoadScene(2);
            Time.timeScale = 1f;
        }

        //Función que carga la última escena jugada
        public void LoadGame()
        {
            lastScene = PlayerPrefs.GetInt("SavedScene");

            if (lastScene !=0)
            {
                SceneManager.LoadScene((int)(lastScene));
                Time.timeScale = 1f;
            }
            else return;
        }

        //Función que finaliza el juego
        public void QuitGame()
        {
            Debug.Log("QUIT!");
            Application.Quit();
        }
    } 
}