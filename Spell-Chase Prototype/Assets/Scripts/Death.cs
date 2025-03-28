using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    
    [SerializeField] GameObject player;

    //public GameObject EndScreenUI;
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
        Debug.Log("Collision detected");
    
    
        if (/*gameObject.GetComponent<PlayerMovement>() != null*/ collision.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerMovement>().enabled = false;

            deathStatus = true;
            GameManager.endGame();
        }
    
    //player.GetComponent<PlayerMovement>().enabled = false;


    }
    /*void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))  // Check if colliding object has the "Player" tag
        {
            Debug.Log("Object hit the player!");
            player.GetComponent<PlayerMovement>().enabled = false;
            EndScreenUI.SetActive(true);
        }
    }*/
}
