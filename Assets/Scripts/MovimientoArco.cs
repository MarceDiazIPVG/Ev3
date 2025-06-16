using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoArco : MonoBehaviour
{
    private float rangoMovimiento;  // ahora privado y variable según dificultad
    private float velocidad = 1f;
    private Vector3 posicionInicial;
    private bool moverArco = false;

    void Start()
    {
        posicionInicial = transform.position;

        // Obtener la dificultad desde PlayerPrefs
        int dificultad = PlayerPrefs.GetInt("DificultadSeleccionada", 0); // 0: Fácil, 1: Media, 2: Difícil

        switch (dificultad)
        {
            case 0: // Fácil
                velocidad = 1f;
                rangoMovimiento = 1.5f;
                break;
            case 1: // Media
                velocidad = 2f;
                rangoMovimiento = 2.5f;
                break;
            case 2: // Difícil
                velocidad = 3.5f;
                rangoMovimiento = 3.5f;
                break;
            default:
                velocidad = 1f;
                rangoMovimiento = 1.5f;
                break;
        }

        // Obtener el estado del toggleMoverArco (0 o 1)
        int mover = PlayerPrefs.GetInt("MoverArco", 0);
        moverArco = mover == 1;
    }

    void Update()
    {
        if (!moverArco)
            return; // No se mueve si no está activado

        float desplazamientoZ = Mathf.PingPong(Time.time * velocidad, rangoMovimiento * 2f) - rangoMovimiento;
        transform.position = new Vector3(posicionInicial.x, posicionInicial.y, posicionInicial.z + desplazamientoZ);
    }
}
