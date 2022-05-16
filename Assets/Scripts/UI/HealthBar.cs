using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Platformer.Core.Simulation;

//Clase encargada de mostrar el sprite del estado de salud del jugador
public class HealthBar : MonoBehaviour
{
    public Sprite emptyHeart, halfHeart, fullHeart;
    Image heartImage;

    //Función que inicializa el sprite de la barra de estado al cargarse la escena
    private void Awake()
    {
        heartImage = GetComponent<Image>();
    }

    //Función que muestra el sprite de corazones correspondiente al estado de salud del jugador
    public void SetHeartImage(HeartStatus status)
    {
        switch (status)
        {
            case HeartStatus.Empty:
                heartImage.sprite = emptyHeart;
                break;
            case HeartStatus.Half:
                heartImage.sprite = halfHeart;
                break;
            case HeartStatus.Full:
                heartImage.sprite = fullHeart;
                break;
        }
    }
}

//Numeración que identifica el estado de salud del jugador
public enum HeartStatus
{
    Empty = 0,
    Half = 1,
    Full = 2
}

