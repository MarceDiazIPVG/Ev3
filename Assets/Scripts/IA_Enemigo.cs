using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA_Enemigo : MonoBehaviour
{
    public NavMeshAgent agenteNavegacion;
    public Transform[] destinos;
    private int i = 0;

    public bool seguirJugador = true;
    private GameObject player;
    private float distanciaJugador;
    public float distanciaMaxima = 10f;

    private int cantidadDeToques = 0;
    private bool estaRalentizado = false;
    private float velocidadOriginal;

    void Start()
    {
        if (destinos.Length > 0)
        {
            agenteNavegacion.destination = destinos[0].position;
        }

        player = GameObject.FindGameObjectWithTag("Player");
        velocidadOriginal = agenteNavegacion.speed;
    }

    void Update()
    {
        if (player == null) return;

        distanciaJugador = Vector3.Distance(transform.position, player.transform.position);

        if (distanciaJugador <= distanciaMaxima && seguirJugador)
        {
            agenteNavegacion.SetDestination(player.transform.position);
        }
        else
        {
            MoverPorRuta();
        }
    }

    void MoverPorRuta()
    {
        if (destinos.Length == 0) return;

        agenteNavegacion.SetDestination(destinos[i].position);

        if (Vector3.Distance(transform.position, destinos[i].position) < 1f)
        {
            i = (i + 1) % destinos.Length;
        }
    }

    void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag("Player"))
        {
            cantidadDeToques++;

            if (cantidadDeToques == 1)
            {
                if (!estaRalentizado)
                {
                    StartCoroutine(RalentizarTemporalmente());
                }
            }
            else if (cantidadDeToques >= 2)
            {
                // Aquí llamas a la función para perder la partida
                Debug.Log("El jugador perdió por kamikaze");
                // Por ejemplo: GameManager.Instance.GameOver("Perdiste por kamikaze");

                Destroy(gameObject);
            }
        }
    }

    IEnumerator RalentizarTemporalmente()
    {
        estaRalentizado = true;
        agenteNavegacion.speed = velocidadOriginal * 0.5f;
        yield return new WaitForSeconds(3f);
        agenteNavegacion.speed = velocidadOriginal;
        estaRalentizado = false;
    }
}