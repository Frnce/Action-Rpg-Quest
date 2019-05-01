using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectButtonScript : MonoBehaviour
{
    public void GoToLevel()
    {
        if(SceneTitle != "???")
        {
            SceneManager.LoadScene(SceneTitle);
        }
        else
        {
            Debug.Log("Level is not unlocked yet");
        }
    }

    public string SceneTitle { get; set; }
}
