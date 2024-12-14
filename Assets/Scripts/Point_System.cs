using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Point_System : MonoBehaviour
{
    public float Current_Points;
    public int Point_Multiplier = 1;
    public TextMeshProUGUI Score_UI;
    void Start()
    {
        Current_Points = 0;
    }

    private void FixedUpdate()
    {
        Current_Points +=Point_Multiplier * Time.deltaTime;
        Score_UI.text = Mathf.RoundToInt(Current_Points).ToString();
    }
}
