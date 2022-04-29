using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;


namespace Platformer.Mechanics
{
    public class AttackManager : MonoBehaviour
    {
        public float Speed;

        private Rigidbody2D Rigidbody2D;
        private Vector2 Direction;

        void Start()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Rigidbody2D.velocity = Direction * Speed;
        }

        public void SetDirection(Vector2 direction)
        {
            Direction = direction;
        }

        public void DestroyBullet()
        {
            Destroy(gameObject);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            EnemyManager enemy = collision.GetComponent<EnemyManager>();
            CorruptionManager corruption = collision.GetComponent<CorruptionManager>();
            if (enemy != null)
            {
                enemy.Hit();
                DestroyBullet();
            }

            if ( corruption != null)
            {
                corruption.Hit();
                DestroyBullet();
            }

        }
    }
}