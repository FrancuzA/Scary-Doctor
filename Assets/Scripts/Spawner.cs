using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Wall_1;
    public GameObject Spawn_Point;
    private Vector3 SpawningPoint;
    public GameObject terrain;
    public Quaternion _Rotation;

    private void FixedUpdate()
    {
        SpawningPoint = Spawn_Point.transform.position;
        Debug.Log(SpawningPoint);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Instantiate(Wall_1,SpawningPoint,_Rotation ,terrain.transform);
            Debug.Log("Spawning Wall at "+SpawningPoint);
        }
    }
}
