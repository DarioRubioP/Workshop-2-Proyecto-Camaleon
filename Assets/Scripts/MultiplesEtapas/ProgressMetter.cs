using UnityEngine;
using UnityEngine.UI;

public class ProgressMetter : MonoBehaviour
{
    public Image progressBar;


    public void FillProgressBar(float progress, float maxProgress)
    {
        progress += Time.deltaTime;
        progressBar.fillAmount = progress / maxProgress;
    }
}
