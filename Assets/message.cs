using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class message : MonoBehaviour
{
    private int collCount;
    [SerializeField] private int playerScore = 0;
    [SerializeField] private float collectRange = 5f;
    GameObject currentCollectible;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GoalArea" && collCount >= 6)
        {
            print("Player entered trigger zone with " + collCount + " collectibles");
        }
    }

    void OnInteract()
    {
        // search for collectibles within range
        Collider[] hits = Physics.OverlapSphere(transform.position, collectRange);
        GameObject nearest = null;
        float minDist = float.MaxValue;

        foreach (var hit in hits)
        {
            if (hit.gameObject.CompareTag("Collectible"))
            {
                float d = Vector3.SqrMagnitude(hit.transform.position - transform.position);
                if (d < minDist)
                {
                    minDist = d;
                    nearest = hit.gameObject;
                }
            }
        }

        if (nearest != null)
        {
            currentCollectible = nearest;
            var collectibleComp = currentCollectible.GetComponent<Collectible>();

            if (collectibleComp != null)
            {
                // inform the collectible it has been collected and get the score value
                int scoreValue = collectibleComp.BeCollected();

                print("Interacting with " + currentCollectible.name + " (+" + scoreValue + " points)");
                playerScore += scoreValue;
                collCount++;
            }
        }
        else
        {
            print("No collectible in range.");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 1f, 0f, 0.25f);
        Gizmos.DrawSphere(transform.position, collectRange);
    }
}