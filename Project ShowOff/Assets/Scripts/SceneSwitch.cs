using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public void scene_changer(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}
