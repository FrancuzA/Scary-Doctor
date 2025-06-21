using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    
    private Vector3 SpawningPoint;
    
    private List<GameObject> CurrentWalls;
    public List<GameObject> WallsLvl1;
    public List<GameObject> WallsLvl2;  
    private int Difficulty;
    private bool LVLChanged= false;

    private void Awake()
    {
        RNG_Custom.Init(-1);
    }
    private void Start()
    {
        SpawningPoint = new Vector3(0, 0, 14);
        CurrentWalls = WallsLvl1;
        Difficulty = PlayerPrefs.GetInt("DifficultyLvl");
        switch (Difficulty) { 
            case 0: ObstacleSpawner.ObstacleSpawnChance = 0.5f;
                break;
            case 1:
                ObstacleSpawner.ObstacleSpawnChance = 0.75f;
                break;
            case 2:
                ObstacleSpawner.ObstacleSpawnChance = 0.95f;
                break;

        }
    }
    public void SpawnSegment() 
    {
        if (Point_System.instance.Current_Points >= 2000 && LVLChanged == false)
        {
            CurrentWalls.Clear();
            CurrentWalls = WallsLvl2;
            MusicLVL.instance.SetLVL2();
            LVLChanged = true;
        }
        int RNG = RNG_Custom.random.Next(0, CurrentWalls.Count);
        GameObject SelectecWall = CurrentWalls[RNG];
        Instantiate(SelectecWall, SpawningPoint,Quaternion.identity);
        SpawningPoint += new Vector3(0, 0, 7);
    }
    
   
}
