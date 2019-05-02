using Advent.Dialogues;
using Advent.Interfaces;
using Advent.Quests;
using Advent.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.NPC
{
    public class BidaBidangNPC : MonoBehaviour, IInteractable
    {
        public enum NpcType
        {
            QUEST,
            TALKER,
            GIVER,
        }
        public NpcType npcType;
        public void Interact()
        {
            switch (npcType)
            {
                case NpcType.QUEST:
                    QuestGiver();
                    break;
                case NpcType.TALKER:
                    Talker();
                    break;
                case NpcType.GIVER:
                    break;
                default:
                    break;
            }
        }
        private void QuestGiver()
        {
            if (!QuestUIManager.instance.questPanelActive)
            {
                QuestUIManager.instance.CheckQuests(GetComponent<QuestObject>());
            }
        }
        private void Talker()
        {
            GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }
}