using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] GameObject player;

    public GameManager GameManager;
    private bool deathStatus = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null && deathStatus == false)
        {
            if (collision.gameObject.GetComponent<CharacterController>() != null)
            {
                player.GetComponent<CharacterController>().enabled = false;

                deathStatus = true;
                GameManager.endGame();
            }
        }
    }
}
