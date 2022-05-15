using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Platformer.Core.Simulation;


namespace Platformer.Mechanics
{
    public class CorruptionManager : MonoBehaviour
    {
        private Rigidbody2D Rigidbody2D;
        private Animator Animator;
        private int health = 2;
        private bool damaged;
        private bool destroyed;

        void Start()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
        }

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

        public void Hit()
        {
            health = health -1;
            if (health == 0)
            {
                destroyed = true;
            }
        }

        public void Destroy()
        {
            Destroy(gameObject);
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
