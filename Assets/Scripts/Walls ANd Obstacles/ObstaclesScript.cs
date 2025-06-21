using Unity.VisualScripting;
using UnityEngine;

public class ObstaclesScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Enemy_Mouvement.instance.gameObject.transform.position =new Vector3(Enemy_Mouvement.instance.gameObject.transform.position.x, Enemy_Mouvement.instance.gameObject.transform.position.y, Player_Manager.instance.transform.position.z - 6f);
            Destroy(this);
        } 
    }
}
