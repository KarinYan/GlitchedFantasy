using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Platformer.Core.Simulation;
using UnityEngine.SceneManagement;  

namespace Platformer.Mechanics
{
    //Clase de administra la zona de muerte del juego
    public class DeathZone : MonoBehaviour
    {
        private Rigidbody2D Rigidbody2D;
        public GameObject gameOverMenuUI;
        public static bool gameIsPaused = false;

        //Función que invoca la función PlayerDied del PlayerManager, cuando el jugador entra en la zona de muerte
        private void OnTriggerEnter2D(Collider2D collision)
        {
            PlayerManager player = collision.GetComponent<PlayerManager>();
            if (player != null)
            {
                player.PlayerDied();
            }
        }
    }
}