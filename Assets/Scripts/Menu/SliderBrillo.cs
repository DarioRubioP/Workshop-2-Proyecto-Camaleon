using UnityEngine;
using UnityEngine.UI;

public class SliderBrillo : MonoBehaviour
{
    public Slider sliderBrillo;
    public float sliderValor;
    public Image panelbrillo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sliderBrillo.value = PlayerPrefs.GetFloat("brillo", 0.5f);
        panelbrillo.color = new Color(panelbrillo.color.r,panelbrillo.color.g,panelbrillo.color.b, sliderValor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeSlider(float value)
    {
        sliderValor = value;
        PlayerPrefs.SetFloat("brillo", sliderValor);
        panelbrillo.color = new Color(panelbrillo.color.r, panelbrillo.color.g, panelbrillo.color.b, sliderValor);
    }
}
