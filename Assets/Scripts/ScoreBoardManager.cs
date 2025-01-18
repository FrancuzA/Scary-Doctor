using UnityEngine;
using System.Collections.Generic;
using System.IO;
using TMPro;

public class ScoreBoardManager : MonoBehaviour
{
    public static ScoreBoardManager instance;

    public GameObject NewScoreLine; // Prefab for the score line (parent GameObject with child TextMeshProUGUI components)
    public GameObject ScoreBoard;  // The parent GameObject for the score lines

    private string filePath;

    // A list to store the serialized score data
    private List<ScoreLineData> scoreLines = new List<ScoreLineData>();

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            filePath = Application.persistentDataPath + "/scores.json";
            LoadScoresFromJson();
        }
        else
        {
            Destroy(gameObject);
        }
        AddNewScore(PlayerPrefs.GetString("NewScoreName"), PlayerPrefs.GetFloat("LastGamePoint"));
    }

    public void AddNewScore(string name, float points)
    {
        Debug.Log($"Adding new score: Name={name}, Points={points}");

        // Create a new GameObject in the scene
        GameObject newLine = Instantiate(NewScoreLine, ScoreBoard.transform);

        // Find the children where Name and Points are displayed
        Transform nameChild = newLine.transform.Find("ScoreName");   
        Transform pointsChild = newLine.transform.Find("Points"); 

        // Set the Name and Points in the UI
        if (nameChild != null && pointsChild != null)
        {
            TextMeshProUGUI nameText = nameChild.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI pointsText = pointsChild.GetComponent<TextMeshProUGUI>();

            if (nameText != null) nameText.text = name;
            if (pointsText != null) pointsText.text = points.ToString();
        }
        else
        {
            Debug.LogWarning("Name or Points child not found in ScoreLine prefab.");
        }

        // Serialize the score data and add it to the list
        ScoreLineData newScore = new ScoreLineData
        {
            name = name,
            points = (int)points
        };
        scoreLines.Add(newScore);

        // Save the updated list to JSON
        SaveScoresToJson();
    }

    public void LoadScoresFromJson()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            ScoreBoardData data = JsonUtility.FromJson<ScoreBoardData>(json);

            if (data != null && data.scores != null)
            {
                scoreLines = new List<ScoreLineData>(data.scores);

                // Populate the UI with the loaded scores
                foreach (ScoreLineData score in scoreLines)
                {
                    GameObject newLine = Instantiate(NewScoreLine, ScoreBoard.transform);

                    // Find the children where Name and Points are displayed
                    Transform nameChild = newLine.transform.Find("Name");   // Replace "Name" with the actual child name
                    Transform pointsChild = newLine.transform.Find("Points"); // Replace "Points" with the actual child name

                    if (nameChild != null && pointsChild != null)
                    {
                        TextMeshProUGUI nameText = nameChild.GetComponent<TextMeshProUGUI>();
                        TextMeshProUGUI pointsText = pointsChild.GetComponent<TextMeshProUGUI>();

                        if (nameText != null) nameText.text = score.name;
                        if (pointsText != null) pointsText.text = score.points.ToString();
                    }
                    else
                    {
                        Debug.LogWarning("Name or Points child not found in ScoreLine prefab.");
                    }
                }
            }
        }
        else
        {
            Debug.Log("No previous scores found.");
        }
    }

    public void SaveScoresToJson()
    {
        ScoreBoardData data = new ScoreBoardData { scores = scoreLines.ToArray() };
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
    }

    // A class to hold the data of a single score line
    [System.Serializable]
    private class ScoreLineData
    {
        public string name;  // The player's name
        public int points;   // The player's score
    }

    // A class to hold the list of all scores
    [System.Serializable]
    private class ScoreBoardData
    {
        public ScoreLineData[] scores;
    }
}
