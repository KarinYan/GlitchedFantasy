using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Platformer.Core.Simulation;
using UnityEngine.SceneManagement;  

namespace Platformer.Mechanics
{
    /// <summary>
    /// VictoryZone components mark a collider which will schedule a
    /// PlayerEnteredVictoryZone event when the player enters the trigger.
    /// </summary>
    public class VictoryZone : MonoBehaviour
    {
        private Rigidbody2D Rigidbody2D;
        public static bool gameIsPaused = false;
        private int sceneCores;

        void Update()
        { 
            sceneCores = GameObject.FindGameObjectsWithTag("Cores").Length;          
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            PlayerManager player = collision.GetComponent<PlayerManager>();
            if (player != null && sceneCores == 0)
            {
                player.PlayerDied();
            }
        }
    }
}