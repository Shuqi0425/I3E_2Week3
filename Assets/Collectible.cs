using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Tooltip("Points awarded to player when this collectible is picked up")]
    public int score = 1;

    // only called by the player when they interact with this collectible
    public int BeCollected()
    {
        // 1. inform the GameManager that an item has been collected 
        if (GameManager.instance != null)
        {
            GameManager.instance.ItemCollected();
        }

        // 2. item should destroy itself after being collected
        Destroy(gameObject);

        // 3. return the score value of this collectible to the player so they can update their score UI
        return score;
    }
}