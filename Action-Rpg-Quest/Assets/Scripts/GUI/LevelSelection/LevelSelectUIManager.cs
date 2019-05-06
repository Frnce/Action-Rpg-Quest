using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Manager;
using UnityEngine.UI;
using TMPro;

namespace Advent.UI
{
    public class LevelSelectUIManager : MonoBehaviour
    {
        public static LevelSelectUIManager instance;
        private void Awake()
        {
            instance = this;
        }
        [SerializeField]
        private GameObject levelSelectPanel = null;
        [SerializeField]
        private GameObject levelSelectHolder = null;
        [SerializeField]
        private GameObject levelSelectButton = null;
        private GameManager gameManager;
        private List<GameObject> levelSelectButtons = new List<GameObject>();
        public delegate void OnLevelUnlocked();
        public OnLevelUnlocked onLevelUnlocked;
        private void Start()
        {
            gameManager = GameManager.instance;
            InstantiateLevelSelectButtons();
            onLevelUnlocked += GetAllLevels;
        }
        private void InstantiateLevelSelectButtons()
        {
            for (int i = 0; i < gameManager.levels.Count; i++)
            {
                GameObject levelObj = Instantiate(levelSelectButton, levelSelectHolder.transform) as GameObject;
                levelSelectButtons.Add(levelObj);
            }
        }
        private void GetAllLevels()
        {
            for (int i = 0; i < levelSelectButtons.Count; i++)
            {
                if (!gameManager.levels[i].isUnlocked)
                {
                    levelSelectButtons[i].GetComponentInChildren<TMP_Text>().text = "???";
                    levelSelectButtons[i].GetComponent<LevelSelectButtonScript>().SceneTitle = "???";
                }
                else
                {
                    levelSelectButtons[i].GetComponentInChildren<TMP_Text>().text = gameManager.levels[i].levelTitle;
                    levelSelectButtons[i].GetComponent<LevelSelectButtonScript>().SceneTitle = gameManager.levels[i].sceneName;
                }
            }
        }
        public void OpenLevelSelect()
        {
            onLevelUnlocked.Invoke();
            levelSelectPanel.SetActive(true);
        }
        public void CloseLevelSelect()
        {
            levelSelectPanel.SetActive(false);
        }
    }
}
