using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Platformer.Core.Simulation;
using UnityEngine.UI;


namespace Platformer.Mechanics
{
    //Clase que administra el comportamiento y estados del jugador
    public class PlayerManager : MonoBehaviour
    {   
        public static event Action OnPlayerDamaged;
        public static event Action OnPlayerHealed;private AudioManager clip;    
        private Rigidbody2D Rigidbody2D;
        private Renderer Renderer; 
        private Animator Animator;
        public GameObject projectilePrefab;
        public GameObject gameOverMenuUI;        
        public LayerMask groundLayer;
        
        public Joystick joystickRun;
        private float horizontalJoystick;

        public float speed;
        public float jumpForce;      
        public static bool gameIsPaused = false;
        private float Horizontal; 
        private bool playerIsGrounded;
        private bool playerIsRunning = false;
        private bool playerIsDead = false;
        private bool playerIsJumping = false;
        private bool playerIsLanding = false;
        private bool playerIsFalling = false; 
        private bool playerIsShooting = false; 
        
        private bool jumpButtonPressed = false; 
        private bool shootButtonPressed = false; 

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
            Renderer = GetComponent<Renderer>();
            clip = gameObject.GetComponent<AudioManager>();
        }
    
        //Función que se ejecuta en cada frame del juego y que va actualizando las animaciones y el estado del jugador
        //en base a distintos parámetros (cumplimiento de condiciones de estados, teclas apretadas, salud)
        void Update()
        {     
            Animator.SetBool("running", playerIsRunning); 
            Animator.SetBool("grounded", playerIsGrounded); 
            Animator.SetBool("dead", playerIsDead);
            Animator.SetBool("jumping", playerIsJumping);
            Animator.SetBool("landing", playerIsLanding);
            Animator.SetBool("falling", playerIsFalling);
            Animator.SetBool("shooting", playerIsShooting); 
            Horizontal = Input.GetAxisRaw("Horizontal");

            if (Horizontal < 0.0f || horizontalJoystick < 0.0f) 
            {
                transform.localScale = new Vector3(-0.5f, 0.5f, 1.0f);
                if (playerIsGrounded == true)
                {
                    playerIsRunning = true;
                }
                else playerIsRunning = false;                
            }
            else if (Horizontal > 0.0f || horizontalJoystick > 0.0f) 
            {
                transform.localScale = new Vector3(0.5f, 0.5f, 1.0f); 
                if (playerIsGrounded == true)
                {
                    playerIsRunning = true;
                }  
                else playerIsRunning = false;  
            } 
            else playerIsRunning = false;   

            if (Physics2D.Raycast(transform.position, Vector3.down, 1f, groundLayer))
            {
                if (Rigidbody2D.velocity.y < 0)
                {
                    playerIsLanding = true; 
                    playerIsGrounded = false;                             
                }
                else if(Rigidbody2D.velocity.y == 0)
                {
                    playerIsLanding = false;
                    playerIsGrounded = true;  
                }
                else playerIsGrounded = true;
            }
            else playerIsGrounded = false;

            if ((Input.GetKeyDown(KeyCode.W) || jumpButtonPressed == true) && playerIsGrounded && !playerIsLanding)
            {                            
                jumpButtonPressed = false;
                playerIsJumping = true;
                Jump();
            }

            if ((Input.GetKey(KeyCode.Space) || shootButtonPressed == true) && Time.time > lastShoot + 0.25f)
            {   
                shootButtonPressed = false;
                playerIsShooting = true;                
                Rigidbody2D.velocity = new Vector3(Horizontal * 0, Rigidbody2D.velocity.y);                               
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
            if (Input.GetKey(KeyCode.Space) || jumpButtonPressed == true)
            {
                Rigidbody2D.velocity = new Vector3(0,Rigidbody2D.velocity.y);
            }            
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
            {
                Rigidbody2D.velocity = new Vector3(Horizontal * speed, Rigidbody2D.velocity.y); 
            }
            else 
            {
                horizontalJoystick = joystickRun.Horizontal * speed;
                Rigidbody2D.velocity = new Vector3(horizontalJoystick, Rigidbody2D.velocity.y); 
            }      
        }

        //Función que aplica velocidad en el eje Y del jugador en función de la fuerza de salto
        private void Jump()
        {
            Rigidbody2D.AddForce(Vector2.up * jumpForce);
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
       
        //Función que deduce una vida al jugador, invoca el evento OnPlayerDamaged, reproduce un sonido e inicializa la corrutina de parpadeo
        public void Touch()
        {             
            health = health - 1;
            clip.PlayHurtAudio();
            StartCoroutine(Blink(2));
            OnPlayerDamaged?.Invoke();
        }

        //Función que crea el efecto de parpadeo
        private IEnumerator Blink(int loops) {
            for (int i = 0; i < loops; i++)
            {
                transform.GetComponent<Renderer>().material.SetColor("_Color", new Color(1,1,1,0f));
                yield return new WaitForSeconds(0.07f);
                transform.GetComponent<Renderer>().material.SetColor("_Color", new Color(1,1,1,1.0f));
                yield return new WaitForSeconds(0.07f);
            }                        
        }

        //Función que incrementa una vida, invoca el evento OnPlayerHealed y reproduce un sonido
        public void HealthCollect()
        {
            health = health + 1;
            clip.PlayHealthCollectAudio();  
            OnPlayerHealed?.Invoke();
        }    

        //Función que actualiza el estado del jugador a muerto y reproduce un sonido
        public void PlayerDied()
        {
            playerIsDead = true;
            playerIsLanding = false;
            playerIsFalling = false;
            playerIsJumping = false;
            health = 0;
        }

        //Función que actualiza el estado del jugador a muerto y destruye el objeto
        public void Die()
        {            
            gameOverMenuUI.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused = true;
        }  

        //Función que activa un valor para condición del Update
        public void OnShoot() 
        {
            shootButtonPressed = true;          
        }

        //Función que activa un valor para condición del Update
        public void OnJump() 
        {
            jumpButtonPressed = true;   
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
                clip.PlayCoreCollectAudio();                                                     
            }
        }
    }
}