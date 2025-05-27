using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseUI;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GoToSettings()
    {
        Debug.Log("go to settings");
    }
}
