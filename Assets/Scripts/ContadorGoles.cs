using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ContadorGoles : MonoBehaviour
{
    private int goles = 0;
    private GameObject pelota;
    private Vector3 posicionInicialPelota;

    public ParticleSystem fireworksPrefab;  // Asignar en inspector

    public GameObject canvasVictoria;
    public TMP_Text textoNombre;
    public TMP_Text textoDificultad;
    public TMP_Text textoGoles;

    public GameObject pelotaPrefab; // Asignar prefab de la pelota en el inspector

    void Start()
    {
        pelota = GameObject.FindGameObjectWithTag("Pelota");

        if (pelota != null)
        {
            posicionInicialPelota = pelota.transform.position;
        }
        else
        {
            Debug.LogWarning("No se encontró la pelota con tag 'Pelota'");
        }

        if (canvasVictoria != null)
            canvasVictoria.SetActive(false);
        else
            Debug.LogWarning("canvasVictoria no asignado en inspector");
    }

    // Permitir que la pelota notifique cuando se destruye
    public void NotificarPelotaDestruida(GameObject pelotaDestruida)
    {
        if (pelota == pelotaDestruida)
        {
            pelota = null;
        }
    }

    void Update()
    {
        // Si no hay pelota en escena, crear una nueva
        if (pelota == null)
        {
            if (pelotaPrefab == null)
            {
                Debug.LogWarning("pelotaPrefab no asignado en inspector o es null");
                return;
            }
            pelota = Instantiate(pelotaPrefab, posicionInicialPelota, Quaternion.identity);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pelota"))
        {
            goles++;
            Debug.Log("¡Gol! Total goles: " + goles);

            ReiniciarPelota();

            if (goles >= 3)
            {
                Debug.Log("¡Has ganado el juego con 3 goles!");

                if (fireworksPrefab != null)
                {
                    Vector3 spawnPos = transform.position + Vector3.up * 1f;
                    ParticleSystem fuegos = Instantiate(fireworksPrefab, spawnPos, Quaternion.LookRotation(Vector3.up));
                    var main = fuegos.main;
                    main.useUnscaledTime = true;
                    fuegos.Play();

                    StartCoroutine(DestruirParticulas(fuegos));
                }
                else
                {
                    Debug.LogWarning("No hay prefab de fuegos artificiales asignado");
                }

                MostrarCanvasVictoria();

                Time.timeScale = 0f;
            }
        }
    }

    void ReiniciarPelota()
    {
        if (pelota != null)
        {
            Rigidbody rb = pelota.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;

                pelota.transform.position = posicionInicialPelota;

                rb.isKinematic = false;
            }
        }
    }

    void MostrarCanvasVictoria()
    {
        if (canvasVictoria != null)
        {
            canvasVictoria.SetActive(true);

            string nombre = PlayerPrefs.GetString("NombreJugador", "Jugador");
            int dificultadInt = PlayerPrefs.GetInt("DificultadSeleccionada", 0);
            string dificultadStr = "Fácil";

            switch (dificultadInt)
            {
                case 0: dificultadStr = "Fácil"; break;
                case 1: dificultadStr = "Media"; break;
                case 2: dificultadStr = "Difícil"; break;
            }

            if (textoNombre != null)
                textoNombre.text = "Nombre: " + nombre;
            if (textoDificultad != null)
                textoDificultad.text = "Dificultad: " + dificultadStr;
            if (textoGoles != null)
                textoGoles.text = "Goles: " + goles.ToString();
        }
        else
        {
            Debug.LogWarning("canvasVictoria no asignado");
        }
    }

    IEnumerator DestruirParticulas(ParticleSystem ps)
    {
        while (ps.IsAlive(true))
        {
            yield return null;
        }
        Destroy(ps.gameObject);
    }
}
