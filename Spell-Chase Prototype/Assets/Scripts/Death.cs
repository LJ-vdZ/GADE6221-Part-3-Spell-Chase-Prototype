using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    
    [SerializeField] GameObject player;

    //public GameManager GameManager;
    private bool deathStatus = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //private void OnTriggerEnter(Collider other)
    //{
    /*if (collision != null && deathStatus == false)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            player.GetComponent<PlayerMovement>().enabled = false;

            deathStatus = true;
            GameManager.endGame();
        }
    }*/
    //player.GetComponent<PlayerMovement>().enabled = false;


    //}
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))  // Check if colliding object has the "Player" tag
        {
            Debug.Log("Object hit the player!");
            player.GetComponent<PlayerMovement>().enabled = false;
        }
    }
}
