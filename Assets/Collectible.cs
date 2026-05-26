using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Tooltip("Points awarded to player when this collectible is picked up")]
    public int score = 1;

    private AudioSource collectibleAudio;

    void Start()
    {
        collectibleAudio = GetComponent<AudioSource>();
    }

    public void PlayCollectibleSound()
    {
        if (collectibleAudio != null)
        {
            if (collectibleAudio.clip != null)
            {
                collectibleAudio.Play();
                Destroy(gameObject, collectibleAudio.clip.length);
            }
            else
            {
                collectibleAudio.Play();
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.Log("No AudioSource component found on this collectible.");
            Destroy(gameObject);
        }
    }

    // only called by the player when they interact with this collectible
    public int BeCollected()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ItemCollected();
        }

        // play audio and stop after destroyed itself
        PlayCollectibleSound();

        return score;
    }
}