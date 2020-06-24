using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{

    public GameObject PauseUI;
    //private Manager m;
    

    void Start()
    {
        //m = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();

    }

      
    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        Application.LoadLevel(1);
    }
    public void ResetHighscore()
    {
        PlayerPrefs.DeleteKey("Highscore");
        Application.LoadLevel(0);
    }
}

