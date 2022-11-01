using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryBonus : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //If collides with player
        if (other.gameObject.CompareTag("Player"))
        {
            //Get score
            int count = int.Parse(GameObject.FindGameObjectWithTag("Score").GetComponent<UnityEngine.UI.Text>().text);
            //Add 100 points
            count = count + 100;
            string newCount = count.ToString();
            //update the score string
            GameObject.FindGameObjectWithTag("Score").GetComponent<UnityEngine.UI.Text>().text = newCount;

            gameObject.SetActive(false);

        }
    }
}
