using Advent.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectButtonScript : MonoBehaviour
{
    public void GoToLevel()
    {
        if (SceneTitle != "???")
        {
            SceneManager.LoadScene(SceneTitle);
            LevelSelectUIManager.instance.CloseLevelSelect();
        }
        else
        {
            Debug.Log("Level is not unlocked yet");
        }
    }

    public string SceneTitle { get; set; }
}