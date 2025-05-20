using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Meta : MonoBehaviour
{
    void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag("Player"))
        {
            Debug.Log("¡Has llegado a la meta!");
            // Aquí puedes cargar una nueva escena o mostrar una pantalla de victoria

            Time.timeScale = 0f;
        }
    }
}