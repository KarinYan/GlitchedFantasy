using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Platformer.Core.Simulation;


namespace Platformer.Mechanics
{
    public class ItemPicker : MonoBehaviour
    {
        private PlayerManager collectHealth;
        private PlayerManager player;
        public Text coreCounter;
        private bool triggerEntered = false;
        
        private int core;

        void Update()
        { 
            if (Input.GetKeyDown (KeyCode.E) && triggerEntered == true && collectHealth !=null) 
            {
                collectHealth.HealthCollect();                
                Destroy(gameObject);
            }            
        }

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
                coreCounter.text = core.ToString();
                Destroy(other.gameObject);
            }
        }
        
    }
}
