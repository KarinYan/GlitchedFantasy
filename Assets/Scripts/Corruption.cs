using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;


namespace Platformer.Mechanics
{
    public class Corruption : MonoBehaviour
    {
        private Rigidbody2D Rigidbody2D;
        private Animator Animator;
        private int Health = 2;
        private bool Damaged;

        void Start()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
        }

        public void Hit()
        {
            Health = Health -1;
            if (Health == 0) Destroy(gameObject);
        }

        void Update()
        {            
            Animator.SetBool("damaged", Damaged); 

            if (Health == 1)
            {
                Damaged = true;
            }
            else Damaged = false;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            DexterController dexter = collision.collider.GetComponent<DexterController>();
            if (dexter != null)
            {
                dexter.Touch();
                Debug.Log("-1!");
            } 
        }
        
    }
}
