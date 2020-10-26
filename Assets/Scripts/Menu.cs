using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
 
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
        
    }

    
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");

    }
  
 
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
        Debug.Log("OMG! You have started the game!!!");
    }
}
