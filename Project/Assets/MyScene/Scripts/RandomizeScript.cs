using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class RandomizeScript : MonoBehaviour
{
    public List<GameObject> ListOfOptions; // List containing the GameObjects which are the user chocies
    public List<TextMeshProUGUI> ListOfTextObjects; // List cointaing the text of each of the above objects

    public void randomizeOptions(List<string> lines, int currentIndex)
    {
        if (lines != null && (currentIndex + 1) != lines.Count)
        {
            int[] randomChoiceIndexes = Shuffle(ListOfOptions.Count); // Get some random indexes for the choices

            for (int i = 0; i < ListOfOptions.Count; i++) // Show both the user choices objects and text
            {
                ListOfOptions[i].SetActive(true);
                ListOfTextObjects[i].text = lines[currentIndex + randomChoiceIndexes[i]];
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
}
