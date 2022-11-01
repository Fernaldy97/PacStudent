using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PacStudentController : MonoBehaviour
{

    private List<GameObject> itemList;
    private LevelGenerator levelGenerator;
    int[,] designLevel;

    GameObject pacStudent;

    private Vector3 movement;
    private float movementSqrMagnitude;
    public float walkSpeed = 1.5f;

    public AudioSource movementAudio;
    Vector3 tempPosition;
    Vector3 previousPosition;
    ParticleSystem theParticles;
    public bool isEnabled = true;
    bool gameOver = false;

    void Awake()
    {
        int[,] designLevel = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<LevelGenerator>().designLevel;
        GameObject pacStudent = GameObject.FindGameObjectWithTag("Player");

        //PacStudent top left starting position
        Vector3 initialPosition = new Vector3(-13f + 0.5f, 18f - 0.5f, 0);
        pacStudent.transform.position = initialPosition;
    }

    void Start()
    {
        levelGenerator = GetComponent<LevelGenerator>();

        StartCoroutine(MyCoroutine());

    }

    void Update()
    {
        //Display timer
        if (!gameOver)
            GameObject.FindGameObjectWithTag("Timer").GetComponent<UnityEngine.UI.Text>().text = FormatTime(Time.timeSinceLevelLoad - 4f).ToString();

        Animator animation = gameObject.GetComponent<Animator>();
        int[,] designLevel = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<LevelGenerator>().designLevel;

        //Warping
        if (gameObject.transform.position.x < -14.5)
        {
            float originalY = gameObject.transform.position.y;
            gameObject.transform.position = new Vector3(-14.5f + designLevel.GetUpperBound(1) * 1f * 2, originalY, 0);
        }

        //Warping
        if (gameObject.transform.position.x > -14.5f + designLevel.GetUpperBound(0) * 1f * 2)
        {
            float originalY = gameObject.transform.position.y;
            gameObject.transform.position = new Vector3(-14.5f, originalY, 0);
        }

        //Pac go up
        if (Input.GetKeyDown(KeyCode.W) && isEnabled)
        {
            animation.SetTrigger("Up");
            animation.ResetTrigger("Right");
            animation.ResetTrigger("Left");
            animation.ResetTrigger("Down");
        }

            //Pac go left
            if (Input.GetKeyDown(KeyCode.A) && isEnabled)
            {
                animation.SetTrigger("Left");
                animation.ResetTrigger("Right");
                animation.ResetTrigger("Up");
                animation.ResetTrigger("Down");
            }

                //Pac go right
                if (Input.GetKeyDown(KeyCode.D) && isEnabled)
                {
                    animation.SetTrigger("Right");
                    animation.ResetTrigger("Left");
                    animation.ResetTrigger("Up");
                    animation.ResetTrigger("Down");
                }

                    //Pac go down
                    if (Input.GetKeyDown(KeyCode.S) && isEnabled)
                    {
                        animation.SetTrigger("Down");
                        animation.ResetTrigger("Right");
                        animation.ResetTrigger("Left");
                        animation.ResetTrigger("Up");
                    }                
            
        //Enable pacStudent movement
        GetMovementVector();
        CharacterPostion();


        //Play movement audio
        if (!gameOver)
            pacStudentMovementAudio();

        //Game over
        if (GameObject.FindGameObjectWithTag("Lives").GetComponent<UnityEngine.UI.Text>().text == "0" ||
            GameObject.FindGameObjectWithTag("Timer").GetComponent<UnityEngine.UI.Text>().text == "99:99:99" ||
            //Code below doesn't work due to missing tag for both normal and power, didn't have time to set up the pellets
            (GameObject.FindGameObjectsWithTag("Normal").Length == 1 && GameObject.FindGameObjectsWithTag("Power").Length == 1))
        {
            //GameOver Coroutine
            if (!gameOver)
            {

                StartCoroutine(GameOverCoroutine());

                //use bool value to check to avoid the coroutine being called indefinately
                gameOver = true;

            }
        }
    }

    void GetMovementVector()
    {
        //Up input
        if (Input.GetAxis("Vertical") > 0 && isEnabled)
        {
            movement.y++;
        }

        //Left input
        if (Input.GetAxis("Horizontal") < 0 && isEnabled)
        {
            movement.x--;
        }

        //Down input
        if (Input.GetAxis("Vertical") < 0 && isEnabled)
        {
            movement.y--;
        }

        //Right input
        if (Input.GetAxis("Horizontal") > 0 && isEnabled)
        {
            movement.x++;
        }

        movement = Vector3.ClampMagnitude(movement, 1.0f);
        movementSqrMagnitude = movement.sqrMagnitude;
    }


    void CharacterPostion()
    {
        transform.Translate(movement * walkSpeed * Time.deltaTime, Space.World);
    }

    void pacStudentMovementAudio()
    {
        //Get audio
        AudioSource movementAudio = gameObject.GetComponent<AudioSource>();
        ParticleSystem theParticles = gameObject.GetComponent<ParticleSystem>();

        //If pacStudent move
        if (tempPosition.x - previousPosition.x > 0.1 || tempPosition.y - previousPosition.y > 0.1)
        {
            //Play particles
            theParticles.Play();
            //Play movement audio
            if (!movementAudio.isPlaying)
            {
                movementAudio.Play();
            }

           
        }

        //If pacStudent is not moving
        else if (tempPosition.x - previousPosition.x < 0.1 && tempPosition.y - previousPosition.y < 0.1)
        {

            //Stop particles
            theParticles.Stop();
            //Stop movement audio
            if (movementAudio.isPlaying)
            {
                movementAudio.Stop();
            }

        }
    }

    //Check if pacStudent is moving or not
    IEnumerator MyCoroutine()
    {
        marker:
        tempPosition = gameObject.transform.position;
        yield return new WaitForSecondsRealtime(0.5f);
        previousPosition = gameObject.transform.position;
        yield return new WaitForSecondsRealtime(0.5f);
        goto marker;
    }

    //Formatting the time value
    string FormatTime(float time)
    {
        int intTime = (int)time;
        int minutes = intTime / 60;
        int seconds = intTime % 60;
        float fraction = time * 1000;
        fraction = (fraction % 1000);
        string gameTime = String.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction);
        return gameTime;
    }

    //Game Over
    IEnumerator GameOverCoroutine()
    {
        //Stop BGM from playing
        GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>().enabled = false;

        //No more lives remaining
        if (GameObject.FindGameObjectWithTag("Lives").GetComponent<UnityEngine.UI.Text>().text == "0")
        {
            //Game Over text
            GameObject.FindGameObjectWithTag("Timer").GetComponent<UnityEngine.UI.Text>().text = "Game Over";
        }

        //When there is no more pellets
        else
        {
            //Victory
            GameObject.FindGameObjectWithTag("Timer").GetComponent<UnityEngine.UI.Text>().text = "Victory";
        }

        yield return new WaitForSeconds(3f);
        SceneManager.LoadSceneAsync("StartScene", LoadSceneMode.Single);
    }
}
