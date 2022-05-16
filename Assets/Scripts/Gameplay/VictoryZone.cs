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

        //Función que se ejecuta en cada frame del juego y que calcula la cantidad de núcleos presentes en la escena
        void Update()
        { 
            sceneCores = GameObject.FindGameObjectsWithTag("Cores").Length;          
        }

        //Función que invoca la función de pase de escena del PlayerManager, cuando el jugador entra en la zona de victoria habiendo recolectado todos los núcleos
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