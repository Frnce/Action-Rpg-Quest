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
    [System.Serializable]
    public class currentQuests
    {
        public QuestData mainQuest;
        public QuestData sideQuest;
        public QuestData missionQuest;
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
        public currentQuests currentQuests = new currentQuests();

        private void Start()
        {
            currentQuests.mainQuest = new QuestData();
            currentQuests.sideQuest = new QuestData();
            currentQuests.missionQuest = new QuestData();
        }

        public void RequestQuest(QuestObject npcQuestObject)
        {
            if (npcQuestObject.availableQuests.Count > 0)
            {
                for (int i = 0; i < questList.Count; i++)
                {
                    for (int k = 0; k < npcQuestObject.availableQuests.Count; k++)
                    {
                        if (questList[i].quest.id == npcQuestObject.availableQuests[k].quest.id && questList[i].progress == QuestProgress.AVAILABLE)
                        {
                            Debug.Log("QuestID : " + npcQuestObject.availableQuests[k] + " " + questList[i].progress);
                            //test
                            //AcceptQuest(npcQuestObject.availableQuestIDs[k]);
                            //UI
                            QuestUIManager.instance.questAvailable = true;
                            QuestUIManager.instance.availableQuests.Add(questList[i]);
                        }
                    }
                }
            }

            //main quest
            if (currentQuests.mainQuest.quest != null)
            {
                Quest quest = currentQuests.mainQuest.quest;
                if(quest.questType == npcQuestObject.receivableType && currentQuests.mainQuest.progress == QuestProgress.AVAILABLE || currentQuests.mainQuest.progress == QuestProgress.COMPLETE)
                {
                    Debug.Log("Quest Type: " + npcQuestObject.receivableType + " is " + currentQuests.mainQuest.progress);
                    QuestUIManager.instance.questRunning = true;
                    QuestUIManager.instance.activeQuests.Add(currentQuests.mainQuest);
                }
            }
            //side quest
            if (currentQuests.sideQuest.quest != null)
            {
                Quest quest = currentQuests.sideQuest.quest;
                if (quest.questType == npcQuestObject.receivableType && currentQuests.sideQuest.progress == QuestProgress.AVAILABLE || currentQuests.sideQuest.progress == QuestProgress.COMPLETE)
                {
                    Debug.Log("Quest Type: " + npcQuestObject.receivableType + " is " + currentQuests.sideQuest.progress);
                    QuestUIManager.instance.questRunning = true;
                    QuestUIManager.instance.activeQuests.Add(currentQuests.sideQuest);
                }
            }
            //Mission Quest
            if(currentQuests.missionQuest.quest != null)
            {
                Quest quest = currentQuests.missionQuest.quest;
                if (quest.questType == npcQuestObject.receivableType && currentQuests.missionQuest.progress == QuestProgress.AVAILABLE || currentQuests.missionQuest.progress == QuestProgress.COMPLETE)
                {
                    Debug.Log("Quest Type: " + npcQuestObject.receivableType + " is " + currentQuests.missionQuest.progress);
                    QuestUIManager.instance.questRunning = true;
                    QuestUIManager.instance.activeQuests.Add(currentQuests.missionQuest);
                }
            }
        }

        public void AcceptQuest(int questID,QuestType questType)
        {
            for (int i = 0; i < questList.Count; i++)
            {
                switch (questType)
                {
                    case QuestType.MAIN:
                        if (questList[i].quest.id == questID && questList[i].progress == QuestProgress.AVAILABLE)
                        {
                            if(currentQuests.mainQuest.quest == null)
                            {
                                currentQuests.mainQuest = questList[i];
                                questList[i].progress = QuestProgress.ACCEPTED;
                            }
                            else
                            {
                                Debug.Log("Quest : " + questList[i].quest.questType + " in Progress");
                                return;
                            }
                        }
                        break;
                    case QuestType.SIDE:
                        if (questList[i].quest.id == questID && questList[i].progress == QuestProgress.AVAILABLE)
                        {
                            if (currentQuests.sideQuest.quest == null)
                            {
                                currentQuests.sideQuest = questList[i];
                                questList[i].progress = QuestProgress.ACCEPTED;
                            }
                            else
                            {
                                Debug.Log("Quest : " + questList[i].quest.questType + " in Progress");
                                return;
                            }
                        }
                        break;
                    case QuestType.MISSION:
                        if (questList[i].quest.id == questID && questList[i].progress == QuestProgress.AVAILABLE)
                        {
                            if (currentQuests.missionQuest.quest == null)
                            {
                                currentQuests.missionQuest = questList[i];
                                questList[i].progress = QuestProgress.ACCEPTED;
                            }
                            else
                            {
                                Debug.Log("Quest : " + questList[i].quest.questType + " in Progress");
                                return;
                            }
                        }
                        break;
                    case QuestType.NONE:
                        break;
                    default:
                        break;
                }
            }
        }

        public void SurrenderQuest(int questID,QuestType questType)
        {
            Quest quest = null;
            switch (questType)
            {
                case QuestType.MAIN:
                    quest = currentQuests.mainQuest.quest; 
                    if(quest.id == questID && currentQuests.mainQuest.progress == QuestProgress.ACCEPTED)
                    {
                        currentQuests.mainQuest.progress = QuestProgress.AVAILABLE;
                        currentQuests.mainQuest.questObjectiveCount = 0;
                        currentQuests.mainQuest = null;
                    }
                    break;
                case QuestType.SIDE:
                     quest = currentQuests.sideQuest.quest;
                    if (quest.id == questID && currentQuests.sideQuest.progress == QuestProgress.ACCEPTED)
                    {
                        currentQuests.sideQuest.progress = QuestProgress.AVAILABLE;
                        currentQuests.sideQuest.questObjectiveCount = 0;
                        currentQuests.sideQuest = null;
                    }
                    break;
                case QuestType.MISSION:
                     quest = currentQuests.missionQuest.quest;
                    if (quest.id == questID && currentQuests.missionQuest.progress == QuestProgress.ACCEPTED)
                    {
                        currentQuests.missionQuest.progress = QuestProgress.AVAILABLE;
                        currentQuests.missionQuest.questObjectiveCount = 0;
                        currentQuests.missionQuest = null;
                    }
                    break;
                case QuestType.NONE:
                    break;
                default:
                    break;
            }
        }

        //Check If a quest is complete upon talking to an NPC
        public bool CheckIfComplete() // return false if It hhas an existing quest on current . return true when nothing is on current. for this to not open the UI
        {
            //Check All Quests if Not null
            if (currentQuests.mainQuest.quest != null)
            {
                Quest quest = currentQuests.mainQuest.quest;
                if(currentQuests.mainQuest.progress == QuestProgress.COMPLETE)
                {
                    CompleteQuest(quest.id, quest.questType);
                    return false;
                }
            }
            //side quest
            if (currentQuests.sideQuest.quest != null)
            {
                Quest quest = currentQuests.sideQuest.quest;
                if(currentQuests.sideQuest.progress == QuestProgress.COMPLETE)
                {
                    CompleteQuest(quest.id, quest.questType);
                    return false;
                }
            }
            //Mission Quest
            if (currentQuests.missionQuest.quest != null)
            {
                Quest quest = currentQuests.missionQuest.quest;
                if(currentQuests.missionQuest.progress == QuestProgress.COMPLETE)
                {
                    CompleteQuest(quest.id, quest.questType);
                    return false;
                }
            }
            return true;
        }

        public void CompleteQuest(int questID, QuestType questType)
        {
            Quest quest = null;
            switch (questType)
            {
                case QuestType.MAIN:
                    quest = currentQuests.mainQuest.quest;
                    if (quest != null)
                    {
                        if (quest.id == questID && currentQuests.mainQuest.progress == QuestProgress.COMPLETE)
                        {
                            currentQuests.mainQuest.progress = QuestProgress.DONE;
                            currentQuests.mainQuest = null;
                        }
                    }
                    break;
                case QuestType.SIDE:
                    quest = currentQuests.sideQuest.quest;
                    if (quest != null)
                    {
                        if (quest.id == questID && currentQuests.sideQuest.progress == QuestProgress.COMPLETE)
                        {
                            currentQuests.sideQuest.progress = QuestProgress.DONE;
                            currentQuests.sideQuest = null;
                        }
                    }
                    break;
                case QuestType.MISSION:
                    quest = currentQuests.missionQuest.quest;
                    if (quest != null)
                    {
                        if (quest.id == questID && currentQuests.missionQuest.progress == QuestProgress.COMPLETE)
                        {
                            currentQuests.missionQuest.progress = QuestProgress.DONE;
                            currentQuests.missionQuest = null;
                        }
                    }
                    break;
                case QuestType.NONE:
                    break;
                default:
                    break;
            }
            //check for chain quest
            CheckChainQuest(questID);
        }

        private void CheckChainQuest(int questID)
        {
            int tempID = 0;
            for (int i = 0; i < questList.Count; i++)
            {
                if (questList[i].quest.id == questID && questList[i].quest.questType == QuestType.CHAIN && questList[i].quest.nextQuest > 0)
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
            Quest quest = null;
            for (int i = 0; i < questList.Count; i++)
            {
                switch (questList[i].quest.questType)
                {
                    case QuestType.MAIN:
                        quest = currentQuests.mainQuest.quest;
                        if (quest != null)
                        {
                            if (quest.questObjective == questObjective && currentQuests.mainQuest.progress == QuestProgress.ACCEPTED)
                            {
                                currentQuests.mainQuest.questObjectiveCount += itemAmount;
                            }

                            if (currentQuests.mainQuest.questObjectiveCount >= quest.questObjectiveRequirement && currentQuests.mainQuest.progress == QuestProgress.ACCEPTED)
                            {
                                currentQuests.mainQuest.progress = QuestProgress.COMPLETE;
                            }
                        }
                        break;
                    case QuestType.SIDE:
                        quest = currentQuests.sideQuest.quest;
                        if(quest != null)
                        {
                            if (quest.questObjective == questObjective && currentQuests.sideQuest.progress == QuestProgress.ACCEPTED)
                            {
                                currentQuests.sideQuest.questObjectiveCount += itemAmount;
                            }

                            if (currentQuests.sideQuest.questObjectiveCount >= quest.questObjectiveRequirement && currentQuests.sideQuest.progress == QuestProgress.ACCEPTED)
                            {
                                currentQuests.sideQuest.progress = QuestProgress.COMPLETE;
                            }
                        }
                        break;
                    case QuestType.MISSION:
                        quest = currentQuests.missionQuest.quest;
                        if(quest != null)
                        {
                            if (quest.questObjective == questObjective && currentQuests.missionQuest.progress == QuestProgress.ACCEPTED)
                            {
                                currentQuests.missionQuest.questObjectiveCount += itemAmount;
                            }
                            if (currentQuests.missionQuest.questObjectiveCount >= quest.questObjectiveRequirement && currentQuests.missionQuest.progress == QuestProgress.ACCEPTED)
                            {
                                currentQuests.missionQuest.progress = QuestProgress.COMPLETE;
                            }
                        }
                        break;
                    case QuestType.CHAIN:
                        break;
                    case QuestType.NONE:
                        break;
                    default:
                        break;
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
                for (int j = 0; j < npcQuestObject.availableQuests.Count; j++)
                {
                    if(questList[i].quest.id  == npcQuestObject.availableQuests[j].quest.id && questList[i].progress == QuestProgress.AVAILABLE)
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
                if (questList[i].quest.questType == npcQuestObject.receivableType && questList[i].progress == QuestProgress.ACCEPTED)
                {
                    return true;
                }
            }
            return false;
        }
        public bool CheckCompletedQuest(QuestObject npcQuestObject)
        {
            for (int i = 0; i < questList.Count; i++)
            {
                if (questList[i].quest.questType == npcQuestObject.receivableType && questList[i].progress == QuestProgress.COMPLETE)
                {
                    return true;
                }
            }
            return false;
        }
        public void ShowQuestLog(int questID)
        {
            if(currentQuests.missionQuest != null)
            {
                QuestUIManager.instance.ShowQuestLog(currentQuests.mainQuest);
            }
            if(currentQuests.sideQuest != null)
            {
                QuestUIManager.instance.ShowQuestLog(currentQuests.sideQuest);
            }
            if(currentQuests.missionQuest != null)
            {
                QuestUIManager.instance.ShowQuestLog(currentQuests.missionQuest);
            }
        }
    }
}
