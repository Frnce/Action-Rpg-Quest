using Advent.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Quests
{
    [System.Serializable]
    public class ItemReward
    {
        public Item item;
        public int count;
    }
    [CreateAssetMenu(menuName ="Quest/Quests",fileName = "New Quest")]
    public class Quest : ScriptableObject
    {
        public string title; //title for the quest
        public int id; //ID number of the quest
        [TextArea(3,10)]
        public string description; //string from our quest giver/reciever
        [TextArea(3, 10)]
        public string hint;//string from our quest giver/reciever
        [TextArea(3, 10)]
        public string congratulations;//string from our quest giver/reciever
        [TextArea(3, 10)]
        public string summary;//string from our quest giver/reciever
        public int nextQuest; //the next quest, if there is any quest chain

        public string questObjective; //name of the quest objective
        public int questObjectiveRequirement; //required amount of quest objective objects
        public int expReward;
        public int goldReward;
        public ItemReward[] itemReward;
    }
}
