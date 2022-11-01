using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPellet: MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        //If collied with power pellet
        if (other.gameObject.CompareTag("Player"))
        {

            //Set all enemies to scared state
            GameObject.FindGameObjectWithTag("Hydro").GetComponent<Animator>().SetBool("Scared", true);
            GameObject.FindGameObjectWithTag("Electro").GetComponent<Animator>().SetBool("Scared", true);
            GameObject.FindGameObjectWithTag("ElectroTwin").GetComponent<Animator>().SetBool("Scared", true);
            GameObject.FindGameObjectWithTag("Pyro").GetComponent<Animator>().SetBool("Scared", true);

            //Begin coroutine
            StartCoroutine(myCoroutine());
            
            //Get and play scared bgm
            GameObject.FindGameObjectWithTag("ScaredBGM").GetComponent<AudioSource>().Play();

            //Move object to a place that can't be seen
            gameObject.transform.position = new Vector3(999, 999, 999);
        }
    }


    IEnumerator myCoroutine()
    {
        //Stop BGM
        GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>().Stop();

        //Start the countdown for scared ghost
        GameObject.FindGameObjectWithTag("ScaredTimer").GetComponent<UnityEngine.UI.Text>().enabled = true;
        GameObject.FindGameObjectWithTag("ScaredTimer").GetComponent<UnityEngine.UI.Text>().text = "10";
        yield return new WaitForSecondsRealtime(1f);
        GameObject.FindGameObjectWithTag("ScaredTimer").GetComponent<UnityEngine.UI.Text>().text = "9";
        yield return new WaitForSecondsRealtime(1f);
        GameObject.FindGameObjectWithTag("ScaredTimer").GetComponent<UnityEngine.UI.Text>().text = "8";
        yield return new WaitForSecondsRealtime(1f);
        GameObject.FindGameObjectWithTag("ScaredTimer").GetComponent<UnityEngine.UI.Text>().text = "7";
        yield return new WaitForSecondsRealtime(1f);
        GameObject.FindGameObjectWithTag("ScaredTimer").GetComponent<UnityEngine.UI.Text>().text = "6";
        yield return new WaitForSecondsRealtime(1f);
        GameObject.FindGameObjectWithTag("ScaredTimer").GetComponent<UnityEngine.UI.Text>().text = "5";
        yield return new WaitForSecondsRealtime(1f);
        GameObject.FindGameObjectWithTag("ScaredTimer").GetComponent<UnityEngine.UI.Text>().text = "4";
        yield return new WaitForSecondsRealtime(1f);
        GameObject.FindGameObjectWithTag("ScaredTimer").GetComponent<UnityEngine.UI.Text>().text = "3";
        yield return new WaitForSecondsRealtime(1f);
        GameObject.FindGameObjectWithTag("ScaredTimer").GetComponent<UnityEngine.UI.Text>().text = "2";
        yield return new WaitForSecondsRealtime(1f);
        GameObject.FindGameObjectWithTag("ScaredTimer").GetComponent<UnityEngine.UI.Text>().text = "1";
        yield return new WaitForSecondsRealtime(1f);

        //Ghost back to normal state
        GameObject.FindGameObjectWithTag("Hydro").GetComponent<Animator>().SetBool("Scared", false);
        GameObject.FindGameObjectWithTag("Electro").GetComponent<Animator>().SetBool("Scared", false);
        GameObject.FindGameObjectWithTag("ElectroTwin").GetComponent<Animator>().SetBool("Scared", false);
        GameObject.FindGameObjectWithTag("Pyro").GetComponent<Animator>().SetBool("Scared", false);

        //Countdown set to false
        GameObject.FindGameObjectWithTag("ScaredTimer").GetComponent<UnityEngine.UI.Text>().enabled = false;
        GameObject.FindGameObjectWithTag("ScaredBGM").GetComponent<AudioSource>().Stop();

        //Resume BGM like normal
        GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>().Play();


    }
}
