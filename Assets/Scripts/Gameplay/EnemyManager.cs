using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Platformer.Core.Simulation;


namespace Platformer.Mechanics
{
    //Clase que administra el comportamiento y el estado de los enemigos
    public class EnemyManager : MonoBehaviour
    {
        public int health;
        public float speed = 1;
        public bool patrol;

        public Transform groundCheck;
        public LayerMask groundLayer;
        public Collider2D wallCollider;

        private Rigidbody2D Rigidbody2D;
        private Animator Animator;
        private bool move;
        private bool flip;
        private bool enemyIsDead = false;

        public AudioClip hurt;
        public AudioClip dead;
        AudioSource audioSource;

        //Función que inicializa la física del enemigo como cuerpo rígido 2D, sus animaciones y sonidos, siempre que se activa la clase
        void Start()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
            patrol = true;
        }

        //Función que se ejecuta en cada frame del juego y que va actualizando las animaciones, sonidos y el estado del enemigo según las configuraciones de cada uno
        void Update()
        {               
            if(patrol)    
            {
                Patrol();
            }

            Animator.SetBool("moving", move); 
            Animator.SetBool("dead", enemyIsDead); 

            if (speed != 0)
            {
                move = true;
            }
            else move = false;

            if (health <= 0)
            {
                audioSource.PlayOneShot(dead, 0.7f);
                enemyIsDead = true;
                speed = 0;
            }
        }

        //Función que se va ejecutando cada 0.02 segundos para detectar si es el momento de invertir dirección y velocidad
        private void FixedUpdate()
        {
            if (patrol)
            {
                flip = !Physics2D.OverlapCircle(groundCheck.position, 0.75f, groundLayer);
            }
        }

        //Función que llama la función Flip cada vez que el enemigo colisiona con las paredes o llega al final de una plataforma
        void Patrol()
        {
            if (flip || wallCollider.IsTouchingLayers(groundLayer))
            {
                Flip();
            }
            Rigidbody2D.velocity = new Vector2(speed * Time.fixedDeltaTime, Rigidbody2D.velocity.y);
        }

        //Función que invierte la dirección del sprite y la velocidad del enemigo
        void Flip()
        {
            if (speed != 0)
            { 
                patrol = false;
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                speed *= -1;
                patrol = true;
            }
        }      

        //Función que deduce una vida al enemigo y reproduce un sonido
        public void Hit()
        {
            health = health - 1;
            audioSource.PlayOneShot(hurt, 0.7f);
        }

        //Función que destruye el objeto del enemigo
        public void EnemyDeath()
        {
            Destroy(gameObject);
        }

        //Función que llama la función Touch del jugador en el momento que el enemigo colisiona con el jugador
        private void OnCollisionEnter2D(Collision2D collision)
        {
            PlayerManager player = collision.collider.GetComponent<PlayerManager>();
            if (player != null && player.health > 0)
            {
                player.Touch();
            } 
        }        
    }
}