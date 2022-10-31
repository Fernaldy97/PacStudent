using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public bool isCountingDown = false;
    public int[,] designLevel =
        {
            {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
            {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
            {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
            {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
            {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
            {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
            {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
            {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
            {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
            {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
            {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
            {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
            {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
            {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
            {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
         };

    //GameStart = false
    public bool gameStart = false;

    void Awake()
    {
        //Disable player control before countdown
        GameObject.FindGameObjectWithTag("Player").GetComponent<PacStudentController>().enabled = false;
        //Start the coroutine for countdown, pacStudent movement and BGM
        StartCoroutine(beginCoroutine());
    }

    void Start()
    {
        //Disable manual map
        GameObject.Find("Grid").SetActive(false);

        //Load Prefabs and Empty
        GameObject normalPellet = (GameObject)Instantiate(Resources.Load("NormalPellet"));
        GameObject powerPellet = (GameObject)Instantiate(Resources.Load("PowerPellet"));
        GameObject outsideWall = (GameObject)Instantiate(Resources.Load("OutsideWall"));
        GameObject nullObject = (GameObject)Instantiate(Resources.Load("Empty"));

        //Initial coordinates
        float initialX = -5.5f;
        float initialY = 5.5f;

        //Generate Map
        for (int firstLoop = 0; firstLoop <= designLevel.GetUpperBound(0); firstLoop++)
        {
            for (int secondLoop = 0; secondLoop <= designLevel.GetUpperBound(1); secondLoop++) 
            {
                //Increment the x coordinate by 0.5 and subtract the y coordinate by 0.5
                Instantiate(LevelDesign(designLevel[firstLoop, secondLoop]),
                    new Vector3(initialX + secondLoop * 0.5f, initialY - firstLoop * 0.5f, 0), Quaternion.identity);
            }
        }

        //Creation of top right map
        for (int firstLoop = 0; firstLoop <= designLevel.GetUpperBound(0); firstLoop++)
        {
            for (int secondLoop = designLevel.GetUpperBound(1); secondLoop >= 0; secondLoop--)
            {
                Instantiate(LevelDesign(designLevel[firstLoop, secondLoop]),
                    new Vector3(initialX - (secondLoop - designLevel.GetUpperBound(1)) * 0.5f + (designLevel.GetUpperBound(1) + 1) * 0.5f, 
                                initialY - firstLoop * 0.5f, 0), Quaternion.identity);
            }
        }

        //Bottom left
        for (int firstLoop = designLevel.GetUpperBound(0) - 1; firstLoop >= 0; firstLoop--)
        {
            for (int secondLoop = 0; secondLoop <= designLevel.GetUpperBound(1); secondLoop++)
            {
                Instantiate(LevelDesign(designLevel[firstLoop, secondLoop]),
                    new Vector3(initialX + secondLoop * 0.5f, initialY + ((firstLoop + 1f) - designLevel.GetUpperBound(0)) * 0.5f - (designLevel.GetUpperBound(0) + 1) * 0.5f, 0), Quaternion.identity);
            }
        }

        //Bottom Right
        for (int firstLoop = designLevel.GetUpperBound(0) - 1; firstLoop >= 0; firstLoop--)
        {
            for (int secondLoop = designLevel.GetUpperBound(1); secondLoop >= 0; secondLoop--) 
            {
                Instantiate(LevelDesign(designLevel[firstLoop, secondLoop]),
                    new Vector3(initialX - (secondLoop - designLevel.GetUpperBound(1)) * 0.5f + (designLevel.GetUpperBound(1) + 1) * 0.5f, 
                                initialY + ((firstLoop + 1f) - designLevel.GetUpperBound(0)) * 0.5f - (designLevel.GetUpperBound(0) + 1) * 0.5f, 0),  
                                Quaternion.identity);
            }
        }

        //Return game object
        GameObject LevelDesign(int designInt)
        {
            switch (designInt)
            {
                case 0:
                    return nullObject;
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    return normalPellet;
                case 6:
                    return powerPellet;
                case 7:
                    return outsideWall;
                default:
                    return nullObject;
            }
        }
    }

    //Coroutine
    IEnumerator beginCoroutine()
    {
        GameObject.FindGameObjectWithTag("Timer").GetComponent<UnityEngine.UI.Text>().text = "3";
        yield return new WaitForSeconds(1f);
        GameObject.FindGameObjectWithTag("Timer").GetComponent<UnityEngine.UI.Text>().text = "2";
        yield return new WaitForSeconds(1f);
        GameObject.FindGameObjectWithTag("Timer").GetComponent<UnityEngine.UI.Text>().text = "1";
        yield return new WaitForSeconds(1f);
        GameObject.FindGameObjectWithTag("Timer").GetComponent<UnityEngine.UI.Text>().text = "GO!";
        yield return new WaitForSeconds(1f);

        //Once countdown is over, pacStudent can move
        GameObject.FindGameObjectWithTag("Player").GetComponent<PacStudentController>().enabled = true;

        //Play BGM
        GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>().Play();

        //Game start = true
        gameStart = true;

    }



}
