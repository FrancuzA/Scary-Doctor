using TMPro;
using UnityEngine;

public class Point_System : MonoBehaviour
{
    public static Point_System instance;

    public int HighScore;
    public int LastGamePoint;
    public int Current_Points;
    public static int Point_Multiplier = 1;
    public TextMeshProUGUI Score_UI;
    public static bool OnBonus = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        Point_Multiplier = 1;
        Current_Points = 0;
    }

    private void FixedUpdate()
    {
        Current_Points +=Point_Multiplier * ((int)Time.deltaTime);
        Score_UI.text = Mathf.RoundToInt(Current_Points).ToString();
        if(HighScore <= Current_Points) 
        {
            PlayerPrefs.SetInt("HighScore", Current_Points);
        }
    }

    public void SaveLastPoints()
    {
        PlayerPrefs.SetInt("LastGamePoint", Current_Points);
    }
}
