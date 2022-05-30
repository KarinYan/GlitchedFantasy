using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Clase para saltar la intro.
public class SkipVideo : MonoBehaviour
{
    //Funcion que carga la escena del menu.
    public void ReturnMainMenu() 
    {
        
        SceneManager.LoadScene(1);
    }

    //=================== Saltar intro pulsando cualquier tecla ==================================

    public bool ActivarTecla;

    private void Start()
    {
        StartCoroutine(PulsarTecla());
    }

    IEnumerator PulsarTecla()
    {
        yield return new WaitForSeconds(498f);

        ActivarTecla = true;
    }


    private void Update()
    {
        if (ActivarTecla == true && Input.anyKey)
        {
            ReturnMainMenu();
        }

    }
    //============================================================================================


}
