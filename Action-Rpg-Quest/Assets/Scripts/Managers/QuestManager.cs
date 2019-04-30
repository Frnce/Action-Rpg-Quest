using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Quests;
using Advent.UI;

namespace Advent.Manager
{
    public enum QuestProgress
    {
        NOT_AVAILABLE,
        AVAILABLE,
        ACCEPTED,
        COMPLETE,
        DONE
    }
    [System.Serializable]
    public class QuestData
    {
        public Quest quest;
        public QuestProgress progress; //state of the current quest
        public int questObjectiveCount; //current number of quest objective
    }
    public class QuestManager : MonoBehaviour
    {
        #region singleton
        public static QuestManager instance;
        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else if(instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }
        #endregion

        public List<QuestData> questList = new List<QuestData>();
        public List<QuestData> currentQuestList = new List<QuestData>();

        public void RequestQuest(QuestObject npcQuestObject)
        {
            if(npcQuestObject.availableQuestIDs.Count > 0)
            {
                for (int i = 0; i < questList.Count; i++)
                {
                    for (int k = 0; k < npcQuestObject.availableQuestIDs.Count; k++)
                    {
                        if(questList[i].quest.id == npcQuestObject.availableQuestIDs[k] && questList[i].progress == QuestProgress.AVAILABLE)
                        {
                            Debug.Log("QuestID : " + npcQuestObject.availableQuestIDs[k] + " " + questList[i].progress);
                            //test
                            //AcceptQuest(npcQuestObject.availableQuestIDs[k]);
                            //UI
                            QuestUIManager.instance.questAvailable = true;
                            QuestUIManager.instance.availableQuests.Add(questList[i]);
                        }
                    }
                }
            }

            for (int i = 0; i < currentQuestList.Count; i++)
            {
                for (int j = 0; j < npcQuestObject.receivableQuestIDs.Count; j++)
                {
                    if(currentQuestList[i].quest.id == npcQuestObject.receivableQuestIDs[j] && currentQuestList[i].progress == QuestProgress.AVAILABLE || currentQuestList[i].progress == QuestProgress.COMPLETE)
                    {
                        Debug.Log("Quest ID: " + npcQuestObject.receivableQuestIDs[j] + " is " + currentQuestList[i].progress);

                        //CompleteQuest(npcQuestObject.receivableQuestIDs[j]);
                        QuestUIManager.instance.questRunning = true;
                        QuestUIManager.instance.activeQuests.Add(questList[i]);
                    }
                }
            }
        }

        public void AcceptQuest(int questID)
        {
            for (int i = 0; i < questList.Count; i++)
            {
                if(questList[i].quest.id == questID && questList[i].progress == QuestProgress.AVAILABLE)
                {
                    currentQuestList.Add(questList[i]);
                    questList[i].progress = QuestProgress.ACCEPTED;
                }
            }
        }

        public void SurrenderQuest(int questID)
        {
            for (int i = 0; i < currentQuestList.Count; i++)
            {
                if(currentQuestList[i].quest.id == questID && currentQuestList[i].progress == QuestProgress.ACCEPTED)
                {
                    currentQuestList[i].progress = QuestProgress.AVAILABLE;
                    currentQuestList[i].questObjectiveCount = 0;
                    currentQuestList.Remove(currentQuestList[i]);
                }
            }
        }

        public void CompleteQuest(int questID)
        {
            for (int i = 0; i < currentQuestList.Count; i++)
            {
                if(currentQuestList[i].quest.id == questID && currentQuestList[i].progress == QuestProgress.COMPLETE)
                {
                    currentQuestList[i].progress = QuestProgress.DONE;
                    currentQuestList.Remove(currentQuestList[i]);

                    //reward
                }
            }
            //check for chain quest
            CheckChainQuest(questID);
        }

        private void CheckChainQuest(int questID)
        {
            int tempID = 0;
            for (int i = 0; i < questList.Count; i++)
            {
                if (questList[i].quest.id == questID && questList[i].quest.nextQuest > 0)
                {
                    tempID = questList[i].quest.nextQuest;
                }
            }

            if (tempID > 0)
            {
                for (int i = 0; i < questList.Count; i++)
                {
                    if (questList[i].quest.id == tempID && questList[i].progress == QuestProgress.NOT_AVAILABLE)
                    {
                        questList[i].progress = QuestProgress.AVAILABLE;
                    }
                }
            }
        }
        public void AddQuestItem(string questObjective, int itemAmount)
        {
            for (int i = 0; i < currentQuestList.Count; i++)
            {
                if(currentQuestList[i].quest.questObjective == questObjective && currentQuestList[i].progress == QuestProgress.ACCEPTED)
                {
                    currentQuestList[i].questObjectiveCount += itemAmount;
                }

                if(currentQuestList[i].questObjectiveCount >= currentQuestList[i].quest.questObjectiveRequirement && currentQuestList[i].progress == QuestProgress.ACCEPTED)
                {
                    currentQuestList[i].progress = QuestProgress.COMPLETE;
                }
            }
        }

        public bool RequestAvailableQuest(int questID)
        {
            for (int i = 0; i < questList.Count; i++)
            {
                if (questList[i].quest.id == questID && questList[i].progress == QuestProgress.AVAILABLE)
                {
                    return true;
                }
            }
            return false;
        }
        public bool RequestAcceptedQuest(int questID)
        {
            for (int i = 0; i < questList.Count; i++)
            {
                if (questList[i].quest.id == questID && questList[i].progress == QuestProgress.ACCEPTED)
                {
                    return true;
                }
            }
            return false;
        }
        public bool RequestCompleteQuest(int questID)
        {
            for (int i = 0; i < questList.Count; i++)
            {
                if (questList[i].quest.id == questID && questList[i].progress == QuestProgress.COMPLETE)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckAvailableQuest(QuestObject npcQuestObject)
        {
            for (int i = 0; i < questList.Count; i++)
            {
                for (int j = 0; j < npcQuestObject.availableQuestIDs.Count; j++)
                {
                    if(questList[i].quest.id  == npcQuestObject.availableQuestIDs[j] && questList[i].progress == QuestProgress.AVAILABLE)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool CheckAcceptedQuest(QuestObject npcQuestObject)
        {
            for (int i = 0; i < questList.Count; i++)
            {
                for (int j = 0; j < npcQuestObject.receivableQuestIDs.Count; j++)
                {
                    if (questList[i].quest.id == npcQuestObject.receivableQuestIDs[j] && questList[i].progress == QuestProgress.ACCEPTED)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool CheckCompletedQuest(QuestObject npcQuestObject)
        {
            for (int i = 0; i < questList.Count; i++)
            {
                for (int j = 0; j < npcQuestObject.receivableQuestIDs.Count; j++)
                {
                    if (questList[i].quest.id == npcQuestObject.receivableQuestIDs[j] && questList[i].progress == QuestProgress.COMPLETE)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void ShowQuestLog(int questID)
        {
            for (int i = 0; i < currentQuestList.Count; i++)
            {
                if(currentQuestList[i].quest.id == questID)
                {
                    QuestUIManager.instance.ShowQuestLog(currentQuestList[i]);
                }
            }
        }
    }
}
