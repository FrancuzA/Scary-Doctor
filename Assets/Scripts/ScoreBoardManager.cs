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
        if (IsDuplicate(name, points))
        {
            return;
        }

        // Serialize the score data and add it to the list
        ScoreLineData newScore = new ScoreLineData
        {
            name = name,
            points = (int)points
        };
        scoreLines.Add(newScore);

        // Create a new UI element for the score
        CreateScoreLineUI(name, points);

        // Save the updated list to JSON
        SaveScoresToJson();
    }

    private void CreateScoreLineUI(string name, float points)
    {
        GameObject newLine = Instantiate(NewScoreLine, ScoreBoard.transform);

        // Find the children where Name and Points are displayed
        Transform nameChild = newLine.transform.Find("ScoreName");   // Replace "Name" with the actual child name
        Transform pointsChild = newLine.transform.Find("Points"); // Replace "Points" with the actual child name

        if (nameChild != null && pointsChild != null)
        {
            TextMeshProUGUI nameText = nameChild.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI pointsText = pointsChild.GetComponent<TextMeshProUGUI>();

            if (nameText != null) nameText.text = name;
            if (pointsText != null) pointsText.text = points.ToString();
        }
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

                // Clear existing score lines in the UI
                foreach (Transform child in ScoreBoard.transform)
                {
                    Destroy(child.gameObject);
                }

                // Populate the UI with the loaded scores
                foreach (ScoreLineData score in scoreLines)
                {
                    CreateScoreLineUI(score.name, score.points);
                }
            }
        }
    }

    public void SaveScoresToJson()
    {
        ScoreBoardData data = new ScoreBoardData { scores = scoreLines.ToArray() };
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
    }

    private bool IsDuplicate(string name, float points)
    {
        // Check if a score with the same name and points already exists
        foreach (ScoreLineData score in scoreLines)
        {
            if (score.name == name && score.points == points)
            {
                return true;
            }
        }
        return false;
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

    public void EraseAllScores()
    {
        // Clear all children from the ScoreBoard (UI)
        foreach (Transform child in ScoreBoard.transform)
        {
            Destroy(child.gameObject);
        }

        // Overwrite the JSON file with an empty structure
        SaveEmptyScoresToJson();

    }

   
    private void SaveEmptyScoresToJson()
    {
        ScoreBoardData emptyData = new ScoreBoardData
        {
            scores = new ScoreLineData[0] // Empty array of scores
        };

        string json = JsonUtility.ToJson(emptyData, true);
        File.WriteAllText(filePath, json);
    }
}
