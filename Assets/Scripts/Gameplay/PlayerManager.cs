using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Platformer.Core.Simulation;


namespace Platformer.Mechanics
{
    //Clase que administra el comportamiento y estados del jugador
    public class PlayerManager : MonoBehaviour
    {   
        public static event Action OnPlayerDamaged;
        public static event Action OnPlayerHealed;
        public GameObject projectilePrefab;
        public GameObject gameOverMenuUI;
        public AudioClip jumping;
        public AudioClip hurt;
        public AudioClip dead;
        public AudioClip collectPotions;
        public AudioClip collectCores;
        AudioSource audioSource;

        public float speed;
        public float jumpForce;      
        public static bool gameIsPaused = false;
        
        private Rigidbody2D Rigidbody2D;
        private Animator Animator;

        private float Horizontal; 
        private bool playerIsGrounded;
        private bool playerIsDead = false;
        private bool playerIsJumping = false;
        private bool playerIsLanding = false;
        private bool playerIsFalling = false; 
        private bool playerIsShooting = false; 
        private float lastShoot;

        public int maxHealth;
        [HideInInspector]
        public int health;

        //Función que inicializa la salud del jugador, la física como cuerpo rígido 2D, sus animaciones y sonidos, siempre que se activa la clase
        void Start()
        {
            health = maxHealth; 
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
        }
    
        //Función que se ejecuta en cada frame del juego y que va actualizando las animaciones, sonidos y el estado del jugador
        //en base a distintos parámetros (cumplimiento de condiciones de estados, teclas apretadas, salud)
        void Update()
        {     
            Animator.SetBool("running", Horizontal != 0.0f); 
            Animator.SetBool("grounded", playerIsGrounded); 
            Animator.SetBool("dead", playerIsDead);
            Animator.SetBool("jumping", playerIsJumping);
            Animator.SetBool("landing", playerIsLanding);
            Animator.SetBool("falling", playerIsFalling);
            Animator.SetBool("shooting", playerIsShooting); 
            Horizontal = Input.GetAxisRaw("Horizontal");

            if (Horizontal < 0.0f) transform.localScale = new Vector3(-0.5f, 0.5f, 1.0f);
            else if (Horizontal > 0.0f) transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);               

            if (Physics2D.Raycast(transform.position, Vector3.down, 1f))
            {
                if (Rigidbody2D.velocity.y < 0)
                {
                    playerIsLanding = true;                              
                }
                else playerIsLanding = false;
                playerIsGrounded = true;
            }
            else playerIsGrounded = false;

            if (Input.GetKeyDown(KeyCode.W) && playerIsGrounded)
            {
                playerIsJumping = true;
                Jump();
            }

            if (Input.GetKey(KeyCode.Space) && Time.time > lastShoot + 0.25f)
            {                   
                Rigidbody2D.velocity = new Vector3(Horizontal * 0, Rigidbody2D.velocity.y);  
                playerIsShooting = true;               
                Shoot();
                lastShoot = Time.time;
            }
            else playerIsShooting = false;            

            if (Rigidbody2D.velocity.y < 0 && playerIsGrounded == false)
            {
                playerIsFalling = true; 
                playerIsJumping = false;
            }
            else playerIsFalling = false; 

            if (health == 0)
            {
                PlayerDied();
            }            
        }

        //Función que se va ejecutando cada 0.02 segundos para actualizar la velocidad del jugador según tecla apretada
        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Rigidbody2D.velocity = new Vector3(0,Rigidbody2D.velocity.y);
            }
            else Rigidbody2D.velocity = new Vector3(Horizontal * speed, Rigidbody2D.velocity.y);            
        }

        //Función que aplica velocidad en el eje Y del jugador en función de la fuerza de salto
        private void Jump()
        {
            Rigidbody2D.AddForce(Vector2.up * jumpForce);
            audioSource.PlayOneShot(jumping, 0.7f);
        }

        //Función que activa lanzamiento de proyectil
        private void Shoot()
        {
            Vector3 direction;
            if (transform.localScale.x == 0.5f) 
            {
                direction = Vector3.right;
            }
            else {
                direction = Vector3.left;
            }       

            GameObject projectile = Instantiate(projectilePrefab, transform.position + direction * 1.0f, Quaternion.identity);
            projectile.GetComponent<AttackManager>().SetDirection(direction);
        }
       
        //Función que deduce una vida al jugador, invoca el evento OnPlayerDamaged y reproduce un sonido
        public void Touch()
        {             
            health = health - 1;
            OnPlayerDamaged?.Invoke();
            audioSource.PlayOneShot(hurt);
        }

        //Función que incrementa una vida, invoca el evento OnPlayerHealed y reproduce un sonido
        public void HealthCollect()
        {
            health = health + 1;
            audioSource.PlayOneShot(collectPotions, 0.7f);
            OnPlayerHealed?.Invoke();
        }    

        //Función que actualiza el estado del jugador a muerto y reproduce un sonido
        public void PlayerDied()
        {
            playerIsDead = true;
            health = 0;
            audioSource.PlayOneShot(dead, 0.7f);
        }

        //Función que actualiza el estado del jugador a muerto y destruye el objeto
        public void Die()
        {            
            gameOverMenuUI.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused = true;
            Destroy(gameObject);
        }        

        //Función que destruye los objetos etiquetados con TutorialObjects y 
        //reproduce un sonido al entrar en contacto con los objetos etiquetados con Cores
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.transform.tag == "TutorialObjects")               
            {              
                Destroy(other.gameObject);                                                    
            }

            if(other.transform.tag == "Cores")               
            {              
                audioSource.PlayOneShot(collectCores, 0.7f);                                                    
            }
        }
    }
}