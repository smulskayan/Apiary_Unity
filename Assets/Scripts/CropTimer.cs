using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CropTimer : MonoBehaviour 
{
    public Image timeBar;
    public float maxTime;
    private Crop currentCrop;
    private float remainingTime;

    void Start() {
        timeBar.fillAmount = 0;
    }

    void Update() {
        if (currentCrop != null && remainingTime > 0) {
            remainingTime -= Time.deltaTime;
            timeBar.fillAmount = remainingTime / maxTime;
        }
    }

    public void StartTimer(Crop crop, float duration) {
        currentCrop = crop;
        maxTime = duration;
        remainingTime = duration;
    }
}