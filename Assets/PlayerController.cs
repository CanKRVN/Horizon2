using System;
using System.Collections;
using System.Collections.Generic;
using UltimateCameraController.Cameras.Controllers;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    public float speed, forwardSpeed, tilt, distance;
    private int score;
    public static int highScore;
    private Quaternion calibrationQuaternion;
    public SimpleTouchPad touchPad;
    public Text distanceText, gameOverText, highScoreText, currentScore;
    public GameObject GameOverPanel, finishPanel;
    public GameObject ship;
    private bool isGameOver;
    

    public Animator animatorRed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        CalibrateAccelerometer();

        PlayerPrefs.GetInt("HighScore");
    }

    private void Update()
    {
        distance = transform.position.z * 0.2f + 3f;
        score = Convert.ToInt32(distance);
        distanceText.text = score.ToString();
    }


    void CalibrateAccelerometer()
    {
        Vector3 accelerationSnapshot = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);
        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    }

    //Get the 'calibrated' value from the Input
    Vector3 FixAcceleration(Vector3 acceleration)
    {
        Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
        return fixedAcceleration;
    }

    private void FixedUpdate()
    {
        Vector2 direction = touchPad.GetDirection();

        Vector3 movement = new Vector3(direction.x * speed, 0.0f, forwardSpeed);

        if (SimpleTouchPad.isTouchStat || isGameOver)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            rb.velocity = movement;
        }
        rb.rotation = Quaternion.Euler(0f, 0.0f, rb.velocity.x * tilt);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Finish"))
        {
            
            finishPanel.SetActive(true);
            Debug.Log("you did!");
            Destroy(Camera.main.GetComponent<CameraController>());
        }

       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(ship);
            Debug.Log(collision.gameObject.name);


            GameOverPanel.SetActive(true);
            isGameOver = true;

            if (score > highScore)
            {
                Debug.Log("HİGH SCORE!");
                highScore = score;

                PlayerPrefs.SetInt("HighScore", highScore);
                PlayerPrefs.Save();
            }

            highScoreText.text = "BEST: " + highScore;
            currentScore.text = "SCORE: " + score;
        }
    }

}
