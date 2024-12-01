using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageSwitcher : MonoBehaviour
{
    public Image slideshowImage; // The Image component for the slideshow
    public Button nextButton; // Button to go to the next image
    public Button backButton; // Button to go to the previous image
    public Sprite[] images; // Array of images for the slideshow
    public float transitionDuration = 1f; // Duration of the slide animation
    public float slideDistance = 500f; // Distance to slide images horizontally

    private int currentIndex = 0;
    private Coroutine transitionCoroutine;
    private RectTransform imageRectTransform;

    void Start()
    {
        if (images.Length > 0)
        {
            slideshowImage.sprite = images[currentIndex];
        }

        imageRectTransform = slideshowImage.GetComponent<RectTransform>();

        UpdateButtonStates();
    }

    public void SwitchToNextImage()
    {
        if (transitionCoroutine == null && currentIndex < images.Length - 1)
        {
            currentIndex++;
            transitionCoroutine = StartCoroutine(SlideShowTransition(images[currentIndex], 1)); // 1 for next
        }
    }

    public void SwitchToPreviousImage()
    {
        if (transitionCoroutine == null && currentIndex > 0)
        {
            currentIndex--;
            transitionCoroutine = StartCoroutine(SlideShowTransition(images[currentIndex], -1)); // -1 for previous
        }
    }

    private IEnumerator SlideShowTransition(Sprite nextImage, int direction)
    {
        // Move the current image off-screen (to the left or right)
        Vector3 originalPosition = imageRectTransform.localPosition;
        Vector3 offScreenPosition = new Vector3(originalPosition.x + (direction * slideDistance), originalPosition.y, originalPosition.z);

        // Slide current image out of the screen
        float timer = 0f;
        while (timer < transitionDuration)
        {
            timer += Time.deltaTime;
            float percentage = timer / transitionDuration;
            imageRectTransform.localPosition = Vector3.Lerp(originalPosition, offScreenPosition, percentage); // Slide out
            yield return null;
        }

        // Switch to the next image and reset position
        slideshowImage.sprite = nextImage;
        imageRectTransform.localPosition = new Vector3(originalPosition.x - (direction * slideDistance), originalPosition.y, originalPosition.z);

        // Slide the next image in
        timer = 0f;
        while (timer < transitionDuration)
        {
            timer += Time.deltaTime;
            float percentage = timer / transitionDuration;
            imageRectTransform.localPosition = Vector3.Lerp(imageRectTransform.localPosition, originalPosition, percentage); // Slide in
            yield return null;
        }

        // Update button states and end the transition
        UpdateButtonStates();
        transitionCoroutine = null;
    }

    private void UpdateButtonStates()
    {
        // Disable the "Back" button if on the first image
        backButton.interactable = currentIndex > 0;

        // Disable the "Next" button if on the last image
        nextButton.interactable = currentIndex < images.Length - 1;
    }
}
