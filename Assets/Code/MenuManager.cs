using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    // theres alr game object for UpgradeMenuCanvas which I need to toggle on and off when menu button clicked
    public GameObject optionsMenu;

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool isActive = optionsMenu.activeSelf;
 
            optionsMenu.SetActive(!isActive);
        }
    }
}
