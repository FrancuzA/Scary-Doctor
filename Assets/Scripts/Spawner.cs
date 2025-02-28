using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Wall_Empty;
    public GameObject Wall_Chairs;
    public GameObject Wall_Doors;
    public GameObject Spawn_Point;
    private Vector3 SpawningPoint;
    public GameObject terrain;
    public Quaternion _Rotation;
    public GameObject This_Wall;
    public GameObject Score_Multip_Obj;
    public LayerMask WhatIsWall;
    public GameObject SpawnChecker;
    public bool IsWallAlready;
    public List<GameObject> Obstacles;
    public float ObstacleSpawnChance;
    public int Difficulty;


    private void Awake()
    {
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
            SpawnSegment();
        }
    }

    public void SpawnSegment() 
    {
        float RNG = UnityEngine.Random.Range(0f, 10f);
        if (RNG <=4.5f) { Instantiate(Wall_Empty, SpawningPoint, _Rotation, terrain.transform); }
        if (RNG > 4.5f && RNG <= 9) { Instantiate(Wall_Chairs, SpawningPoint, _Rotation, terrain.transform); }
        if(RNG >9 && RNG <=10) { Instantiate(Wall_Doors, SpawningPoint, _Rotation, terrain.transform); }


        RollForObstacle();
    }
    
    public void RollForBonus()
    {
        float BRNG = UnityEngine.Random.value;
        if (BRNG <= 0.1) { Score_Multip_Obj.SetActive(true); Debug.Log("bonus spawned"); }
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
