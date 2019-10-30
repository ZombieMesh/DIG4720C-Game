using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DateChoice : MonoBehaviour
{
   public void ChooseJin()
    {
        SceneManager.LoadScene("JinSoyooNarrativeScene");
    }

    public void ChooseIleana()
    {
        SceneManager.LoadScene("IleanaNarrativeScene");
    }

    public void ChooseDulla()
    {
        SceneManager.LoadScene("DullaNarrativeScene");
    }

    public void ChooseScience()
    {
        SceneManager.LoadScene("Level 3");
    }
    
    
}
