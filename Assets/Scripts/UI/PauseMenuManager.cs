using FMODUnity;
using UnityEngine;
using FMOD.Studio;
public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseUI;
    public EventReference pauseSound;
    public EventInstance pauseSoundInstance;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            pauseUI.SetActive(true);
            Player_Manager.instance.PauseSounds();
            pauseSoundInstance = RuntimeManager.CreateInstance(pauseSound);
            pauseSoundInstance.start();
            pauseSoundInstance.release();
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
