using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Platformer.Core.Simulation;


namespace Platformer.Mechanics
{
    public class PlayerManager : MonoBehaviour
    {   
        public static event Action OnPlayerDamaged;
        public static event Action OnPlayerHealed;
        public GameObject projectilePrefab;
        public GameObject gameOverMenuUI;

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

        void Start()
        {
            health = maxHealth; 
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            transform.rotation = Quaternion.identity;
        }
    
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

            if (Physics2D.Raycast(transform.position, Vector3.down, 0.75f))
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
                   
                Rigidbody2D.velocity = new Vector2(Horizontal * 0, Rigidbody2D.velocity.y);  
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

         private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Rigidbody2D.velocity = new Vector2(0,Rigidbody2D.velocity.y);
            }
            else Rigidbody2D.velocity = new Vector2(Horizontal * speed, Rigidbody2D.velocity.y);            
        }

        private void Jump()
        {
            Rigidbody2D.AddForce(Vector2.up * jumpForce);
        }

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
       
        public void Touch()
        {             
            health = health - 1;
            OnPlayerDamaged?.Invoke();
        }

        public void HealthCollect()
        {
            health = health + 1;
            OnPlayerHealed?.Invoke();
        }    

        public void PlayerDied()
        {
            playerIsDead = true;
        }

        public void Die()
        {            
            gameOverMenuUI.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused = true;
            Destroy(gameObject);
        }        

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.transform.tag == "TutorialObjects")               
            {              
                Destroy(other.gameObject);                                                    
            }
        }
    }
}