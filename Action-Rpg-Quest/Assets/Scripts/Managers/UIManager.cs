using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Manager
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;
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
        }
    }
}