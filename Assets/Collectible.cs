using UnityEngine;

public class Collectible : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // check if the collided object has the tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("A physical collision has been detected! Collector name: " + collision.gameObject.name);


            // call GameManager to update the score or collected items count
            if (GameManager.instance != null)
            {
                GameManager.instance.ItemCollected();
            }

            // item destroyed after collection
            Destroy(gameObject);
        }
    }
}