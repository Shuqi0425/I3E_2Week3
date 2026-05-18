using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Tooltip("Points awarded to player when this collectible is picked up")]
    public int score = 1;

    public int BeCollected()
    {
        // 1. inform GameManager that an item has been collected, so it can update the count and check for win condition
        if (GameManager.instance != null)
        {
            GameManager.instance.ItemCollected();
        }

        Destroy(gameObject);
        return score;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("A physical collision has been detected! Collector name: " + collision.gameObject.name);

            // Link with the message script on the player to award points
            var playerMessage = collision.gameObject.GetComponent<message>();
            if (playerMessage != null)
            {
                
            }

            BeCollected();
        }
    }
}