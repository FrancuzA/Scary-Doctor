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


    private void Awake()
    {
        This_Wall.transform.parent = null;
        RollForBonus();
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
        float RNG = Random.Range(0f, 10f);
        if (RNG <=4.5f) { Instantiate(Wall_Empty, SpawningPoint, _Rotation, terrain.transform); }
        if (RNG > 4.5f && RNG <= 9) { Instantiate(Wall_Chairs, SpawningPoint, _Rotation, terrain.transform); }
        if(RNG >9 && RNG <=10) { Instantiate(Wall_Doors, SpawningPoint, _Rotation, terrain.transform); }
    
    }
    
    public void RollForBonus()
    {
        float BRNG = Random.value;
        if (BRNG <= 0.1) { Score_Multip_Obj.SetActive(true); Debug.Log("bonus spawned"); }
    }

}
