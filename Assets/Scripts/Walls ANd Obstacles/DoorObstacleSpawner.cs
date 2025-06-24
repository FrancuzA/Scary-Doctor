using System.Collections.Generic;
using UnityEngine;

public class DoorObstacleSpawner : MonoBehaviour
{
    public GameObject Score_Multip_Obj;
    public List<GameObject> Obstacles;
    public static float ObstacleSpawnChance;
    public Animator DoorAnim;
    private int JumpscareInt;
    public GameObject JumpScare;
    public GameObject BackRoom;
    private void Start()
    {
        JumpscareInt = RNG_Custom.random.Next(0, 3);
        RollForBonus();
        if (JumpscareInt == 0) return;
        RollForObstacle();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (JumpscareInt == 0)
            {
                DoorAnim.SetBool("JumpScare", true);
                int RNG = RNG_Custom.random.Next(0, 2);
                if(RNG == 0)
                {
                    JumpScare.SetActive(true);
                }
                else
                {
                    BackRoom.SetActive(true);
                }

                
            }
        }
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
