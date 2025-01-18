using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuUI;
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
        MainMenuUI.SetActive(false);
        Scoreboard.SetActive(true);
    }

    public void CloseScoreboard()
    {
        MainMenuUI.SetActive(true);
        Scoreboard.SetActive(false);
    }
}
