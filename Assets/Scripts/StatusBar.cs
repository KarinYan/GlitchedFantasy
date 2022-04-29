using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;


namespace Platformer.Mechanics
{
    public class StatusBar : MonoBehaviour
    {
        public GameObject heartPrefab;
        public PlayerManager playerHealth;
        List<HealthBar> hearts = new List<HealthBar>();

        private void OnEnable()
        {
            PlayerManager.OnPlayerDamaged += DrawHearts;
            PlayerManager.OnPlayerHealed += DrawHearts;
        }

        private void OnDisable()
        {
            PlayerManager.OnPlayerDamaged -= DrawHearts;
            PlayerManager.OnPlayerHealed -= DrawHearts;
        }

        private void Start()
        {
            DrawHearts();
        }
        
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

        public void CreateEmptyHeart()
        {
            
            GameObject newHeart = Instantiate(heartPrefab);
        
            newHeart.transform.SetParent(transform);
            HealthBar heartComponent = newHeart.GetComponent<HealthBar>();
            heartComponent.SetHeartImage(HeartStatus.Empty);
            hearts.Add(heartComponent);
        }


        public void ClearHearts()
        {
            
            foreach(Transform t in transform)
            {               
                Destroy(t.gameObject);
            }
            hearts = new List<HealthBar>();
        }

    }
}
