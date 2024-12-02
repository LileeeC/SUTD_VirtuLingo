using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RandomizeScript : MonoBehaviour
{
    public List<GameObject> ListOfOptions; // List containing the GameObjects which are the user chocies
    public List<TextMeshProUGUI> ListOfTextObjects; // List cointaing the text of each of the above objects

    private GameObject currentCorrectChoice;
    private bool isTimerRunning = false;

    private SpeechBubble speech;
    private string currentDialogue;

    private GameObject userChoice;

    void Start()
    {
        speech = GetComponent<SpeechBubble>();
    }

    public void randomizeOptions(List<string> lines, int currentIndex, List<int> correctChoices)
    {
        if (lines != null && (currentIndex + 1) != lines.Count)
        {
            int[] randomChoiceIndexes = Shuffle(ListOfOptions.Count); // Get some random indexes for the choices
            currentDialogue = lines[currentIndex];

            for (int i = 0; i < ListOfOptions.Count; i++) // Show both the user choices objects and text
            {
                getTextImage(ListOfOptions[i]).color = Color.white;
                int lineIndex = currentIndex + randomChoiceIndexes[i];

                ListOfOptions[i].SetActive(true);
                ListOfTextObjects[i].text = lines[lineIndex];

                if (lineIndex == (correctChoices[currentIndex / 4])) // CHANGE THIS VALUE AS ITS NOT DYNAMIC
                {
                    currentCorrectChoice = ListOfOptions[i];
                }
            }
        }

        else
        {
            hideBubbles();
        }
    }

    public void hideBubbles()
    {
        foreach (GameObject option in ListOfOptions) // Hide both the user choices objects and text
        {
            option.SetActive(false);
        }
    }

    private int[] Shuffle(int numOfOptions)
    {
        int[] array = new int[numOfOptions];

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = i + 1;
        }

        // Shuffler loop
        for (int i = array.Length - 1; i > 0; i--)
        {

            int j = Random.Range(0, i + 1);

            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        return array;
    }

    public void HandleClick(GameObject clickedObject)
    {
        if (!isTimerRunning) // Check if the timer is already running.
        {
            userChoice = clickedObject;
            StartCoroutine(StartTimer(3.5f, "...", 1f, true, Color.black)); // Start a 10-second timer.
        }
    }

    IEnumerator StartTimer(float timerLength, string msg, float typespeed, bool s, Color textColor)
    {
        isTimerRunning = true; // Set the flag to indicate the timer is running.
        speech.ShowBubble(msg, typespeed, textColor);

        yield return new WaitForSeconds(timerLength); // Wait for the specified time.

        if (s)
        {
            CheckChoice();
        }
        else
        {
            speech.NextLine();
        }

        isTimerRunning = false; // Reset the flag after the timer completes.
    }

    IEnumerator IncorrectMsg(float timerLength, string msg, float typespeed, bool s, Color textColor)
    {
        isTimerRunning = true; // Set the flag to indicate the timer is running.
        speech.ShowBubble(msg, typespeed, textColor);

        yield return new WaitForSeconds(timerLength); // Wait for the specified time.
        
        speech.ShowBubble(currentDialogue, 0.05f, Color.black);
        
        isTimerRunning = false; // Reset the flag after the timer completes.
    }

    void CheckChoice()
    {
        Image textImage = userChoice.GetComponent<Image>();

        if (userChoice == currentCorrectChoice)
        {
            getTextImage(userChoice).color = Color.green;
            StartCoroutine(StartTimer(2, "Correct!", 0.05f, false, Color.green));
        }

        else
        {
            getTextImage(userChoice).color = Color.red;
            StartCoroutine(IncorrectMsg(2, "Incorrect!", 0.05f, false, Color.red));
        }
    }

    Image getTextImage(GameObject obj)
    {
        return obj.GetComponent<Image>();
    }
}
