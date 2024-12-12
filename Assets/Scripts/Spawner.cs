using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Wall_1;
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
            Instantiate(Wall_1,SpawningPoint,_Rotation ,terrain.transform);
        }
    }
}
