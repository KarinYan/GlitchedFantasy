using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    //Clase que actualiza la barra del estado de salud
    public class HealthBar : MonoBehaviour
    {
        public GameObject heartPrefab;
        public PlayerManager playerHealth;
        List<StatusHearts> hearts = new List<StatusHearts>();

        //Función que se ejecuta siempre que el objeto se activa, para suscribirse a los eventos del PlayerManager 
        private void OnEnable()
        {
            PlayerManager.OnPlayerDamaged += DrawHearts;
            PlayerManager.OnPlayerHealed += DrawHearts;
        }
        //Función que se ejecuta siempre que el objeto se desactiva, para desuscribirse a los eventos del PlayerManager 
        private void OnDisable()
        {
            PlayerManager.OnPlayerDamaged -= DrawHearts;
            PlayerManager.OnPlayerHealed -= DrawHearts;
        }

        //Función que ejecuta la actualización de la barra de estado siempre que el objeto se activa
        private void Start()
        {
            DrawHearts();
        }
        
        //Función que actualiza la barra de estado en varios pasos:
        //1. Limpiar la barra de estado
        //2. Crear corazones vacíos hasta el límite de la salud máxima definida en PlayerHealth
        //3. Actualizar la imagen de los corazones según el estado actual del jugador definido en PlayerHealth
        public void DrawHearts()
        {
            ClearHearts();

            float maxHealthRemainder = playerHealth.maxHealth % 2;
            
            int heartsToMake = (int)(playerHealth.maxHealth / 2 + maxHealthRemainder);
            for(int i = 0; i < heartsToMake; i++)
            {
                CreateEmptyHeart();
            }
            
            for(int i = 0; i < hearts.Count; i++)
            {
                int heartStatusRemainder = (int)Mathf.Clamp(playerHealth.health - (i * 2), 0, 2);
                hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
            }

            foreach(Transform t in transform)
            {
                t.GetComponent<RectTransform>().localScale = Vector3.one;
            }
        }

        //Función que crea un corazón vacío en la barra de estado
        public void CreateEmptyHeart()
        {
            GameObject newHeart = Instantiate(heartPrefab);
        
            newHeart.transform.SetParent(transform);
            StatusHearts heartComponent = newHeart.GetComponent<StatusHearts>();
            heartComponent.SetHeartImage(HeartStatus.Empty);
            hearts.Add(heartComponent);
        }

        //Función que limpia la barra de estado destruyendo todos los corazones y creando una lista vacía
        public void ClearHearts()
        {
            foreach(Transform t in transform)
            {               
                Destroy(t.gameObject);
            }
            hearts = new List<StatusHearts>();
        }
    }
}
