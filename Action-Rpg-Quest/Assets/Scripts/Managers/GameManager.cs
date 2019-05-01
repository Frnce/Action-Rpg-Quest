using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Manager
{
    [System.Serializable]
    public class Level
    {
        public string sceneName;
        public string levelTitle;
        public bool isUnlocked;

        public Level(string _sceneName, string _levelTitle, bool _isUnlocked)
        {
            sceneName = _sceneName;
            levelTitle = _levelTitle;
            isUnlocked = _isUnlocked;
        }
    }
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        private void Awake()
        {
            if(instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
            DontDestroyOnLoad(gameObject);

            GetAllLevels();
        }

        public List<Level> levels = new List<Level>();
        // Start is called before the first frame update
        private void GetAllLevels()
        {
            int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
            for (int i = 1; i < sceneCount; i++)
            {
                string scene = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
                string[] splittedName = scene.Split('_');
                if (scene.Contains("Level"))
                {
                    levels.Add(new Level(scene,splittedName[1], false));
                }
            }
        }
        public void UnlockInstance(string levelName)
        {
            for (int i = 0; i < levels.Count; i++)
            {
                if (levels[i].sceneName.Contains(levelName))
                {
                    levels[i].isUnlocked = true;
                    return;
                }
            }
        }
    }

}