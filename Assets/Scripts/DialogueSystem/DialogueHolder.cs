using System.Collections;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueHolder : MonoBehaviour
    {
        private IEnumerator dialogueSeq;
        private bool dialogueFinished;

        public GameObject quizScript;
        public string subjectName;

        public GameObject ExceptSituationsCanvas;

        private void OnEnable()
        {
            dialogueSeq = DialogueSequence();
            ExceptSituationsCanvas.SetActive(false);
            StartCoroutine(dialogueSeq);
           
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Deactivate();
                gameObject.SetActive(false);
                ExceptSituationsCanvas.SetActive(true);
                StopCoroutine(dialogueSeq);
            }
        }

        private IEnumerator DialogueSequence()
        {

            foreach (Test test in DataHolder.doneTests )
            {
            if ( test.subject == subjectName )
                dialogueFinished = true;
            }

            if (!dialogueFinished)
            {
                for (int i = 2; i < transform.childCount-1; i++)
                {
                    Deactivate();
                    transform.GetChild(i).gameObject.SetActive(true);
                    yield return new WaitUntil(() => transform.GetChild(i).GetComponent<DialogueLine>().finished);
                }
                quizScript.SetActive(true);
            }
            else
            {
                int index = transform.childCount - 1;
                Deactivate();
                transform.GetChild(index).gameObject.SetActive(true);
                yield return new WaitUntil(() => transform.GetChild(index).GetComponent<DialogueLine>().finished);
            }
            dialogueFinished = true;
            gameObject.SetActive(false);

        }

        private void Deactivate()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}