using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
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

        void Start()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
        }

        public void Hit()
        {
            health = health -1;
            if (health == 0) Destroy(gameObject);
        }

        void Update()
        {            
            Animator.SetBool("damaged", damaged); 

            if (health == 1)
            {
                damaged = true;
            }
            else damaged = false;
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