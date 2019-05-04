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
        public QuestType questType;
        public TMP_Text questTitle;

        //private GameObject acceptButton;
        //private GameObject giveupButton;
        //private GameObject completeButton;

        //private QButtonScript acceptButtonScript;
        //private QButtonScript giveupButtonScript;
        //private QButtonScript completeButtonScript;

        //private void Start()
        //{
        //    //acceptButton = GameObject.Find("Canvas").gameObject.transform.Find("QuestPanel").gameObject.transform.Find("QuestDescription").gameObject.transform.Find("GameObject").gameObject.transform.Find("Accept Button").gameObject;
        //    acceptButton = GameObject.FindWithTag("AcceptQuestButton");
        //    //acceptButtonScript = acceptButton.GetComponent<QButtonScript>();
        //    Debug.Log(acceptButton);
        //    //giveupButton = GameObject.Find("Canvas").gameObject.transform.Find("QuestPanel").gameObject.transform.Find("QuestDescription").gameObject.transform.Find("GameObject").gameObject.transform.Find("Giveup Button").gameObject;
        //    giveupButton = GameObject.FindWithTag("GiveUpQuestButton");
        //    //giveupButtonScript = acceptButton.GetComponent<QButtonScript>();

        //    //completeButton = GameObject.Find("Canvas").gameObject.transform.Find("QuestPanel").gameObject.transform.Find("QuestDescription").gameObject.transform.Find("GameObject").gameObject.transform.Find("Complete Button").gameObject;
        //    completeButton = GameObject.FindWithTag("CompleteQuestButton");
        //    //completeButtonScript = acceptButton.GetComponent<QButtonScript>();

        //    acceptButton.SetActive(false);
        //    giveupButton.SetActive(false);
        //    completeButton.SetActive(false);
        //}

        public void ShowAllInfos()
        {
            QuestUIManager.instance.ShowSelectedQuest(questID);

            //ACcept button
            if (QuestManager.instance.RequestAvailableQuest(questID))
            {
                QuestUIManager.instance.acceptButton.SetActive(true);
                QuestUIManager.instance.acceptButtonScript.GetComponent<QButtonScript>().questID = questID;
            }
            else
            {
                QuestUIManager.instance.acceptButton.SetActive(false);
            }
            //giveup button
            if (QuestManager.instance.RequestAcceptedQuest(questID))
            {
                QuestUIManager.instance.giveupButton.SetActive(true);
                QuestUIManager.instance.giveupButtonScript.GetComponent<QButtonScript>().questID = questID;
            }
            else
            {
                QuestUIManager.instance.giveupButton.SetActive(false);
            }
            //complete Button
            if (QuestManager.instance.RequestCompleteQuest(questID))
            {
                QuestUIManager.instance.completeButton.SetActive(true);
                QuestUIManager.instance.completeButtonScript.GetComponent<QButtonScript>().questID = questID;
            }
            else
            {
                QuestUIManager.instance.completeButton.SetActive(false);
            }
        }

        public void AcceptQuest()
        {
            QuestManager.instance.AcceptQuest(questID, questType);
            QuestUIManager.instance.HideQuestPanel();

            //Update all Quest Giver NPC
            QuestObject[] currentQuestGivers = FindObjectsOfType<QuestObject>() as QuestObject[];
            foreach (QuestObject item in currentQuestGivers)
            {
                //item.SetQuestMaker();
            }
        }

        public void GiveUpQuest()
        {
            QuestManager.instance.SurrenderQuest(questID,questType);
            QuestUIManager.instance.HideQuestPanel();

            //Update all Quest Giver NPC
            QuestObject[] currentQuestGivers = FindObjectsOfType<QuestObject>() as QuestObject[];
            foreach (QuestObject item in currentQuestGivers)
            {
                //item.SetQuestMaker();
            }
        }

        public void CompleteQuest()
        {
            QuestManager.instance.CompleteQuest(questID, questType);
            QuestUIManager.instance.HideQuestPanel();

            //Update all Quest Giver NPC
            QuestObject[] currentQuestGivers = FindObjectsOfType<QuestObject>() as QuestObject[];
            foreach (QuestObject item in currentQuestGivers)
            {
                //item.SetQuestMaker();
            }
        }
        public void ClosePanel()
        {
            QuestUIManager.instance.HideQuestPanel();
            QuestUIManager.instance.acceptButton.SetActive(false);
            QuestUIManager.instance.giveupButton.SetActive(false);
            QuestUIManager.instance.completeButton.SetActive(false);
        }
    }
}