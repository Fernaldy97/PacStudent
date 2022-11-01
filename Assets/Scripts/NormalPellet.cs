using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPellet : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        //If collides with player
        if (other.gameObject.CompareTag("Player"))
        {
            //Get audio and play       
            GameObject.FindGameObjectWithTag("Normal").GetComponent<AudioSource>().Play();
            //Get score
            int count = int.Parse(GameObject.FindGameObjectWithTag("Score").GetComponent<UnityEngine.UI.Text>().text);
            //Add 1 score
            count++;
            string newCount= count.ToString();
            //Update the score
            GameObject.FindGameObjectWithTag("Score").GetComponent<UnityEngine.UI.Text>().text = newCount;
            

            gameObject.SetActive(false);
        }
    }
}
