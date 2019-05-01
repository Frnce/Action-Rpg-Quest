using Advent.Manager;
using Advent.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Advent
{
    public class TravelToInstance : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                //QuestManager.instance.AddQuestItem("Leave Town", 1);
                //SceneManager.LoadScene("Forest_1-1");
                //test
                LevelSelectorUIManager.instance.OpenLevelSelect();
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                LevelSelectorUIManager.instance.CloseLevelSelect();
            }
        }
    }
}