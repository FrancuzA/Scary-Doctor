using System.Collections;
using UnityEngine;

public class Score_Multip : MonoBehaviour
{
    public GameObject shape;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Point_System.instance.OnBonus == false)
        {
            StartCoroutine(StartBonus());
        }

        if (other.CompareTag("Player") && Point_System.instance.OnBonus == true)
        {
            shape.SetActive(false);
        }
    }


    public IEnumerator StartBonus()
    {
        Point_System.instance.OnBonus = true;
        shape.SetActive(false) ;
        Point_System.instance.Point_Multiplier = 30;
        yield return new WaitForSeconds(5);
        Point_System.instance.Point_Multiplier = 10;
        Point_System.instance.OnBonus = false;
    }
}
