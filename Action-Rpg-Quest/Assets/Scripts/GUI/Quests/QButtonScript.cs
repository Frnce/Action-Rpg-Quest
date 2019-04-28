using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Advent.Manager;
using Advent.Quests;

namespace Advent.UI
{
    public class QButtonScript : MonoBehaviour
    {
        public int questID;
        public TMP_Text questTitle;

        private GameObject acceptButton;
        private GameObject giveupButton;
        private GameObject completeButton;

        private QButtonScript acceptButtonScript;
        private QButtonScript giveupButtonScript;
        private QButtonScript completeButtonScript;

        private void Start()
        {
            //acceptButton = GameObject.Find("Canvas").gameObject.transform.Find("QuestPanel").gameObject.transform.Find("QuestDescription").gameObject.transform.Find("GameObject").gameObject.transform.Find("Accept Button").gameObject;
            acceptButton = GameObject.FindWithTag("AcceptQuestButton");
            acceptButtonScript = acceptButton.GetComponent<QButtonScript>();

            //giveupButton = GameObject.Find("Canvas").gameObject.transform.Find("QuestPanel").gameObject.transform.Find("QuestDescription").gameObject.transform.Find("GameObject").gameObject.transform.Find("Giveup Button").gameObject;
            giveupButton = GameObject.FindWithTag("GiveUpQuestButton");
            giveupButtonScript = acceptButton.GetComponent<QButtonScript>();

            //completeButton = GameObject.Find("Canvas").gameObject.transform.Find("QuestPanel").gameObject.transform.Find("QuestDescription").gameObject.transform.Find("GameObject").gameObject.transform.Find("Complete Button").gameObject;
            completeButton = GameObject.FindWithTag("CompleteQuestButton");
            completeButtonScript = acceptButton.GetComponent<QButtonScript>();

            acceptButton.SetActive(false);
            giveupButton.SetActive(false);
            completeButton.SetActive(false);
        }

        public void ShowAllInfos()
        {
            QuestUIManager.instance.ShowSelectedQuest(questID);

            //ACcept button
            if (QuestManager.instance.RequestAvailableQuest(questID))
            {
                acceptButton.SetActive(true);
                acceptButtonScript.questID = questID;
            }
            else
            {
                acceptButton.SetActive(false);
            }
            //giveup button
            if (QuestManager.instance.RequestAcceptedQuest(questID))
            {
                giveupButton.SetActive(true);
                giveupButtonScript.questID = questID;
            }
            else
            {
                giveupButton.SetActive(false);
            }
            //complete Button
            if (QuestManager.instance.RequestCompleteQuest(questID))
            {
                completeButton.SetActive(true);
                completeButtonScript.questID = questID;
            }
            else
            {
                completeButton.SetActive(false);
            }
        }

        public void AcceptQuest()
        {
            QuestManager.instance.AcceptQuest(questID);
            QuestUIManager.instance.HideQuestPanel();

            //Update all Quest Giver NPC
            QuestObject[] currentQuestGivers = FindObjectsOfType<QuestObject>() as QuestObject[];
            foreach (QuestObject item in currentQuestGivers)
            {

            }
        }

        public void GiveUpQuest()
        {
            QuestManager.instance.SurrenderQuest(questID);
            QuestUIManager.instance.HideQuestPanel();

            //Update all Quest Giver NPC
            QuestObject[] currentQuestGivers = FindObjectsOfType<QuestObject>() as QuestObject[];
            foreach (QuestObject item in currentQuestGivers)
            {

            }
        }

        public void CompleteQuest()
        {
            QuestManager.instance.CompleteQuest(questID);
            QuestUIManager.instance.HideQuestPanel();

            //Update all Quest Giver NPC
            QuestObject[] currentQuestGivers = FindObjectsOfType<QuestObject>() as QuestObject[];
            foreach (QuestObject item in currentQuestGivers)
            {

            }
        }
        public void ClosePanel()
        {
            QuestUIManager.instance.HideQuestPanel();
        }
    }
}