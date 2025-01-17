using TMPro; // Include the TMP namespace
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    // Reference to the TextMeshProUGUI component
    public TextMeshProUGUI healthText;

    // A variable that you want to display
    private int health;

    Health playerHealth;

    void Start()
    {
        playerHealth = FindObjectOfType<Health>();

        // If you haven't manually linked the TextMeshProUGUI object in the Inspector
        if (healthText == null)
        {
            healthText = GetComponent<TextMeshProUGUI>();
        }

        // Initially set the text to show the score
    }

    void Update()
    {
        health = playerHealth.health;
        UpdateScoreText();
    }

    // Method to update the displayed score in the TextMeshProUGUI component
    void UpdateScoreText()
    {
        healthText.text = health.ToString(); // Display the score
    }
}
