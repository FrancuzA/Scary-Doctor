using System.Collections;
using UnityEngine;

public class Score_Multip : MonoBehaviour
{
    public GameObject shape;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Point_System.OnBonus == false)
        {
            StartCoroutine(StartBonus());
        }

        if (other.CompareTag("Player") && Point_System.OnBonus == true)
        {
            shape.SetActive(false);
        }
    }


    public IEnumerator StartBonus()
    {
        Point_System.OnBonus = true;
        shape.SetActive(false) ;
        Point_System.Point_Multiplier = 3;
        yield return new WaitForSeconds(5);
        Point_System.Point_Multiplier = 1;
        Point_System.OnBonus = false;
    }
}
