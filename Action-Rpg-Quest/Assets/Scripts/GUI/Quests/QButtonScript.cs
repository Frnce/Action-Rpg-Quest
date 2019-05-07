using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Advent.Manager;
using Advent.Quests;
using UnityEngine.EventSystems;

namespace Advent.UI
{
    public class QButtonScript : MonoBehaviour, IPointerEnterHandler
    {
        [HideInInspector]
        public int questID;
        [HideInInspector]
        public QuestType questType;
        [HideInInspector] 
        public TMP_Text questTitle;

        public void ShowConfirmation()
        {
            QuestUIManager.instance.ShowConfirmationPanel();

            FindObjectOfType<QuestConfirmationScript>().questID = questID;
            FindObjectOfType<QuestConfirmationScript>().questType = questType;
        }

        //public void ShowAllInfos()
        //{
        //    QuestUIManager.instance.ShowSelectedQuest(questID);

        //    //ACcept button
        //    if (QuestManager.instance.RequestAvailableQuest(questID))
        //    {
        //        QuestUIManager.instance.acceptButton.SetActive(true);
        //        QuestUIManager.instance.acceptButtonScript.GetComponent<QButtonScript>().questID = questID;
        //    }
        //    else
        //    {
        //        QuestUIManager.instance.acceptButton.SetActive(false);
        //    }
        //    //giveup button
        //    if (QuestManager.instance.RequestAcceptedQuest(questID))
        //    {
        //        QuestUIManager.instance.giveupButton.SetActive(true);
        //        QuestUIManager.instance.giveupButtonScript.GetComponent<QButtonScript>().questID = questID;
        //    }
        //    else
        //    {
        //        QuestUIManager.instance.giveupButton.SetActive(false);
        //    }
        //    //complete Button
        //    if (QuestManager.instance.RequestCompleteQuest(questID))
        //    {
        //        QuestUIManager.instance.completeButton.SetActive(true);
        //        QuestUIManager.instance.completeButtonScript.GetComponent<QButtonScript>().questID = questID;
        //    }
        //    else
        //    {
        //        QuestUIManager.instance.completeButton.SetActive(false);
        //    }
        //}
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
            //QuestUIManager.instance.acceptButton.SetActive(false);
            //QuestUIManager.instance.giveupButton.SetActive(false);
            //QuestUIManager.instance.completeButton.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("selected : " + questID);
            QuestUIManager.instance.ShowSelectedQuest(questID);
        }
    }
}