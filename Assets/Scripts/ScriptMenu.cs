using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ScriptMenu : MonoBehaviour
{
    public void iniciarJuego()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("JUEGO");
    }

    public void salir()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego");
    }

    public void volverAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuEV2");
    }
}
