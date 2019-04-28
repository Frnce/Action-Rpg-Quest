using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Quests
{
    [System.Serializable]
    public class Quest
    {
        public enum QuestProgress
        {
            NOT_AVAILABLE,
            AVAILABLE,
            ACCEPTED,
            COMPLETE,
            DONE
        }

        public string title; //title for the quest
        public int id; //ID number of the quest
        public QuestProgress progress; //state of the current quest
        public string description; //string from our quest giver/reciever
        public string hint;//string from our quest giver/reciever
        public string congratulations;//string from our quest giver/reciever
        public string summary;//string from our quest giver/reciever
        public int nextQuest; //the next quest, if there is any quest chain

        public string questObjective; //name of the quest objective
        public int questObjectiveCount; //current number of quest objective
        public int questObjectiveRequirement; //required amount of quest objective objects

        public int expReward;
        public int goldReward;
        public string itemReward;
    }
}
