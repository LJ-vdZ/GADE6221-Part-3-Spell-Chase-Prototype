using UnityEngine;

public class FixAngryLogRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //change objects rotation so it is up-right
        transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }

}
