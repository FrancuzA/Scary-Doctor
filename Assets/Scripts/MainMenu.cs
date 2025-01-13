using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Scoreboard;
    public void StartGame() 
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OpenScoreboard()
    {
        Scoreboard.SetActive(true);
    }

    public void CloseScoreboard()
    {
        Scoreboard.SetActive(false);
    }
}
