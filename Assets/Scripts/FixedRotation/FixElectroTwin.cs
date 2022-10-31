using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixElectroTwin : MonoBehaviour
{
   
    void Update()
    {
        gameObject.transform.position =
            new Vector3(GameObject.FindGameObjectWithTag("ElectroTwin").transform.position.x - 0.1f,
                         GameObject.FindGameObjectWithTag("ElectroTwin").transform.position.y + 0.5f, 0);
    }
}
