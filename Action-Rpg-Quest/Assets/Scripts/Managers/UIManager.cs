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
            if(instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }

            DontDestroyOnLoad(gameObject);

            ShowUI(true);
        }
        private void Start()
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