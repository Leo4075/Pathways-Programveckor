using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public void PauseButton()
    {
        Time.timeScale = 0;
    }
    public void ResumeButton()
    {
        Time.timeScale = 1;
    }
    public void HomeButton()
    {
        SceneManager.LoadScene(0);
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ResumeButton();
    }
}
