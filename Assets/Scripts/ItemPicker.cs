using System;
using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using UnityEngine;
using UnityEngine.UI;
using static Platformer.Core.Simulation;


namespace Platformer.Mechanics
{
    public class ItemPicker : MonoBehaviour
    {
        public Text coreCounter;
        //private bool isPressed = false;
       // private PlayerManager collectHealth;
        
        private int core;
        //public int potion;

        void Update()
        { 
            

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            /*if(other.transform.tag == "Potions" && (Input.GetKey(KeyCode.E)) )               
            {              
                potion++;
                                                      
            }*/

            if(other.transform.tag == "Cores")
            {
                core ++;
                coreCounter.text = core.ToString();
                Destroy(other.gameObject);
            }
        }

        
    }
}
