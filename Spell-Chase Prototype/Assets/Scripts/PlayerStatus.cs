using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public bool isImmune = false;
    public float time = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isImmune)
        {
            time -= Time.deltaTime;

            if (time <= 0 )
            {
                isImmune = false;
                time = 10f;
            }
        }
    }
}
