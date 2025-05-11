using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;


public class PickupBar : MonoBehaviour
{
    public Slider slider;
    public MoveHallway cooldownDuration;
    //public float max = 40f;


    public void setMaxSlider(float max)
    {
        slider.maxValue = max;
        slider.value = max;
    }

    public void sliderValue(float value) 
    {
        slider.value = value;
    }

    

}
//https://youtu.be/BLfNP4Sc_iA?si=EsXPTGAxHK9He-DQ
