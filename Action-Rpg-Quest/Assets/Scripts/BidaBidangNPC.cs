using Advent.Interfaces;
using Advent.Quests;
using Advent.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.NPC
{
    public class BidaBidangNPC : MonoBehaviour , IInteractable
    {
        public void Interact()
        {
            if (!QuestUIManager.instance.questPanelActive)
            {
                //quest ui manager 
                QuestUIManager.instance.CheckQuests(GetComponent<QuestObject>());
                //QuestManager.instance.RequestQuest(this);
            }
        }
    }
}