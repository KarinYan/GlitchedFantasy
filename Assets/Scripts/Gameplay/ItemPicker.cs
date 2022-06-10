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
        public GameObject infoHealthUI;
        [HideInInspector]
        public Text coreCounter;
        private bool triggerEntered = false;             
        private bool interactButtonPressed = false; 
        private bool infoHealthUIactive = false;

        private int core;
        private int sceneCores;

        //Función que inicializa el valor del número de núcleos en escena
        void Start()
        {
            sceneCores = GameObject.FindGameObjectsWithTag("Cores").Length;     
        }

        //Función que se ejecuta en cada frame del juego y que, según el estado de salud del jugador y la tecla apretada, permite coleccionar items o desactivar/activar menú
        void Update()
        { 
           if ((Input.GetKeyDown (KeyCode.E) || interactButtonPressed == true) && triggerEntered == true && collectHealth !=null && player.health < player.maxHealth) 
            {
                interactButtonPressed = false;
                collectHealth.HealthCollect();                
                Destroy(gameObject);
            }       

            if ((Input.GetKeyDown (KeyCode.E) || interactButtonPressed == true) && triggerEntered == true && collectHealth !=null && player.health == player.maxHealth)
            {                
                interactButtonPressed = false; 

                if (infoHealthUIactive == false)
                {
                    infoHealthUI.SetActive(true);
                    infoHealthUIactive = true;
                }   
                else 
                {
                    infoHealthUI.SetActive(false);   
                    infoHealthUIactive = false;
                    triggerEntered = false; 
                } 
            }  
        }        

        //Función que activa un valor para condición del Update
        public void OnClick()
        {
            if (triggerEntered == true)
            {
                interactButtonPressed = true;  
            }            
        }

        //Función que desactiva menú HealthUI
        public void CloseHealthUI()
        {
            infoHealthUI.SetActive(false);
        }

        //Función que, al producirse un contacto entre objetos, desencadena la posibilidad de coleccionar salud al jugador
        //y actualiza el contador de núcleos, destruyéndolos posteriormente
        private void OnTriggerEnter2D(Collider2D other)
        {
            player = other.GetComponent<PlayerManager>();
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
            player = other.GetComponent<PlayerManager>();
            triggerEntered = false; 
        }
        
    }
}
