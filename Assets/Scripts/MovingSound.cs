using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSound : MonoBehaviour
{
    public AudioClip moveSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(playSoundEffect());
    }

    IEnumerator playSoundEffect()
    {
        while (true)
        {
           
            GetComponent<AudioSource>().clip = moveSoundEffect;
            GetComponent<AudioSource>().Pause();
            yield return new WaitForSeconds(7.5f);
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(4.8f);


        }
    }
}
