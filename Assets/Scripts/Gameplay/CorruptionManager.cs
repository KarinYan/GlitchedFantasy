using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Platformer.Core.Simulation;


namespace Platformer.Mechanics
{
    //Clase que administra el estado de las corrupciones
    public class CorruptionManager : MonoBehaviour
    {
        private Rigidbody2D Rigidbody2D;
        private Animator Animator;
        private int health = 2;
        private bool damaged;
        private bool destroyed;

        //Función que inicializa la física de la corrupción como cuerpo rígido 2D y sus animaciones, siempre que se activa la clase
        void Start()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
        }

        //Función que se ejecuta en cada frame del juego y que va actualizando las animaciones y el estado de la corrupción
        void Update()
        {            
            Animator.SetBool("damaged", damaged);            
            Animator.SetBool("destroyed", destroyed); 

            if (health == 1)
            {
                damaged = true;
            }
            else damaged = false;
        }

        //Función que deduce una vida a la corrupción
        public void Hit()
        {
            health = health -1;
            if (health == 0)
            {
                destroyed = true;
            }
        }

        //Función que destruye el objeto de la corrupcicón
        public void Destroy()
        {
            Destroy(gameObject);
        }

        //Función que llama la función Touch del jugador en el momento que la corrupción colisiona con el jugador
        private void OnCollisionEnter2D(Collision2D collision)
        {
            PlayerManager player = collision.collider.GetComponent<PlayerManager>();
            if (player != null)
            {
                player.Touch();
            } 
        }
        
    }
}
