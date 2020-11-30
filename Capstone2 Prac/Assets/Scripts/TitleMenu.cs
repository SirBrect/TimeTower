using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    //public Canvas title;
    public Button quitButton;
    public Button startButton;
    public Button options;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(){
        SceneManager.LoadScene("Intro", LoadSceneMode.Single);
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void ShowOptions(){
        
    }
}
