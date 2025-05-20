using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodgoVolumen : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    public Image imagenMute;
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        AudioListener.volume = slider.value;
        RevisarSiEstaMuteado();
    }
    public void ChengeSlider(float valor)
    {
        sliderValue = valor;
        PlayerPrefs.SetFloat("volumenAudio", sliderValue);
        AudioListener.volume = sliderValue;
        RevisarSiEstaMuteado();
    }

    public void RevisarSiEstaMuteado()
    {
        if (sliderValue == 0)
        {
            imagenMute.gameObject.SetActive(true);
        }
        else
        {
            imagenMute.gameObject.SetActive(false);
        }
    }

}
