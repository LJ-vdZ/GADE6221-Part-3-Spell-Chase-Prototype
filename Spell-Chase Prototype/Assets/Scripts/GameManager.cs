using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : Death
{
    public GameObject EndScreenUI;
    public bool reset = false;
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Death.deathStatus == true) 
        {
            endGame();
        }
        if(reset == true)
        {
            EndScreenUI.SetActive(false);
            reset = false;
            deathStatus = false;
        }
    }

    public void endGame()
    {
        scoreText.enabled = false;
        EndScreenUI.SetActive(true); 
    }

    public void restartGame()
    {
        reset = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void quit()
    {
        Application.Quit();
    }

 
}
