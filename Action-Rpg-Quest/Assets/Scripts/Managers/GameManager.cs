using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

            GetAllLevels();
        }
        public List<Level> levels = new List<Level>();
        [Header("Hit Stop FX")]
        [SerializeField]
        [Range(0f,1.5f)]
        private float duration = 1f;
        private bool isFrozen = false;
        private float pendingFreezeDuration = 0f;
        [Header("ScreenShake")]
        [SerializeField]
        private float shakeMagnitude = 0f;
        [SerializeField]
        private float shakeRough = 0f;
        [SerializeField]
        private float shakeFadeIn = 0f;
        [SerializeField]
        private float shakeFadeOut = 0f;

        private void Start()
        {
            
        }
        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.Alpha9)) // For Screenshoting stuff
            //{
            //    ScreenCapture.CaptureScreenshot("SomeLevel.png");
            //}
            if(pendingFreezeDuration > 0 && !isFrozen)
            {
                StartCoroutine(DoFreeze());
            }
        }
        public void ShakeCamera()
        {
            CameraShaker.Instance.ShakeOnce(shakeMagnitude, shakeRough, shakeFadeIn, shakeFadeOut);
        }
        private IEnumerator DoFreeze()
        {
            isFrozen = true;
            var original = Time.timeScale;
            Time.timeScale = 0;

            yield return new WaitForSecondsRealtime(duration);

            Time.timeScale = original;
            pendingFreezeDuration = 0;
            isFrozen = false;
        }
        public void Freeze()
        {
            pendingFreezeDuration = duration;
        }

        private void GetAllLevels()
        {

            int sceneCount = SceneManager.sceneCountInBuildSettings;
            for (int i = 1; i < sceneCount; i++)
            {
                string scene = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
                string[] splittedName = scene.Split('_');
                if (scene.Contains("Level"))
                {
                    levels.Add(new Level(scene, splittedName[1], false));
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