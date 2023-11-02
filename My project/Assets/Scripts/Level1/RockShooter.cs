using UnityEngine;

public class RockShooter : MonoBehaviour
{
    public GameObject rockPrefab; // Prefab of the rock object to be shot
    public Transform shootPoint; // Point where rocks will be spawned
    public float shootInterval = 1f; // Time interval between rock shots
    private bool playerInRange; // Flag to check if player is in the shooter's range
    public float shootSpeed = 1f; // Speed  for rock shots

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            InvokeRepeating("ShootRock", 0f, shootInterval);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            CancelInvoke("ShootRock");
        }
    }

    private void ShootRock()
    {
        if (playerInRange)
        {
            GameObject rock = Instantiate(rockPrefab, shootPoint.position, Quaternion.identity);
            // Add force to shoot the rock (customize the force as needed)
            Instantiate(rockPrefab,shootPoint);
        }
    }
}
