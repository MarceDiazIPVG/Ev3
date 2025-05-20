using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Temporizador : MonoBehaviour
{
    public float tiempoRestante = 15f;
    public Text textoTemporizador;

    public GameObject pantallaGameOver; // Asigna en el Inspector un panel con texto + botón

    void Update()
    {
        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            textoTemporizador.text = "Tiempo: " + Mathf.CeilToInt(tiempoRestante).ToString();
        }
        else
        {
            GameOverPorTiempo();
        }
    }

    void GameOverPorTiempo()
    {
        Debug.Log("Tiempo terminado. Game Over.");
        pantallaGameOver.SetActive(true);
        Time.timeScale = 0f; // Detener el juego
    }

    // Este método se llama desde el botón en el menú GameOver
    public void VolverAlMenu()
    {
        Time.timeScale = 1f; // Restaurar el tiempo por si estaba pausado
        SceneManager.LoadScene("MenuPrincipal"); // Cambia el nombre por el de tu escena de menú
    }
}