using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoop : MonoBehaviour
{
    public AudioClip IntroMusic;
    public AudioClip LoopMusic;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(playSound());
    }

    IEnumerator playSound()
    {
        GetComponent<AudioSource>().clip = IntroMusic;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(IntroMusic.length);

        GetComponent<AudioSource>().clip = LoopMusic;
        GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().loop = true;
    }
}
