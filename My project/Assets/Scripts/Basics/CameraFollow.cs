using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public string playerTag = "Player"; // Tag of the player object to follow
    public Vector3 offset = new Vector3(0f, 1f, -10f); // Offset of the camera from the player

    void Update()
    {
        // Find the GameObject with the specified tag
        GameObject player = GameObject.FindWithTag(playerTag);

        // Check if the player object is found
        if (player != null)
        {
            // Set the camera position to the player's position with the offset
            transform.position = player.transform.position + offset;
        }
        else
        {
            // Log a warning if the player object is not found
            Debug.LogWarning("Player object with tag '" + playerTag + "' not found.");
        }
    }
}
