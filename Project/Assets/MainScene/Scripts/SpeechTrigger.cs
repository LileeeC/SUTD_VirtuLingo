using UnityEngine;

public class SpeechTrigger : MonoBehaviour
{
    public GameObject targetElement; // The element to activate
    public GameObject button;        // The button to deactivate

    public void ActivateTarget()
    {
        if (targetElement != null)
        {
            targetElement.SetActive(true); // Activate the target element
        }

        if (button != null)
        {
            button.SetActive(false); // Deactivate the button
        }
    }
}
