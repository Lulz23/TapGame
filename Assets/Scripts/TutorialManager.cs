using UnityEngine;
using UnityEngine.UI; // Required for UI elements
using TMPro; // Required for TextMeshPro

public class TutorialManager : MonoBehaviour
{
    public GameObject messagePanel; // UI panel for showing tutorial messages
    public TextMeshProUGUI messageText; // TextMeshPro element for displaying messages

    void Start()
    {
        // Ensure that the panel and text fields are assigned
        if (messagePanel == null || messageText == null)
        {
            Debug.LogError("MessagePanel or MessageText is not assigned in the Inspector!");
        }
    }

    // Highlights a UI element by adding an Outline component dynamically
    public void Highlight(GameObject obj)
    {
        // Check if the object already has an Outline component
        Outline outline = obj.GetComponent<Outline>();
        if (outline == null)
        {
            // Add the Outline component if it doesn't exist
            outline = obj.AddComponent<Outline>();
        }

        // Customize the Outline appearance
        outline.effectColor = Color.yellow; // Set the highlight color to yellow
        outline.effectDistance = new Vector2(5, 5); // Set the thickness of the glow
    }

    // Displays a tutorial message on the screen
    public void ShowMessage(string message)
    {
        // Show the message panel
        messagePanel.SetActive(true);

        // Update the text with the provided message
        messageText.text = message;

        // Automatically hide the message after 3 seconds
        Invoke(nameof(HideMessage), 3f);
    }

    // Hides the tutorial message
    public void HideMessage()
    {
        if (messagePanel.activeSelf) // Check if the panel is active
        {
            messagePanel.SetActive(false); // Hide the panel
        }
    }
}
