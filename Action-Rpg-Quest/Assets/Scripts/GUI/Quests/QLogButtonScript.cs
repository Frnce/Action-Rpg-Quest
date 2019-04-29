using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Advent.Manager;

namespace Advent.UI
{
    public class QLogButtonScript : MonoBehaviour
    {
        public int questID;
        public TMP_Text questTitle;

        public void ShowAllInfos()
        {
            QuestManager.instance.ShowQuestLog(questID);
        }
        public void ClosePanel()
        {
            QuestUIManager.instance.HideQuestLogPanel();
        }
    }
}