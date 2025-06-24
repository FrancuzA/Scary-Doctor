using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseUI;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseUI.SetActive(true);
            Player_Manager.instance.PauseSounds();
            Time.timeScale = 0f;
        }
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        Player_Manager.instance.UnPauseSounds();
        Time.timeScale = 1f;
    }

    public void GoToSettings()
    {
        Debug.Log("go to settings");
    }
}
