using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Clase para controlar el tiempo de la intro.
public class Wait : MonoBehaviour
{
    public float wait_time = 5f;

    //Función que inicializa la corrutina de espera
    void Start()
    {
        StartCoroutine(wait_for_intro());
    }

    //Función que marca un tiempo de espera y posteriormente carga el menú principal
    IEnumerator wait_for_intro() 
    {
        yield return new WaitForSeconds(wait_time);

        SceneManager.LoadScene(1);
    }
}
