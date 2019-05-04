using Advent.Interfaces;
using Advent.Quests;
using Advent.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.GameObjects
{
    public class MissionBoardScript : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            ShowMissions();
        }
        private void ShowMissions()
        {
            if (!QuestUIManager.instance.questPanelActive)
            {
                QuestUIManager.instance.CheckQuests(GetComponent<QuestObject>());
            }
        }
    }
}
