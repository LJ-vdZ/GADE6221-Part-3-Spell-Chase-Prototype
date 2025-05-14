using UnityEngine;

public class BossDie : Boss
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (score <= 0)
        {
            Destroy(gameObject);
        } 
    }
}
