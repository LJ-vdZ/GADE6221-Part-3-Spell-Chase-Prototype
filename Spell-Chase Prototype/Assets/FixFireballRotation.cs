using UnityEngine;

public class FixFireballRotation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //change objects rotation so it is up-right
        transform.rotation = Quaternion.Euler(0f, -90f, 0f);
    }

    
}
