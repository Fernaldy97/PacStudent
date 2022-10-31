using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixHydro : MonoBehaviour
{
    
    void Update()
    {
        gameObject.transform.position =
            new Vector3(GameObject.FindGameObjectWithTag("Hydro").transform.position.x + 0.1f,
                         GameObject.FindGameObjectWithTag("Hydro").transform.position.y + 0.5f, 0);
    }
}
