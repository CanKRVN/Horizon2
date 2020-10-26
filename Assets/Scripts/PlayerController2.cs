using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerController2 : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    public float speed, forwardSpeed, tilt, distance;
    private int score;
    public static int highScore;
    private Quaternion calibrationQuaternion;
    public SimpleTouchPad touchPad;
    public Text distanceText, gameOverText,highScoreText,currentScore;
    public GameObject GameOverPanel, finishPanel;
    public GameObject ship;
    private bool isGameOver;
  
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        CalibrateAccelerometer();

        PlayerPrefs.GetInt("HighScore");
        
    }

    private void Update()
    {
        distance = transform.position.z * 0.2f;
        score = Convert.ToInt32(distance);
        distanceText.text = score.ToString();
    }

    void CalibrateAccelerometer()
    {
        Vector3 accelerationSnapshot = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);
        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    }

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
            rb.velocity = movement ;
        }
        Quaternion xRot = Quaternion.AngleAxis(-90f, transform.right);
        Quaternion yRot = Quaternion.AngleAxis(0f, transform.up);
        Quaternion zRot = Quaternion.AngleAxis(rb.velocity.x * tilt, transform.forward);
        if(direction.x != 0)
        {
            rb.rotation = zRot;
        }
        transform.right = new Vector3(0f,0f,0f);
        

       
        //rb.rotation = Quaternion.Euler(-90f, 0.0f, rb.velocity.x * tilt);
    }

    private void OnTriggerEnter(Collider other)
    {
      
        if (other.gameObject.CompareTag("Finish"))
        {
            isGameOver = true;
            finishPanel.SetActive(true);
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
            
            if(score > highScore)
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
