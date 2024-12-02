using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleted : MonoBehaviour
{
    //Create array of gameObjects
    public GameObject[] sentenceBlks;

    //Used to check if all blocks are in place
    public bool blkInPlace;

    //Flag to ensure level completed only once
    private bool completed = false;

    // Update is called once per frame
    void Update()
    {
        //Check if all five blocks have been snapped into place, and make sure we haven't completed level yet
        if(levelCompleted() == true && completed == false) //levelCompleted() is below
        {
            Debug.Log(levelCompleted());
            //Enter next level code here
            //SceneManager.LoadScene("EasyLifeScene");
        }
    }

    //Use a for loop to iterate through the array of blocks
    //Checks to see if isSnapped is set to true on each one
    //Returns false if any one of the five are false
    //If all five return true, then set to true
    private bool levelCompleted() {
        for(int i = 0; i < sentenceBlks.Length; i++)
        {
            blkInPlace = sentenceBlks[i].GetComponent<SnapObject>().isSnapped;
            if(blkInPlace == false)
            {
                return false;
            }
        }
        return true;
    }
}
