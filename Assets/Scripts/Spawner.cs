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

    private void Awake()
    {
        This_Wall.transform.parent = null;
    }

    private void FixedUpdate()
    {
        SpawningPoint = Spawn_Point.transform.position; 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PlayerEnteredCollider");
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
    

}
