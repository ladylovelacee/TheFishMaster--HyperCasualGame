using UnityEngine;
using DG.Tweening;

public class Fish : MonoBehaviour
{
    CircleCollider2D fishCollider;
    Tweener tweener;
    [SerializeField] FishData fishData = default;
 
    public FishData FishData
    {
        get
        {
            return fishData;
        }
    }

    void Awake()
    {
        fishCollider = GetComponent<CircleCollider2D>();
    }

    public void ResetFish()
    {
        if (tweener != null)
            tweener.Kill(false);

        fishCollider.enabled = true;

        float num = Random.Range(fishData.MinLength, fishData.MaxLength);
        Vector3 position = transform.position;
        position.y = num;
        position.x = ScreenCalculator.Instance.ScreenWeight;
        transform.position = position;

        float num2 = 1;
        float y = UnityEngine.Random.Range(num - num2, num + num2);
        Vector2 v = new Vector2(-position.x, y);

        float num3 = 3;
        float delay = UnityEngine.Random.Range(0, 2 * num3);
        tweener = transform.DOMove(v, num3, false).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear).SetDelay(delay).OnStepComplete(delegate
        {
            Vector3 localScale = transform.localScale;
            localScale.x = -localScale.x;
            transform.localScale = localScale;
        });
    }

    public void Hooked()
    {
        fishCollider.enabled = false;
        tweener.Kill(false);
    }
}
