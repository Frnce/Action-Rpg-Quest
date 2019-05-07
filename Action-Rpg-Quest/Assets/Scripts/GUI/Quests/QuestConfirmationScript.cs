using Advent.Manager;
using Advent.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Quests
{
    public class QuestConfirmationScript : MonoBehaviour
    {
        public int questID { get; set; }
        public QuestType questType { get; set; }

        public void OnAccept()
        {
            AcceptQuest();
        }
        public void OnCancel()
        {
            ClosePanel();
        }
        private void AcceptQuest()
        {   
            QuestManager.instance.AcceptQuest(questID, questType);
            QuestUIManager.instance.HideQuestPanel();
            //Put Additional effects upon accepting quest here.
            ClosePanel();
        }
        private void ClosePanel()
        {
            gameObject.SetActive(false);
        }
    }
}