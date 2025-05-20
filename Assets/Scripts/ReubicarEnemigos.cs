using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReubicarEnemigos : MonoBehaviour
{
    public GameObject[] enemigosExistentes;      // Asigna enemigo1, enemigo2, ..., enemigo5
    public Transform[] posicionesSpawn;          // Puedes asignar muchas más de 5

    void Start()
    {
        if (enemigosExistentes.Length > posicionesSpawn.Length)
        {
            Debug.LogError("Hay más enemigos que posiciones disponibles.");
            return;
        }

        // Creamos una lista con todas las posiciones disponibles
        List<Transform> posicionesDisponibles = new List<Transform>(posicionesSpawn);

        // Para cada enemigo, le damos una posición aleatoria y la quitamos de la lista
        foreach (GameObject enemigo in enemigosExistentes)
        {
            int index = Random.Range(0, posicionesDisponibles.Count);
            Transform posicionSeleccionada = posicionesDisponibles[index];

            enemigo.transform.position = posicionSeleccionada.position;

            posicionesDisponibles.RemoveAt(index); // Así no se repiten posiciones
        }
    }
}