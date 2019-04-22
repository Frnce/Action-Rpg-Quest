using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Quests
{
    public class QuestEvent
    {
        public enum EventStatus
        {
            WAITING,
            CURRENT,
            DONE
        }

        public string name;
        public string description;
        public string id;
        public EventStatus status;

        public List<QuestPath> pathList = new List<QuestPath>();

        public QuestEvent(string _name,string _description)
        {
            id = Guid.NewGuid().ToString();
            name = _name;
            description = _description;
            status = EventStatus.WAITING;
        }
        public void UpdateQuestEvent(EventStatus eventStatus)
        {
            status = eventStatus;
        }
        public string GetId()
        {
            return id;
        }
    }
}