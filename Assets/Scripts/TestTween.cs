using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestTween : MonoBehaviour
{
    public float duration;
    public Transform pacMan;
    public Vector3[] path;
    public PathType pathType;
    public PathMode pathMode;

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(playMovement());
    }

    IEnumerator playMovement()
    {
        while (true)
        {
            yield return new WaitForSeconds(7.5f);
            pacMan.DOPath(path, duration, pathType, pathMode, 10);
        }

    }
}
