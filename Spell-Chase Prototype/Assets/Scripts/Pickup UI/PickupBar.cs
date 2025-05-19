using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;


public class PickupBar : MonoBehaviour
{
    public static PickupBar Instance { get; private set; }

    public Slider slider;
    public MoveHallway cooldownDuration;

    void Awake()
    {
        // Ensure there's only one instance
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Optional: prevent duplicates
            return;
        }

        Instance = this;
    }

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
//Brackeys, 2020. How to make a HEALTH BAR in Unity!. [video online] Available at: <https://youtu.be/BLfNP4Sc_iA?si=EsXPTGAxHK9He-DQ>[Accessed 05 May 2025].
