using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void gotoScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
    public void EndGame()
    {
        Application.Quit();
    }
}
