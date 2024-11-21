using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace PW
{
    public class Flashcard : MonoBehaviour
    {
        public Text questionText;
        public Text[] optionTexts;
        public GameObject questionPanel;
        public GameObject[] answerPanels;

        private bool answersShown = false; // To check if answers are already shown

        private void Awake()
        {
            // Hide answer panels initially
            foreach (var panel in answerPanels)
            {
                panel.SetActive(false);
            }
        }

        public void SetQuestion(string question)
        {
            questionText.text = question;
        }

        public void SetOptions(string[] options)
        {
            for (int i = 0; i < optionTexts.Length; i++)
            {
                if (i < options.Length)
                {
                    optionTexts[i].text = options[i];
                }
                else
                {
                    optionTexts[i].text = "";
                }
            }
        }

        public void ShowQuestionPanel()
        {
            questionPanel.SetActive(true);
        }

        public IEnumerator ShowAnswerPanelsWithDelay(float delay)
        {
            if (!answersShown) // Ensure this runs only once
            {
                answersShown = true;
                yield return new WaitForSeconds(delay);
                foreach (var panel in answerPanels)
                {
                    panel.SetActive(true);
                    // Add interaction event listener to each panel
                    var interactable = panel.GetComponent<XRBaseInteractable>();
                    if (interactable != null)
                    {
                        interactable.selectEntered.AddListener((interactor) => OnAnswerSelected(panel));
                    }
                }
            }
        }

        private void OnAnswerSelected(GameObject selectedPanel)
        {
            foreach (var panel in answerPanels)
            {
                // Change the color of the selected panel to green
                if (panel == selectedPanel)
                {
                    panel.GetComponent<Image>().color = Color.green;
                }
                else
                {
                    panel.GetComponent<Image>().color = Color.white;
                }
            }
        }
    }
}
