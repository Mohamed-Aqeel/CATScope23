using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HP : MonoBehaviour
{
    public int maxLives = 3; // Maximum lives of the player
    private int currentLives; // Current lives of the player

    public Image[] livesImages; // An array of Image components representing player lives

    private void Start()
    {
        currentLives = maxLives;
        UpdateLivesUI();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("Boss")) // Check if player collides with trap or boss
        {
            TakeDamage();
            UpdateLivesUI();
        }
    }
    public void TakeDamage()
    {
        currentLives--;

        if (currentLives <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Perform actions when the player runs out of lives, e.g., show game over screen, reset level, etc.
        Debug.Log("Player has died!");
        // Add your game over logic here

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateLivesUI()
    {
        // Iterate through the livesImages array and disable images for lost lives
        for (int i = 0; i < livesImages.Length; i++)
        {
            if (i < currentLives)
            {
                livesImages[i].enabled = true; // Enable image for remaining lives
            }
            else
            {
                livesImages[i].enabled = false; // Disable image for lost lives
            }
        }
    }
}
