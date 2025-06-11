using UnityEngine;
using UnityEngine.UI;

public class SliderVolumen : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volumenMusica", 0.5f);
        AudioListener.volume = slider.value;
    }

    public void ChangeSlider(float value)
    {
        sliderValue = value;
        PlayerPrefs.SetFloat("volumenMusica", sliderValue);
        AudioListener.volume = slider.value;
    }
    void Update()
    {
        
    }
}
