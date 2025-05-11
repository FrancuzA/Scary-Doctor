using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    public GameObject Spawn_Point;
    public GameObject terrain;
    public GameObject This_Wall;
    public GameObject Score_Multip_Obj;
    public GameObject SpawnChecker;
    private Vector3 SpawningPoint;
    private Quaternion _Rotation;
    public LayerMask WhatIsWall;
    private bool IsWallAlready;
    public List<GameObject> Obstacles;
    private List<GameObject> CurrentWalls;
    public List<GameObject> WallsLvl1;
    public List<GameObject> WallsLvl2;  
    private float ObstacleSpawnChance;
    private int Difficulty;


    private void Awake()
    {
        CurrentWalls = WallsLvl1;
        This_Wall.transform.parent = null;
        RollForBonus();
        Difficulty = PlayerPrefs.GetInt("DifficultyLvl");
        switch (Difficulty) { 
            case 0: ObstacleSpawnChance = 0.3f;
                break;
            case 1: ObstacleSpawnChance = 0.45f;
                break;
            case 2: ObstacleSpawnChance = 0.7f;
                break;

        }
    }

    private void FixedUpdate()
    {
        SpawningPoint = Spawn_Point.transform.position; 
        IsWallAlready = Physics.Raycast(SpawnChecker.transform.position, Vector3.down,2f, WhatIsWall);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Spawner") && IsWallAlready==false)
        {
            if(Point_System.instance.Current_Points >= 2000)
            {
                CurrentWalls.Clear();
                CurrentWalls = WallsLvl2;
            }

            SpawnSegment();
        }
    }

    public void SpawnSegment() 
    {
        Debug.Log("ile w liscie " + CurrentWalls.Count);
        int RNG = UnityEngine.Random.Range(0, CurrentWalls.Count);
        Debug.Log("wylososwano " + RNG);
        GameObject SelectecWall = CurrentWalls[RNG];
        Instantiate(SelectecWall, SpawningPoint, _Rotation, terrain.transform);
        RollForObstacle();
    }
    
    public void RollForBonus()
    {
        float BRNG = UnityEngine.Random.value;
        if (BRNG <= 0.1) { Score_Multip_Obj.SetActive(true);}
    }

    public void RollForObstacle()
    {
        if (UnityEngine.Random.value <= ObstacleSpawnChance && Obstacles.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, Obstacles.Count);
            GameObject selectedObstacle = Obstacles[randomIndex];

           PlaceObstacle(selectedObstacle);
        }
    }

    public void PlaceObstacle(GameObject obstacle)
    {
        obstacle.SetActive(true);
    }
}
