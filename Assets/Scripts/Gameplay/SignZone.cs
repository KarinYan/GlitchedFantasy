using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Platformer.Core.Simulation;
using UnityEngine.SceneManagement;  

namespace Platformer.Mechanics
{
    public class SignZone : MonoBehaviour
    {
        public GameObject infoMenuUI;
        public static bool gameIsPaused = false;
        private bool triggerEntered = false;
        
        void Update()
        {
            if (Input.GetKeyDown (KeyCode.E) && triggerEntered == true) 
            {
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            PlayerManager player = collision.GetComponent<PlayerManager>();
            triggerEntered = true;            
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            PlayerManager player = collision.GetComponent<PlayerManager>();
            triggerEntered = false;                      
        }

        public void ExitSign()
        {
            triggerEntered = false;
            Time.timeScale = 1f;
            gameIsPaused = false;
        }
    }
}