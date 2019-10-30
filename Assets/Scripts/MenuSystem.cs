using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Game Quits");
        Application.Quit();
    }

    public void NewGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("intro");
    }
}
