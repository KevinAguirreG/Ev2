using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public event Action OnEnter;

    public void HandleUpdate()
    {
        HandleEnterTheGame();
    }

    void HandleEnterTheGame()
    {

        if (Input.GetAxisRaw("Submit") != 0)
        {
            OnEnter();
        }
    }
}
