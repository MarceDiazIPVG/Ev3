using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptMenu : MonoBehaviour
{

    public void iniciarJuego(string nivel)
    {
        SceneManager.LoadScene(nivel);
    }

    public void salir()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego");
    }
}
