using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;


namespace Platformer.Mechanics
{
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


        void Start()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            patrol = true;
        }

        void Update()
        {   
            if(patrol)    
            {
                Patrol();
            }

            Animator.SetBool("moving", move); 

            if (speed != 0)
            {
                move = true;
            }
            else move = false;
        }

        private void FixedUpdate()
        {
            if (patrol)
            {
                flip = !Physics2D.OverlapCircle(groundCheck.position, 1f, groundLayer);
            }
        }

        void Patrol()
        {
            if (flip || wallCollider.IsTouchingLayers(groundLayer))
            {
                Flip();
            }
            Rigidbody2D.velocity = new Vector2(speed * Time.fixedDeltaTime, Rigidbody2D.velocity.y);
        }

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

        public void Hit()
        {
            health = health - 1;
            if (health == 0) Destroy(gameObject);
        }

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