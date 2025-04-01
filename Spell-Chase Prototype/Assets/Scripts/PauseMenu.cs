using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void Pause() //Brings up pause screen
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume() //Exits pause screen
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart() //Function to reload the game
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Quit() //Function to exit the game
    {
       Application.Quit();
    }
}
