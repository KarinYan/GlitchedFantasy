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
        private PlayerManager player;
        [HideInInspector]
        public Text coreCounter;
        private bool triggerEntered = false;

        private int core;
        private int sceneCores;

        //Función que encuentra los objetos en escena etiquetados con Cores
        void Start()
        {
            sceneCores = GameObject.FindGameObjectsWithTag("Cores").Length;     
        }

        //Función que se ejecuta en cada frame del juego y que invoca la función HealthCollect del PlayerManager y destruye el objeto si se cumplen las condiciones definidas
        void Update()
        { 
            if (Input.GetKeyDown (KeyCode.E) && triggerEntered == true && collectHealth !=null) 
            {
                collectHealth.HealthCollect();                
                Destroy(gameObject);
            }            
        }

        //Función que, al producirse una colisión de objetos, activa la posibilidad de coleccionar salud al jugador
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
        
    }
}
