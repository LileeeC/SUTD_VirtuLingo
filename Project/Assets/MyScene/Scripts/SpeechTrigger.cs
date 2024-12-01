using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
    private string targetObjectName = "Player";
    private bool alreadyTriggered = false;
    public GameObject mainSpeech;

    // This function is called when a collision happens
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the target object by tag or name
        if (collision.gameObject.name == targetObjectName && !alreadyTriggered)
        {
            alreadyTriggered = true;
            mainSpeech.GetComponent<SpeechBubble>().NextLine();
        }
    }
}
