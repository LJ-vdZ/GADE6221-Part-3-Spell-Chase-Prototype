using UnityEngine;
using UnityEngine.SceneManagement;  //to change scenes

public class MainMenuScreen : MonoBehaviour
{
    public void StartGame() 
    {
         SceneManager.LoadScene("EnterPlayerInfo");
    }

    public void PlayGame() 
    {
        SceneManager.LoadScene("SampleScene");
    }
}
