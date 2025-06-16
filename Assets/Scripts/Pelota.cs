using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pelota : MonoBehaviour
{
    public float fuerzaMin = 5f;
    public float fuerzaMax = 80f;
    public float tiempoMax = 3f;
    private float tiempoPresionado = 0f;
    private bool presionando = false;
    private Coroutine autoDestruirCoroutine;
    public string tagArco = "Arco";
    public GameObject pelotaPrefab;
    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;

    void Start()
    {
        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;
        // Iniciar autodestrucción en 3 segundos
        autoDestruirCoroutine = StartCoroutine(AutoDestruirEnTiempo(4f));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            presionando = true;
            tiempoPresionado = 0f;
        }
        if (presionando)
        {
            tiempoPresionado += Time.deltaTime;
            tiempoPresionado = Mathf.Clamp(tiempoPresionado, 0, tiempoMax);
        }
        if (Input.GetKeyUp(KeyCode.Space) && presionando)
        {
            presionando = false;
            float fuerza = Mathf.Lerp(fuerzaMin, fuerzaMax, tiempoPresionado / tiempoMax);
            LanzarBalo(fuerza);

            // Reproducir sonido golpe al lanzar la pelota
            SonidosBalon sonidos = GetComponent<SonidosBalon>();
            if (sonidos != null)
            {
                sonidos.ReproducirSonidoGolpe();
            }
        }
    }

    IEnumerator AutoDestruirEnTiempo(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        ReaparecerPelota();
        Destroy(gameObject);
    }

    void ReaparecerPelota()
    {
        if (pelotaPrefab != null)
        {
            Instantiate(pelotaPrefab, posicionInicial, rotacionInicial);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Si colisiona con el arco, reproducir sonido de gol usando PlayClipAtPoint, reaparecer y destruir la pelota
        if (collision.gameObject.CompareTag(tagArco))
        {
            SonidosBalon sonidos = GetComponent<SonidosBalon>();
            if (sonidos != null && sonidos.sonidoArco != null)
            {
                AudioSource.PlayClipAtPoint(sonidos.sonidoArco, transform.position);
            }
            if (autoDestruirCoroutine != null)
                StopCoroutine(autoDestruirCoroutine);
            ReaparecerPelota();
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        // Notificar a ContadorGoles que esta pelota fue destruida
        ContadorGoles contador = FindObjectOfType<ContadorGoles>();
        if (contador != null)
        {
            contador.NotificarPelotaDestruida(this.gameObject);
        }
    }

    void LanzarBalo(float fuerza)
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * fuerza, ForceMode.Impulse);
    }
}
