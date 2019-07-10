using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Manager
{
    public class MainManager : MonoBehaviour
    {
        public static MainManager instance;
        private void Awake()
        {
            if(instance == null)    
            {
                instance = this;
            }
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            UIManager.instance.InitializeUIManager();
        }
    }
}