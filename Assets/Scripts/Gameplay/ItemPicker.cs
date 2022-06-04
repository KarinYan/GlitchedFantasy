using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Platformer.Core.Simulation;


namespace Platformer.Mechanics
{
    //Clase encargada de coleccionar objetos
    public class ItemPicker : MonoBehaviour
    {
        private PlayerManager collectHealth;
        public PlayerManager player;
        [HideInInspector]
        public Text coreCounter;
        private bool triggerEntered = false;

        private int core;
        private int sceneCores;
        void Start()
        {
            sceneCores = GameObject.FindGameObjectsWithTag("Cores").Length;     
        }
        void Update()
        { 
           if (Input.GetKeyDown (KeyCode.E) && triggerEntered == true && collectHealth !=null && player.health < player.maxHealth) 
            {
                collectHealth.HealthCollect();                
                Destroy(gameObject);
            }            
        }

        //Función que, al producirse un contacto entre objetos, desencadena la posibilidad de coleccionar salud al jugador
        //y actualiza el contador de núcleos, destruyéndolos posteriormente
        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerManager player = other.GetComponent<PlayerManager>();
            if(player != null)               
            {              
                collectHealth = other.GetComponent<PlayerManager>();
                triggerEntered = true;                                    
            }

            if(other.transform.tag == "Cores")
            {
                core ++;
                coreCounter.text = (core.ToString() + "/" + sceneCores);
                Destroy(other.gameObject);
            }
        }

        //Función que desactiva la posibilidad de coleccionar salud una vez el jugador ya no está en contacto con la poción
        private void OnTriggerExit2D(Collider2D other)
        {
            PlayerManager player = other.GetComponent<PlayerManager>();
            triggerEntered = false; 
        }
        
    }
}
