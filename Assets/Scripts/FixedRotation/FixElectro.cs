using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixElectro : MonoBehaviour
{

    void Update()
    {
        gameObject.transform.position =
            new Vector3(GameObject.FindGameObjectWithTag("Electro").transform.position.x + 0.1f,
                         GameObject.FindGameObjectWithTag("Electro").transform.position.y + 0.5f, 0);
    }

}
