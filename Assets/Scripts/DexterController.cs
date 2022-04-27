using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;


namespace Platformer.Mechanics
{
    public class DexterController : MonoBehaviour
    {
        public GameObject ProjectilePrefab;
        public GameObject gameOverMenuUI;

        public float Speed;
        public float JumpForce;      
        public static bool GameIsPaused = false;
        
        private Rigidbody2D Rigidbody2D;
        private Animator Animator;

        private float Horizontal; 
        private bool Grounded;
        private float LastShoot;
        public int Health = 10;

        void Start()
        {
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

            if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.25f)
            {
                Shoot();
                LastShoot = Time.time;
            }
        }

        private void Jump()
        {
            Rigidbody2D.AddForce(Vector2.up * JumpForce);
        }

        private void Shoot()
        {
            Vector3 direction;
            if (transform.localScale.x == 5.0f) direction = Vector2.right;
            else direction = Vector2.left;

            GameObject projectile = Instantiate(ProjectilePrefab, transform.position + direction * 1f, Quaternion.identity);
            projectile.GetComponent<Attack>().SetDirection(direction);
        }

        private void FixedUpdate()
        {
            Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
        }

        public void Touch()
        {
            Health = Health -1;
            if (Health == 0) 
            {
                Destroy(gameObject);
                gameOverMenuUI.SetActive(true);
                Time.timeScale = 0f;
                GameIsPaused = true;
            }

        }

        public void DeathZone()
        {
            Health = 0;
            Destroy(gameObject);
        }
    }
}