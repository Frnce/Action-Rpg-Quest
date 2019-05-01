using Advent.Dialogues;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Advent.Manager
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager instance;
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
            DontDestroyOnLoad(gameObject);
        }
        [SerializeField]
        private TMP_Text dialogText = null;
        [SerializeField]
        private TMP_Text dialogNameText = null;
        [SerializeField]
        private GameObject dialogPanel = null;
        private Queue<string> sentences;

        private void Start()
        {
            sentences = new Queue<string>();
            dialogPanel.SetActive(false);
        }

        public void StartDialogue(Dialogue dialogue)
        {
            dialogPanel.SetActive(true);
            dialogNameText.text = dialogue.name;

            sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

            DisplayNextSentence();
        }
        public void DisplayNextSentence()
        {
            if(sentences.Count == 0)
            {
                EndDialogue();
                return;
            }
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeEffectText(sentence));
        }
        private IEnumerator TypeEffectText(string sentence)
        {
            dialogText.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                dialogText.text += letter;
                yield return null;
            }
        }
        void EndDialogue()
        {
            Debug.Log("End of convo");
            dialogPanel.SetActive(false);
        }
    }
}
