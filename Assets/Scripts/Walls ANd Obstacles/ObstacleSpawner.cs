using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject Score_Multip_Obj;
    public List<GameObject> Obstacles;
    public static float ObstacleSpawnChance;

    private void Start()
    {
        RollForBonus();
        RollForObstacle();
    }
    public void RollForBonus()
    {
        float BRNG = (float)RNG_Custom.random.NextDouble();
        if (BRNG <= 0.1) { Score_Multip_Obj.SetActive(true); }
    }

    public void RollForObstacle()
    {
        if (RNG_Custom.random.NextDouble() <= ObstacleSpawnChance && Obstacles.Count > 0)
        {
            int RNG = RNG_Custom.random.Next(0, Obstacles.Count);
            GameObject selectedObstacle = Obstacles[RNG];

            PlaceObstacle(selectedObstacle);
        }
    }

    public void PlaceObstacle(GameObject obstacle)
    {
        obstacle.SetActive(true);
    }
}
