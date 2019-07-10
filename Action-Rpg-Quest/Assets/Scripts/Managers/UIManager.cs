using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Manager
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;
        [SerializeField]
        private List<GameObject> panelList = new List<GameObject>();
        private void Awake()
        {
            instance = this;

            ShowUI(true);
        }
        public void InitializeUIManager()
        {
            ShowUI(false);
        }
        private void ShowUI(bool value)
        {
            for (int i = 0; i < panelList.Count; i++)
            {
                panelList[i].SetActive(value);
            }
        }
    }
}