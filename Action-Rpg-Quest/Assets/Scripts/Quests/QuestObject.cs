using Advent.Controller;
using Advent.Manager;
using Advent.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Advent.Quests
{
    public class QuestObject : MonoBehaviour
    {
        public List<int> availableQuestIDs = new List<int>();
        public List<int> receivableQuestIDs = new List<int>();

        public GameObject questMarker;
        public Image theImage;

        public Sprite questAvailableSprite;
        public Sprite questReceivableSprite;

        // Start is called before the first frame update
        void Start()
        {
            SetQuestMaker();
        }

        public void SetQuestMaker()
        {
            if (QuestManager.instance.CheckCompletedQuest(this))
            {
                questMarker.SetActive(true);
                theImage.sprite = questReceivableSprite;
                theImage.color = Color.yellow;
            }
            else if (QuestManager.instance.CheckAvailableQuest(this))
            {
                questMarker.SetActive(true);
                theImage.sprite = questAvailableSprite;
                theImage.color = Color.yellow;
            }
            else if (QuestManager.instance.CheckAcceptedQuest(this))
            {
                questMarker.SetActive(true);
                theImage.sprite = questReceivableSprite;
                theImage.color = Color.gray;
            }
            else
            {
                questMarker.SetActive(false);
            }
        }
    }
}
