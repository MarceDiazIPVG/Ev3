using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorAudio : MonoBehaviour
{

	public AudioSource Audio;


	public int sound = 0;

	public AudioClip[] Sounds;
	


	

	public void loadClip(int Indice)
	{
		if (Indice < Sounds.Length && Indice >= 0)
		{
			sound = Indice;
			Audio.clip = Sounds[Indice];
			Audio.Play();
			Debug.Log(Audio.clip.name);
			Debug.Log("audio posicion: " + Indice);
		}
		else
		{
			Debug.Log("Indice fuera de rango");
		}
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
			loadClip(sound);
        }
    }
}
