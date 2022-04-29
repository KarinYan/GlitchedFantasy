using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;
using UnityEngine.SceneManagement;  

namespace Platformer.Mechanics
{
    /// <summary>
    /// DeathZone components mark a collider which will schedule a
    /// PlayerEnteredDeathZone event when the player enters the trigger.
    /// </summary>
    public class SignZone : MonoBehaviour
    {
        private Rigidbody2D Rigidbody2D;
        public GameObject infoMenuUI;
        public static bool gameIsPaused = false;
        private bool triggerEntered = false;

        void Update()
        {
            if (Input.GetKeyDown (KeyCode.E) && triggerEntered == true) 
            {
                infoMenuUI.SetActive(true);
                Time.timeScale = 0f;
                gameIsPaused = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            PlayerManager player = collision.GetComponent<PlayerManager>();
            if (player != null)
            {                
                triggerEntered = true;
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            PlayerManager player = collision.GetComponent<PlayerManager>();
            if (player = null)
            {                
                triggerEntered = false;
                
            }
        }

        public void ExitSign()
        {
            Time.timeScale = 1f;
            gameIsPaused = false;
        }
    }
}