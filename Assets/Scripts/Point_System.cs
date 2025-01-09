using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Point_System : MonoBehaviour
{
    public float Current_Points;
    public static int Point_Multiplier = 1;
    public TextMeshProUGUI Score_UI;
    public static bool OnBonus = false;
    void Start()
    {
        Point_Multiplier = 1;
        Current_Points = 0;
    }

    private void FixedUpdate()
    {
        Current_Points +=Point_Multiplier * Time.deltaTime;
        Score_UI.text = Mathf.RoundToInt(Current_Points).ToString();
    }
}
