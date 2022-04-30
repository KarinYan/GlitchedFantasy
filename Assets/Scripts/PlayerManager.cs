using System;
using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
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
        private bool Grounded;
        private float lastShoot;
        public int maxHealth;
        [HideInInspector]
        public int health;

        void Start()
        {
            health = maxHealth; 
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
        }
    
        void Update()
        {
            Horizontal = Input.GetAxisRaw("Horizontal");

            if (Horizontal < 0.0f) transform.localScale = new Vector3(-5.0f, 5.0f, 1.0f);
            else if (Horizontal > 0.0f) transform.localScale = new Vector3(5.0f, 5.0f, 1.0f);

            Animator.SetBool("running", Horizontal != 0.0f); 

            Animator.SetBool("grounded", Grounded); 

            if (Physics2D.Raycast(transform.position, Vector3.down, 1.0f))
            {
                Grounded = true;
            }
            else Grounded = false;

            if (Input.GetKeyDown(KeyCode.W) && Grounded)
            {
                Jump();
            }

            if (Input.GetKey(KeyCode.Space) && Time.time > lastShoot + 0.25f)
            {
                Shoot();
                lastShoot = Time.time;
            }
        }

         private void FixedUpdate()
        {
            Rigidbody2D.velocity = new Vector2(Horizontal * speed, Rigidbody2D.velocity.y);
        }

        private void Jump()
        {
            Rigidbody2D.AddForce(Vector2.up * jumpForce);
        }

        private void Shoot()
        {
            Vector3 direction;
            if (transform.localScale.x == 5.0f) direction = Vector2.right;
            else direction = Vector2.left;

            GameObject projectile = Instantiate(projectilePrefab, transform.position + direction * 1f, Quaternion.identity);
            projectile.GetComponent<AttackManager>().SetDirection(direction);
        }
       
        public void Touch()
        {             
            health = health - 1;
            OnPlayerDamaged?.Invoke();
            if (health == 0) 
            {
                Destroy(gameObject);
                gameOverMenuUI.SetActive(true);
                Time.timeScale = 0f;
                gameIsPaused = true;
            }
        }     

        public void HealthCollect()
        {
            health = health + 1;
            OnPlayerHealed?.Invoke();
        }    

        public void DeathZone()
        {
            health = 0;
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