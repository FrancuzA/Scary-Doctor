using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DeathScreen : MonoBehaviour
{
    public static DeathScreen instance;
    public GameObject ScoreSaving;
    public GameObject Choose;
    public GameObject InsertName;
    public TMP_InputField NameChosen;
    public bool WantToSave = false;
    public string NewScoreName;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        ScoreSaving.SetActive(true);
    }
    public void TryAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void StartSaving() 
    {
        Choose.SetActive(false);
        InsertName.SetActive(true);
    }

    public void SetNewName()
    {
        NewScoreName = NameChosen.text;
        PlayerPrefs.SetString("NewScoreName", NewScoreName);
        Point_System.instance.SaveLastPoints();
    }

    public void NoSaving()
    {
        ScoreSaving.SetActive(false);
    }
}
