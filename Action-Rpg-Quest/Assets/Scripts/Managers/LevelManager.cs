using Advent.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Manager
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Transform startingPointObject = null;
        private Player player = null;
        private Camera thisCamera;
        private List<GameObject> enemyList = new List<GameObject>();
        // Start is called before the first frame update
        void Start()
        {
            player = Player.instance;
            thisCamera = Camera.main;
            InitializeLevel();
        }
        private void InitializeLevel()
        {
            player.transform.position = startingPointObject.position;
            thisCamera.transform.position = startingPointObject.position;
            GetEnemies();
        }
        private void GetEnemies()
        {
            for (int i = 0; i < FindObjectsOfType<EnemyController>().Length; i++)
            {
                enemyList.Add(FindObjectsOfType<EnemyController>()[i].gameObject);
            }
        }
    }
}
