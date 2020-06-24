using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{

    public GameObject PauseUI;
    private Manager m;

    private bool paused = false;

    void Start()
    {
        m = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        PauseUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
        }

        if (paused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;
        }

        if(!paused)

        {
            PauseUI.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void TouchPause(bool x)
    {
        if (x)
        {
            Debug.Log("DAJE JEDEN DO PAUSY");
            paused = true;
            Time.timeScale = 0;
        }
    }

    public void Resume()
    {

        paused = false;
    }

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void MainMenu()
    {
        if (PlayerPrefs.HasKey("Highscore"))
        {
            if (PlayerPrefs.GetInt("Highscore") < m.score)
            {
                PlayerPrefs.SetInt("Highscore", m.score);
            }
        }
        else
        {
            PlayerPrefs.SetInt("Highscore", m.score);
        }

        SaveScore();
        Application.LoadLevel(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

   
    void SaveScore()
    {
        PlayerPrefs.SetInt("Score", m.score);
    }

}

