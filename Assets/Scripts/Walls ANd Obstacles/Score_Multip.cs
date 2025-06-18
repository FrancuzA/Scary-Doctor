using System.Collections;
using UnityEngine;

public class Score_Multip : MonoBehaviour
{
    public GameObject shape;
    public GameObject pickUpVFX;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Point_System.instance.OnBonus == false)
        {
            pickUpVFX.SetActive(true);
            shape.SetActive(false);
            Point_System.instance.StartBonus();
        }

        if (other.CompareTag("Player") && Point_System.instance.OnBonus == true)
        {
            pickUpVFX.SetActive(true);
            shape.SetActive(false);
            Point_System.instance.Timer += 1f;
        }
    }
}
