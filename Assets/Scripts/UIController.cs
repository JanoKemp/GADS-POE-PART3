using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

//Make sure to add scenes into build settings when applying this script
public class UIController : MonoBehaviour
{
    public GameObject SettingsMenu;
    
    public void OnPlayClick()
    {
        //Load relevant game scene.
        SceneManager.LoadScene(1);
    }

    public void OnSettingsClick()
    {
        SettingsMenu.SetActive(true);
    }

    public void OnBackClick()
    {
        SettingsMenu.SetActive(false);
    }

    public void OnExitClick()
    {
        Application.Quit();
    }
}
