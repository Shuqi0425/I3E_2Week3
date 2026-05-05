using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager instance;

    public int totalItems = 0; // Total number of items to collect
    public int collectedItems = 0; // Number of items collected
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
            {
        // Count total items in the scene at the start
        totalItems = GameObject.FindGameObjectsWithTag("Collectible").Length;
        Debug.Log("Total items to collect: " + totalItems);
    }

    public void ItemCollected()
    {
        collectedItems++;
        Debug.Log("Item collected! Total collected: " + collectedItems + "/" + totalItems);
    }

    public bool AllItemsCollected()
    {
        return collectedItems >= totalItems;
    }
}