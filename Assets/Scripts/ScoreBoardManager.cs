using UnityEngine;

public class ScoreBoardManager : MonoBehaviour
{
    public GameObject NewScoreLine;
    public GameObject ScoreBoard;
    public GameObject MainMenuCanva;
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void AddNewScore() 
    {
        GameObject NewLine = Instantiate(NewScoreLine,ScoreBoard.transform);


    }

    public void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape)) 
        {
            ScoreBoard.SetActive(!ScoreBoard.activeInHierarchy);
            MainMenuCanva.SetActive(!MainMenuCanva.activeInHierarchy);
        }
    }
}
