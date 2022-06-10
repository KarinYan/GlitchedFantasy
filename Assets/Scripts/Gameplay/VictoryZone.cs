using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Platformer.Core.Simulation;
using UnityEngine.SceneManagement;  

namespace Platformer.Mechanics
{
    //Clase de administra la zona de victoria del juego
    public class VictoryZone : MonoBehaviour
    {
        private Rigidbody2D Rigidbody2D;
        public static bool gameIsPaused = false;
        private int sceneCores;
        private int sceneIndex;
        private int sceneUnlockedLocks;
        
        public GameObject door;
        public GameObject victoryUnlock;
        private bool victoryDisplayed = false;
        private float delay = 1.35f; 
        
        //Función que inicializa el indice de la escena activa
        void Start()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            sceneIndex = currentScene.buildIndex;
        }
        
        //Función que se ejecuta en cada frame del juego y que calcula la cantidad de núcleos y locks presentes en la escena
        //si todavía no se ha activado, activa el objeto y lo destruye después
        void Update()
        { 
            sceneCores = GameObject.FindGameObjectsWithTag("Cores").Length;  
            sceneUnlockedLocks = GameObject.FindGameObjectsWithTag("UnlockedLock").Length;  
            if (sceneCores == 0 && victoryDisplayed == false)
            {     
                victoryUnlock.SetActive(true);           
                Destroy(door.gameObject);
                Destroy(victoryUnlock.gameObject, delay);
                victoryDisplayed = true;
            }       
        }


        //Función que, según el cumplimiento de condiciones, devuelve al jugador al inicio o lo pasa al siguiente nivel
        private void OnTriggerEnter2D(Collider2D collision)
        {
            PlayerManager player = collision.GetComponent<PlayerManager>();
            if (player != null && sceneCores == 0)
            {
                if (sceneIndex == 3)
                {
                    SceneManager.LoadScene(1);
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    Time.timeScale = 1f;
                }
            }
        }
    }
}