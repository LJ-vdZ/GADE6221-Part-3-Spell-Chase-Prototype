using UnityEngine;
using UnityEngine.SceneManagement;  //to change scenes
using UnityEngine.UI;
using TMPro;

public class MainMenuScreen : MonoBehaviour
{
    [SerializeField]
    //public TMP_InputField inputField;

    //public Button playButton;

    public void Start()
    {
        //playButton.interactable = false;

        ////ensure input field exists and add listener for text changes
        //if (inputField != null)
        //{
        //    inputField.onValueChanged.AddListener(OnNameInputChanged);

        //    //check starting input field state
        //    OnNameInputChanged(inputField.text);
        //}
    }

    public void StartGame() 
    {
         SceneManager.LoadScene("EnterPlayerInfo");
    }

    public void PlayGame() 
    {
        //reset game state
        if (GameManager.Instance != null) 
        {
            GameManager.Instance.restartGame();
        }

        //ensure death menu is disabled
        GameObject deathMenu = GameObject.Find("DeathMenuCanvas"); 

        if (deathMenu != null)
        {
            deathMenu.SetActive(false);
        }

        SceneManager.LoadScene("SampleScene");

    }

    public void ExitGame() 
    {
        Application.Quit();
        Debug.Log("Game Exited");
    }

    public void BackToMainMenu() 
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToScoreBoard() 
    {
        SceneManager.LoadScene("ScoreBoard");
    }


    //called when input field text changes
    //private void OnNameInputChanged(string inputText)
    //{
    //    //enable button if input is not empty
    //    if (playButton != null)
    //    {
    //        playButton.interactable = !string.IsNullOrWhiteSpace(inputText);    //ignore whitespace
    //    }
    //}

}
