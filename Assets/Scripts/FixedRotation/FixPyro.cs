using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPyro : MonoBehaviour
{

    void Update()
    {
        gameObject.transform.position =
            new Vector3(GameObject.FindGameObjectWithTag("Pyro").transform.position.x + 0.1f,
                         GameObject.FindGameObjectWithTag("Pyro").transform.position.y + 0.5f, 0);
    }
}
