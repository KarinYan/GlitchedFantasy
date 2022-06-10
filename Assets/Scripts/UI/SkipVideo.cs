using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Clase para saltar la intro.
public class SkipVideo : MonoBehaviour
{
    //Función que carga la escena del menú.
    public void ReturnMainMenu() 
    {
        
        SceneManager.LoadScene(1);
    }

    //=================== Saltar intro pulsando cualquier tecla ==================================

    public bool ActivarTecla;

    //Función que inicializa la corrutina de espera
    private void Start()
    {
        StartCoroutine(PulsarTecla());
    }

    //Función que marca un tiempo de espera y posteriormente activa el botón para saltar la intro
    IEnumerator PulsarTecla()
    {
        yield return new WaitForSeconds(498f);

        ActivarTecla = true;
    }

    //Función que se ejecuta en cada frame del juego y comprueba si se ha activado el botón de saltar la intro. Si es así, carga el menú principal
    private void Update()
    {
        if (ActivarTecla == true && Input.anyKey)
        {
            ReturnMainMenu();
        }

    }
    //============================================================================================


}
