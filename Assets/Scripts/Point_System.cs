using TMPro;
using UnityEngine;

public class Point_System : MonoBehaviour
{
    public static Point_System instance;

    public int HighScore;
    public int LastGamePoint;
    public float Current_Points;
    public int Point_Multiplier = 1;
    public TextMeshProUGUI Score_UI;
    public bool OnBonus = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        Point_Multiplier = 10;
        Current_Points = 0;
    }

    private void Update()
    {
        Debug.Log("addin points " + " current points: " + Current_Points + " Multiplier: " + Point_Multiplier + " time delta timme?: " + Time.deltaTime + " should add " + ( Point_Multiplier * ((int)Time.deltaTime)) + " to print on screen: " + Mathf.RoundToInt(Current_Points).ToString());
        Current_Points += (Point_Multiplier * Time.deltaTime);
        Score_UI.text = Mathf.RoundToInt(Current_Points).ToString();
        if(HighScore <= Current_Points) 
        {
            PlayerPrefs.SetFloat("HighScore", Current_Points);
        }
    }

    public void SaveLastPoints()
    {
        PlayerPrefs.SetFloat("LastGamePoint", Current_Points);
    }
}
