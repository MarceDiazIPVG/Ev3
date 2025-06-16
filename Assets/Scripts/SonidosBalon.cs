using System.Collections;
using UnityEngine;

public class SonidosBalon : MonoBehaviour
{
    public AudioClip sonidoGolpe;
    public AudioClip sonidoArco;
    public string tagArco = "Arco";
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Colisión detectada con: {collision.gameObject.name} (Tag: {collision.gameObject.tag})");
        // Solo reproducir sonido si colisiona con el arco
        if (collision.gameObject.CompareTag(tagArco))
        {
            if (sonidoArco != null)
                audioSource.PlayOneShot(sonidoArco);
        }
    }

    // Método público para reproducir el sonido de golpe
    public void ReproducirSonidoGolpe()
    {
        if (sonidoGolpe != null)
        {
            audioSource.PlayOneShot(sonidoGolpe);
            Debug.Log("Reproduciendo sonido de golpe");
        }
        else
        {
            Debug.LogWarning("No hay clip asignado para sonidoGolpe");
        }
    }
}





