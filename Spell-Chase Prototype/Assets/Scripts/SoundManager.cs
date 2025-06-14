using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    Slider BackgroundMusicSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeBackgroundVolume() 
    {
        AudioListener.volume = BackgroundMusicSlider.value;
    }
}
//https://youtu.be/yWCHaTwVblk
