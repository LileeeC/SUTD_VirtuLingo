using System.Collections;
using UnityEngine;

namespace PW
{
    public class FlashcardGenerator : MonoBehaviour
    {
        public GameObject flashcardPrefab;
        public Transform UIParentForFlashcards;
        public int MaxFlashcards = 5;
        private int currentFlashcardCount;

        private void Start()
        {
            StartCoroutine(GenerateFlashcardRoutine(3f));
        }

        private IEnumerator GenerateFlashcardRoutine(float intervalTime)
        {
            while (true)
            {
                if (currentFlashcardCount < MaxFlashcards)
                {
                    GenerateFlashcard();
                    yield return new WaitForSeconds(intervalTime);
                }
                else
                {
                    yield return null;
                }
            }
        }

        private void GenerateFlashcard()
        {
            var newFlashcard = Instantiate(flashcardPrefab, UIParentForFlashcards).GetComponent<Flashcard>();
            newFlashcard.SetQuestion("What is your question?");
            newFlashcard.SetOptions(new string[] { "Option 1", "Option 2", "Option 3", "Option 4" });

            newFlashcard.ShowQuestionPanel();
            StartCoroutine(newFlashcard.ShowAnswerPanelsWithDelay(5f));

            currentFlashcardCount++;
        }
    }
}
