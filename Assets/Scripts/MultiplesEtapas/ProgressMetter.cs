using UnityEngine;
using UnityEngine.UI;

public class ProgressMetter : MonoBehaviour
{
    public Image progressBar;
    public float maxProgress;
    float progress;


    public void FillProgressBar(/*float progress, float maxProgress*/)
    {
        progress += Time.deltaTime;
        progressBar.fillAmount = progress / maxProgress;
    }

    void Start()
    {
        progress = 0;
    }

    void Update()
    {
        FillProgressBar();
    }
}
