using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuController : MonoBehaviour
{
    
    void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            GameManager.Instance.StartGame();
        }
    }
}
