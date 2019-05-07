using Advent.Manager;
using Advent.Quests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Advent.Entities;

namespace Advent.UI
{
    public class QuestUIManager : MonoBehaviour
    {
        public static QuestUIManager instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        public bool questAvailable = false;
        public bool questRunning = false;
        public bool questPanelActive = false;
        public bool questLogPanelActive = false;
        public bool questConfirmationPanelActive = false;

        public GameObject questPanel;
        public GameObject questLogPanel;
        public GameObject questConfirmationPanel;

        public List<QuestData> availableQuests = new List<QuestData>();
        public List<QuestData> activeQuests = new List<QuestData>();

        public GameObject qButton;
        public GameObject qLogButton;
        private List<GameObject> qButtons = new List<GameObject>();

        //public GameObject acceptButton;
        //public GameObject giveupButton;
        //public GameObject completeButton;

        public Transform qButtonSpace1;
        public Transform qButtonSpacer2;
        public Transform qLogButtonSpacer;

        public TMP_Text questTitle;
        public TMP_Text questDescription;
        public TMP_Text questSummary;

        public TMP_Text questLogTitle;
        public TMP_Text questLogDescription;
        public TMP_Text questLogSummary;

        private QuestObject currentQuestObject;

        //public QButtonScript acceptButtonScript;
        //public QButtonScript giveupButtonScript;
        //public QButtonScript completeButtonScript;

        private void Start()
        {
            //acceptButtonScript = acceptButton.GetComponent<QButtonScript>();
            //giveupButtonScript = giveupButton.GetComponent<QButtonScript>();
            //completeButtonScript = completeButton.GetComponent<QButtonScript>();

            //acceptButton.SetActive(false);
            //giveupButton.SetActive(false);
            //completeButton.SetActive(false);

            HideQuestPanel();
        }
        public void CheckQuests(QuestObject questObject)
        {
            currentQuestObject = questObject;
            QuestManager.instance.RequestQuest(questObject);
            if((questRunning || questAvailable )&& !questPanelActive)
            {
                ShowQuestPanel();
            }
            else
            {
                Debug.Log("No Quest Available");
            }
        }
        private void Update()
        {
            if (questPanelActive)
            {
                Player.instance.SetPlayerStates(PlayerStates.INMENU);
            }
        }

        public void ShowQuestPanel()
        {
            questPanelActive = true;
            questPanel.SetActive(questPanelActive);

            FillQuestButtons();
        }
        public void ShowConfirmationPanel()
        {

            questConfirmationPanelActive = true;
            questConfirmationPanel.SetActive(questConfirmationPanelActive);
        }
        //public void ShowQuestLogPanel()
        //{
        //    questLogPanel.SetActive(questLogPanelActive);
        //    if(questLogPanelActive && !questPanelActive)
        //    {
        //        foreach (QuestData currentQuest in QuestManager.instance.currentQuestList)
        //        {
        //            GameObject questButton = Instantiate(qLogButton);
        //            QLogButtonScript qButton = questButton.GetComponent<QLogButtonScript>();

        //            qButton.questID = currentQuest.quest.id;
        //            qButton.questTitle.text = currentQuest.quest.title;

        //            qButton.transform.SetParent(qLogButtonSpacer,false);
        //            qButtons.Add(questButton);
        //        }
        //    }
        //    else if(!questLogPanelActive && !questPanelActive)
        //    {
        //        HideQuestLogPanel();
        //    }
        //}
        public void ShowQuestLog(QuestData activeQuest)
        {
            questLogTitle.text = activeQuest.quest.title;
            if(activeQuest.progress == QuestProgress.ACCEPTED)
            {
                questLogDescription.text = activeQuest.quest.hint;
                questLogSummary.text = activeQuest.quest.questObjective + " : " + activeQuest.questObjectiveCount + " / " + activeQuest.quest.questObjectiveRequirement;
            }
            else if(activeQuest.progress == QuestProgress.COMPLETE)
            {
                questLogDescription.text = activeQuest.quest.congratulations;
                questLogSummary.text = activeQuest.quest.questObjective + " : " + activeQuest.questObjectiveCount + " / " + activeQuest.quest.questObjectiveRequirement;
            }
        }

        public void HideQuestPanel()
        {
            questPanelActive = false;
            questAvailable = false;
            questRunning = false;

            questTitle.text = "";
            questDescription.text = "";
            questSummary.text = "";

            availableQuests.Clear();
            activeQuests.Clear();

            for (int i = 0; i < qButtons.Count; i++)
            {
                Destroy(qButtons[i]);
            }
            qButtons.Clear();
            questPanel.SetActive(questPanelActive);
        }
        public void HideQuestLogPanel()
        {
            questLogPanelActive = false;

            questLogTitle.text = "";
            questLogDescription.text = "";
            questLogSummary.text = "";

            for (int i = 0; i < qButtons.Count; i++)
            {
                Destroy(qButtons[i]);
            }
            qButtons.Clear();
            questLogPanel.SetActive(questLogPanelActive);
        }

        void FillQuestButtons()
        {
            foreach (QuestData availableQuest in availableQuests)
            {
                GameObject questButton = Instantiate(qButton);
                QButtonScript qButtonScript = questButton.GetComponent<QButtonScript>();

                qButtonScript.questID = availableQuest.quest.id;
                qButtonScript.questTitle.text = availableQuest.quest.title;
                qButtonScript.questType = availableQuest.quest.questType;

                questButton.transform.SetParent(qButtonSpace1, false);
                qButtons.Add(questButton);
            }

            foreach (QuestData activeQuest in activeQuests)
            {
                GameObject questButton = Instantiate(qButton);
                QButtonScript qButtonScript = questButton.GetComponent<QButtonScript>();

                qButtonScript.questID = activeQuest.quest.id;
                qButtonScript.questTitle.text = activeQuest.quest.title;
                qButtonScript.questType = activeQuest.quest.questType;

                questButton.transform.SetParent(qButtonSpacer2, false);
                qButtons.Add(questButton);
            }
        }

        public void ShowSelectedQuest(int questID)
        {
            for (int i = 0; i < availableQuests.Count; i++)
            {
                if(availableQuests[i].quest.id == questID)
                {
                    questTitle.text = availableQuests[i].quest.title;
                    if(availableQuests[i].progress == QuestProgress.AVAILABLE)
                    {
                        questDescription.text = availableQuests[i].quest.description;
                        questSummary.text = availableQuests[i].quest.questObjective + " : " + availableQuests[i].questObjectiveCount + " / " + availableQuests[i].quest.questObjectiveRequirement;
                    }
                }
            }

            for (int i = 0; i < activeQuests.Count; i++)
            {
                if(activeQuests[i].quest.id == questID)
                {
                    questTitle.text = activeQuests[i].quest.title;
                    if(activeQuests[i].progress == QuestProgress.ACCEPTED)
                    {
                        questDescription.text = activeQuests[i].quest.hint;
                        questSummary.text = activeQuests[i].quest.questObjective + " : " + activeQuests[i].questObjectiveCount + " / " + activeQuests[i].quest.questObjectiveRequirement;
                    }
                    else if(activeQuests[i].progress == QuestProgress.COMPLETE)
                    {
                        questDescription.text = activeQuests[i].quest.congratulations;
                    }
                }
            }
        }
    }
}