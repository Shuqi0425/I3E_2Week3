using UnityEngine;

public class FinishZone : MonoBehaviour
{
    //only trigger when the player enters the finish zone
    void OnTriggerEnter(Collider other)
    {

        // check if the collided object has the tag "Player"
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance.AllItemsCollected())
            {
                Debug.Log("MISSION COMPLETE: You collected all items!");
            }
            else
            {
                Debug.Log("MISSION INCOMPLETE: Keep searching...");
            }
        }
    }
}