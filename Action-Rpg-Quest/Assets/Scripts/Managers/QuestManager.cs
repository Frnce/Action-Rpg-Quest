using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Quests;
using Advent.UI;

namespace Advent.Manager
{
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

        public List<Quest> questList = new List<Quest>();
        public List<Quest> currentQuestList = new List<Quest>();

        public void RequestQuest(QuestObject npcQuestObject)
        {
            if(npcQuestObject.availableQuestIDs.Count > 0)
            {
                for (int i = 0; i < questList.Count; i++)
                {
                    for (int k = 0; k < npcQuestObject.availableQuestIDs.Count; k++)
                    {
                        if(questList[i].id == npcQuestObject.availableQuestIDs[k] && questList[i].progress == Quest.QuestProgress.AVAILABLE)
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
                    if(currentQuestList[i].id == npcQuestObject.receivableQuestIDs[j] && currentQuestList[i].progress == Quest.QuestProgress.AVAILABLE || currentQuestList[i].progress == Quest.QuestProgress.COMPLETE)
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
                if(questList[i].id == questID && questList[i].progress == Quest.QuestProgress.AVAILABLE)
                {
                    currentQuestList.Add(questList[i]);
                    questList[i].progress = Quest.QuestProgress.ACCEPTED;
                }
            }
        }

        public void SurrenderQuest(int questID)
        {
            for (int i = 0; i < currentQuestList.Count; i++)
            {
                if(currentQuestList[i].id == questID && currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED)
                {
                    currentQuestList[i].progress = Quest.QuestProgress.AVAILABLE;
                    currentQuestList[i].questObjectiveCount = 0;
                    currentQuestList.Remove(currentQuestList[i]);
                }
            }
        }

        public void CompleteQuest(int questID)
        {
            for (int i = 0; i < currentQuestList.Count; i++)
            {
                if(currentQuestList[i].id == questID && currentQuestList[i].progress == Quest.QuestProgress.COMPLETE)
                {
                    currentQuestList[i].progress = Quest.QuestProgress.DONE;
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
                if (questList[i].id == questID && questList[i].nextQuest > 0)
                {
                    tempID = questList[i].nextQuest;
                }
            }

            if (tempID > 0)
            {
                for (int i = 0; i < questList.Count; i++)
                {
                    if (questList[i].id == tempID && questList[i].progress == Quest.QuestProgress.NOT_AVAILABLE)
                    {
                        questList[i].progress = Quest.QuestProgress.AVAILABLE;
                    }
                }
            }
        }
        public void AddQuestItem(string questObjective, int itemAmount)
        {
            for (int i = 0; i < currentQuestList.Count; i++)
            {
                if(currentQuestList[i].questObjective == questObjective && currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED)
                {
                    currentQuestList[i].questObjectiveCount += itemAmount;
                }

                if(currentQuestList[i].questObjectiveCount >= currentQuestList[i].questObjectiveRequirement && currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED)
                {
                    currentQuestList[i].progress = Quest.QuestProgress.COMPLETE;
                }
            }
        }

        public bool RequestAvailableQuest(int questID)
        {
            for (int i = 0; i < questList.Count; i++)
            {
                if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.AVAILABLE)
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
                if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.ACCEPTED)
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
                if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.COMPLETE)
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
                    if(questList[i].id  == npcQuestObject.availableQuestIDs[j] && questList[i].progress == Quest.QuestProgress.AVAILABLE)
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
                    if (questList[i].id == npcQuestObject.receivableQuestIDs[j] && questList[i].progress == Quest.QuestProgress.ACCEPTED)
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
                    if (questList[i].id == npcQuestObject.receivableQuestIDs[j] && questList[i].progress == Quest.QuestProgress.COMPLETE)
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
                if(currentQuestList[i].id == questID)
                {
                    QuestUIManager.instance.ShowQuestLog(currentQuestList[i]);
                }
            }
        }
    }
}
