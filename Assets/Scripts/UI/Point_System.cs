using TMPro;
using UnityEngine;


public class Point_System : MonoBehaviour
{
    public static Point_System instance;
    public int HighScore;
    public int LastGamePoint;
    public float BonusPoints;
    public float Current_Points;
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
        Current_Points = 0;
    }

    private void FixedUpdate()
    {
        if (Timer > 0f)
        {
            Timer -= Time.deltaTime;
            BonusPoints += 0.144f;
            if (Timer <= 0f)
            {

                EndBonus();
            }
        }
    }

    private void Update()
    {
        if (GameStarted)
        {
            Current_Points = Player_Manager.instance.PlayerRigidbody.position.z + 7 + BonusPoints;
            Score_UI.text = Mathf.RoundToInt(Current_Points).ToString();
        }

        if(Current_Points >= 20)
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
    }

    public void EndBonus()
    {
        OnBonus = false;
        PointsAnim.SetTrigger("GoIn");
    }
}
