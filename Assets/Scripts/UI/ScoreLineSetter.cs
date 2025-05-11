using UnityEngine;
using TMPro;
public class ScoreLineSetter : MonoBehaviour
{
    public TextMeshProUGUI NameUI;
    public TextMeshProUGUI PointsUI;
    public void Awake()
    {
       /* Debug.Log("setting new line info");
        NameUI.text = PlayerPrefs.GetString("NewScoreName");
        PointsUI.text = PlayerPrefs.GetFloat("LastGamePoints").ToString();*/
    }
}
