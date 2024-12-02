using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class SpeechBubble : MonoBehaviour
{
    public GameObject speechBubble; // Assign speech bubble GameObject
    public TextMeshProUGUI text; // Assign text
    private RandomizeScript randomize; // Script to display options

    private List<string> lines; // List to store lines of dialogue
    private int currentLineIndex = 0; // Track the current line index
    private string checkCharacter = "#";
    private List<int> correctChoices = new List<int>();
    public float typeSpeed = 0.05f; // Delay between characters

    private Coroutine typingCoroutine; // Store reference to the current coroutine

    void Start()
    {
        HideBubble();
        LoadDialogue("Assets/MainScene/DialogueText/dialogue.txt"); // Put path to dialogue text file here
        randomize = GetComponent<RandomizeScript>();
        randomize.hideBubbles();
        NextLine();
    }

    void LoadDialogue(string path)
    {
        if (File.Exists(path))
        {
            lines = new List<string>(File.ReadAllLines(path));

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Contains(checkCharacter.ToString()))
                {
                    correctChoices.Add(i);
                    lines[i] = lines[i].Substring(1);
                }
            }
        }
        else
        {
            Debug.LogError("Dialogue file not found!");
        }
    }

    public void ShowBubble(string speech, float speed, Color textColor)
    {
        if (lines != null && lines.Count > 0)
        {
            speechBubble.SetActive(true); // Show the bubble
            text.color = textColor; 

            // Stop any existing typewriter coroutine
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }

            // Start a new typewriter coroutine
            typingCoroutine = StartCoroutine(TypeText(speech, speed));
        }
    }

    IEnumerator TypeText(string line, float speed)
    {
        text.text = ""; // Clear the text
        foreach (char c in line)
        {
            text.text += c; // Add one character at a time
            yield return new WaitForSeconds(speed); // Wait for the specified delay
        }

        // Reset the coroutine reference after completion
        typingCoroutine = null;
    }

    public void NextLine()
    {
        if (lines != null && currentLineIndex < lines.Count)
        {
            ShowBubble(lines[currentLineIndex], typeSpeed, Color.black); // Show main speech bubble
            randomize.randomizeOptions(lines, currentLineIndex, correctChoices); // Show user choices
            currentLineIndex = currentLineIndex + 4; // CHANGE THIS VALUE AS ITS NOT DYNAMIC
        }
        else
        {
            randomize.hideBubbles(); // Hide user choices
            HideBubble(); // Hide main speech bubble
        }
    }

    public void HideBubble()
    {
        // Stop the current typewriter coroutine if it's running
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        speechBubble.SetActive(false);
        currentLineIndex = 0;
    }
}
