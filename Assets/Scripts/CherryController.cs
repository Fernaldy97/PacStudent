using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(MyCoroutine());
    }

    IEnumerator MyCoroutine()
    {  
        //Wait for 15 seconds to align with 10 seconds spawn time
        yield return new WaitForSecondsRealtime(15f);
        
        marker:

        //Create cherry
        GameObject cherry = (GameObject)Instantiate(Resources.Load("BonusCherry"));
        int[,] designLevel = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<LevelGenerator>().designLevel;
       
        //Randomly generate new cherry
        cherry.transform.position = new Vector3(Random.Range(-13f, -1f + designLevel.GetUpperBound(1) * 1f), 18.5f + 1, 0f);

        //Wait for 10 seconds
        yield return new WaitForSecondsRealtime(10f);

        goto marker;
    }
}
