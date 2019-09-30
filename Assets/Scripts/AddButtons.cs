﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButtons : MonoBehaviour
{
    [SerializeField]
    private Transform puzzleField;
    
    public int amountOfButtons = 20;

    [SerializeField]
    private GameObject btn;

    private void Awake()
    {
        for (int i = 0; i < amountOfButtons; i++)
        {
            GameObject button = Instantiate(btn);
            button.name = "" + i;
            button.transform.SetParent(puzzleField, false);
        }

    }
   
}
