using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DateChoice : MonoBehaviour
{
   public void ChooseJin()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void ChooseDulla()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void ChooseScience()
    {
        SceneManager.LoadScene("Level 3");
    }
    
    
}
