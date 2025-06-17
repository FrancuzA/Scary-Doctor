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
    public bool GameStarted = false;
    public float Timer = 3;
    public Animator PointsAnim;

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
        if(Timer > 0f)
        {
            Timer -= Time.deltaTime;
            if(Timer <= 0f) 
            {
                EndBonus();
            }
        }


        if (GameStarted)
        {
            Current_Points += (Point_Multiplier * Time.deltaTime);
            Score_UI.text = Mathf.RoundToInt(Current_Points).ToString();
        }

        if(Current_Points >= 1000)
        {
            ChooseCharacterMAnager.instance.UnlockGirl();

        }
    }

    public void CheckForHighScore()
    {
        if (PlayerPrefs.GetFloat("HighScore") <= Current_Points)
        {
            PlayerPrefs.SetFloat("HighScore", Mathf.RoundToInt(Current_Points));
        }
    }

    public void SaveLastPoints()
    {
        PlayerPrefs.SetFloat("LastGamePoint", Mathf.RoundToInt(Current_Points));
    }

    public void StartBonus()
    {
        PointsAnim.SetTrigger("GoOut");
        OnBonus = true;
        Timer = 3f;
        Point_Multiplier = 30;
    }

    public void EndBonus()
    {
        Point_Multiplier = 10;
        OnBonus = false;
        PointsAnim.SetTrigger("GoIn");
    }
}
