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
    public class DeathZone : MonoBehaviour
    {
        private Rigidbody2D Rigidbody2D;
        public GameObject gameOverMenuUI;
        public static bool gameIsPaused = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            PlayerManager player = collision.GetComponent<PlayerManager>();
            if (player != null)
            {
                player.DeathZone();
                gameOverMenuUI.SetActive(true);
                Time.timeScale = 0f;
                gameIsPaused = true;
            }
        }
    }
}