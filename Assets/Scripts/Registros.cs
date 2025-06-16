using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Registros : MonoBehaviour
{
    public TMP_InputField input;
    public Toggle toggleFacil;
    public Toggle toggleMedia;
    public Toggle toggleDificil;
    public Toggle toggleMoverArco;
    public Slider slider;
    public AudioSource audioSource;

    private static Registros instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        toggleMoverArco.isOn = false;
        PlayerPrefs.SetInt("MoverArco", 0);
        PlayerPrefs.Save();

        Debug.Log("MoverArco desactivado al iniciar");
    }

    public void GuardarDificultad()
    {
        int dificultad = 0;

        if (toggleFacil.isOn)
            dificultad = 0;
        else if (toggleMedia.isOn)
            dificultad = 1;
        else if (toggleDificil.isOn)
            dificultad = 2;

        PlayerPrefs.SetInt("DificultadSeleccionada", dificultad);
        PlayerPrefs.Save();

        Debug.Log("Dificultad guardada: " + dificultad);
    }

    public void GuardarMoverArco()
    {
        int mover = toggleMoverArco != null && toggleMoverArco.isOn ? 1 : 0;
        PlayerPrefs.SetInt("MoverArco", mover);
        PlayerPrefs.Save();

        Debug.Log("MoverArco guardado: " + mover);
    }

    public void GuardarNombre()
    {
        string nombre = input.text;
        PlayerPrefs.SetString("NombreJugador", nombre);
        PlayerPrefs.Save();
        Debug.Log("Nombre guardado: " + nombre);
    }

    public void setAudio()
    {
        float volumen = slider.value;
        audioSource.volume = volumen;
        Debug.Log("El volumen es: " + volumen);
    }

    public void IniciarJuego()
    {
        string nombre = input.text;

        if (string.IsNullOrEmpty(nombre))
        {
            Debug.LogWarning("El nombre del jugador no puede estar vacío");
            return;
        }

        PlayerPrefs.SetString("NombreJugador", nombre);
        PlayerPrefs.Save();

        GuardarDificultad();
        GuardarMoverArco();

        SceneManager.LoadScene("JUEGO");
    }
}
