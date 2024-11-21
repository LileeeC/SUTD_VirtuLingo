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

    void Start()
    {
        HideBubble(); 
        LoadDialogue("Assets/dialogue.txt"); // Put path to dialogue text file here
        randomize = GetComponent<RandomizeScript>();
        randomize.hideBubbles();
    }

    void LoadDialogue(string path)
    {
        if (File.Exists(path))
        {
            lines = new List<string>(File.ReadAllLines(path));
        }
        else
        {
            Debug.LogError("Dialogue file not found!");
        }
    }

    public void ShowBubble()
    {
        if (lines != null && lines.Count > 0)
        {
            text.text = lines[currentLineIndex]; // Set the message to the current line
            speechBubble.SetActive(true); // Show the bubble
        }
    }

    public void NextLine()
    {
        if (lines != null && currentLineIndex < lines.Count)
        {
            ShowBubble(); // Show main speech bubble
            randomize.randomizeOptions(lines, currentLineIndex); // Show user choices
            currentLineIndex = currentLineIndex + 5; // Increment by n
        }
        else
        {
            randomize.hideBubbles(); // Hide user choices
            HideBubble(); // Hide main speech bubble
        }
    }

    public void HideBubble()
    {
        speechBubble.SetActive(false);
        currentLineIndex = 0;
    }
}