using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Quests
{
    public class Quest
    {
        public List<QuestEvent> questEvents = new List<QuestEvent>();
        private List<QuestEvent> pathList = new List<QuestEvent>();

        public Quest() { }
        
        public QuestEvent AddQuestEvent(string name, string desc)
        {
            QuestEvent questEvent = new QuestEvent(name, desc);
            questEvents.Add(questEvent);
            return questEvent;
        }
        public void AddPath(string fromQuestEvent,string toQuestEvent)
        {
            QuestEvent from = FindQuestEvent(fromQuestEvent);
            QuestEvent to = FindQuestEvent(toQuestEvent);

            if(from != null && to != null)
            {
                QuestPath path = new QuestPath(from, to);
                from.pathList.Add(path);
            }
        }
        private QuestEvent FindQuestEvent(string id)
        {
            foreach (QuestEvent quest in questEvents)
            {
                if(quest.GetId() == id)
                {
                    return quest;
                }
            }
            return null;
        }
        public void PrintPath()
        {   
            foreach (QuestEvent quest in questEvents)
            {
                Debug.Log(quest.name);
            }
        }
    }
}