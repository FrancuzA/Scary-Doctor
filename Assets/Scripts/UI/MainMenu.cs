using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuUI;
    public GameObject Scoreboard;
    public GameObject CreditsUI;
    public GameObject DifficultyUI;
    public void StartGame() 
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OpenCloseScoreboard()
    {
        MainMenuUI.SetActive(!MainMenuUI.activeInHierarchy);
        Scoreboard.SetActive(!Scoreboard.activeInHierarchy);
    }

    public void OpenCloseCredits()
    {
        CreditsUI.SetActive(!CreditsUI.activeInHierarchy);
    }

    public void ChooseDifficulty() 
    {
        DifficultyUI.SetActive(!DifficultyUI.activeInHierarchy);
    }

    public void SetDifficulty(int Lvl)
    {
        PlayerPrefs.SetInt("DifficultyLvl", Lvl);
        StartGame();
    }
}
