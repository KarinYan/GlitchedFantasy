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

}
