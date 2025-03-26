using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    // theres alr game object for UpgradeMenuCanvas which I need to toggle on and off when menu button clicked
    public GameObject optionsMenu;

    void Update () {
        // Reverse the active state every time escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Check whether it's active / inactive 
            bool isActive = optionsMenu.activeSelf;
 
            optionsMenu.SetActive(!isActive);
        }
    }
}
