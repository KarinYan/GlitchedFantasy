using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Platformer.Core.Simulation;


namespace Platformer.Mechanics
{
    //Clase que administra las acciones de ataque
    public class AttackManager : MonoBehaviour
    {
        public float Speed;
        private Rigidbody2D Rigidbody2D;
        private Vector3 Direction; 
        private float Horizontal; 

        //Función que inicializa la física del proyectil como cuerpo rígido 2D siempre que se activa la clase
        void Start()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        //Función que se va ejecutando cada 0.02 segundos para actualizar la velocidad del proyectil
        private void FixedUpdate()
        {
            Rigidbody2D.velocity = Direction * Speed;
        }

        //Función que actualiza la dirección y orientación del proyectil con el vector indicado
        public void SetDirection(Vector3 direction)
        {
            Direction = direction;
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-5f, 5f, 1.0f);
            }
            else transform.localScale = new Vector3(5f, 5f, 1.0f);
        }

        //Función que destruye el proyectil 
        public void DestroyBullet()
        {
            Destroy(gameObject);
        }

        //Función que, al entrar el proyectil en contacto con el enemigo o la corrupción, llama las funciones Hit correspondientes y destruye el proyectil
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