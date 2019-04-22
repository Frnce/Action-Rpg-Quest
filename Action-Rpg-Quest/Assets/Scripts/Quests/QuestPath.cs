using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Advent.Quests
{
    public class QuestPath
    {
        public QuestEvent startEvent;
        public QuestEvent endEvent;
        public QuestPath(QuestEvent from, QuestEvent to)
        {
            startEvent = from;
            endEvent = to;
        }
    }
}