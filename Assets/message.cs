using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using TMPro; // 联动文本框需要的命名空间
using UnityEngine.UI; // 【PPT核心：控制 UI Image 组件必须引入这个命名空间】

public class message : MonoBehaviour
{
    private int collCount; // 对应 PPT 里的 currentCoinCount 计数器，用来追踪收集数量和图片索引
    [SerializeField] private int playerScore = 0;
    [SerializeField] private float collectRange = 5f;
    GameObject currentCollectible;

    [Header("UI Text Settings")]
    [SerializeField] private TextMeshProUGUI scoreText; // 联动 TextMeshPro 文本框

    // 【PPT 核心变量声明】：公开 Icon 槽位和图片数组槽位
    [Header("UI Image Settings (From PPT)")]
    [SerializeField] private Image coinTrackingIcon;       // 对应 PPT 里的 coinTrackingIcon
    [SerializeField] private Sprite[] coinTrackingSprites; // 对应 PPT 里的 coinTrackingSprites 数组

    void Start()
    {
        // 游戏开始，初始化分数和所有 UI（包括文字和第一张初始图片）
        UpdateScoreUI();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GoalArea" && collCount >= 6)
        {
            print("Player entered trigger zone with " + collCount + " collectibles");
        }
    }

    // press e to interact with nearby collectibles
    void OnInteract()
    {
        // search for nearby collectibles within collectRange
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

        // find the nearest collectible and interact with it
        if (nearest != null)
        {
            currentCollectible = nearest;
            var collectibleComp = currentCollectible.GetComponent<Collectible>();

            if (collectibleComp != null)
            {
                // call the collectible script, let it notify the GameManager and destroy itself, while returning its own score
                int scoreValue = collectibleComp.BeCollected();

                print("Interacting with " + currentCollectible.name + " (+" + scoreValue + " points)");

                // update player's local data: add score
                playerScore += scoreValue;

                // 【完全遵循 PPT 逻辑】：先增加计数器 (We increase currentCoinCount first)
                collCount++;

                // 统一刷新所有 UI (包括文字和图片切换)
                UpdateScoreUI();
            }
        }
        else
        {
            print("No collectible in range.");
        }
    }

    // update the score display and image sprite on the UI
    private void UpdateScoreUI()
    {
        // 1. refresh the score text display (if we have a reference to it)
        if (scoreText != null)
        {
            scoreText.text = "Score: " + playerScore;
        }

        // 2. change the coin tracking icon sprite based on the current collectible count 
        if (coinTrackingIcon != null && coinTrackingSprites != null && coinTrackingSprites.Length > 0)
        {
            // add an if statement to make sure that currentCoinCount does not go higher than the number of sprites we have
            if (collCount < coinTrackingSprites.Length)
            {
                // use currentCoinCount as the index to select the corresponding sprite from the array and update the UI Image component
                coinTrackingIcon.sprite = coinTrackingSprites[collCount];
            }
            else
            {
                // force the icon to show the last sprite if we have collected more than the maximum count (to prevent index out of range errors)
                coinTrackingIcon.sprite = coinTrackingSprites[coinTrackingSprites.Length - 1];
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 1f, 0f, 0.25f);
        Gizmos.DrawSphere(transform.position, collectRange);
    }
}