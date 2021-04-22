using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionMenu : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    
    }
    public void Options()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);

    }
    public void Controls()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);

    }
    public void Quit()
    {
        Application.Quit();

    }
}
