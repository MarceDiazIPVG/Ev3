using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Registros : MonoBehaviour
{
    public TMPro.TMP_InputField input;
    public TMPro.TMP_Dropdown dropdown;
    public Slider slider;
    public AudioSource audioSource;


    public void getinput()
    {

        string nombre = input.text;
        Debug.Log("El nombre es: " + nombre);

    }

    public void getdropdown()
    {

        int valor = dropdown.value;
        TMPro.TMP_Dropdown.OptionData seleccion = dropdown.options[valor];

        string select = seleccion.text;
        Debug.Log("La opcion escogida es: " + select);
        Debug.Log("El indice es: " + valor);
    }

    public void setAudio()
    {
        float volumen;
        volumen = slider.value;
        audioSource.volume = volumen;
        Debug.Log("El volumen es: " + volumen);
    }

}
