using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HookController : MonoBehaviour
{
    [SerializeField] Camera mainCamera = default;
    [SerializeField] Transform hookedTransform = default;

    Tweener cameraTween;
    Collider2D hookCollider;

    List<Fish> hookedFishes;

    float ropeLength;
    float strength;

    float hookStarterPosition = -6.0f;
    int fishCount = 0;

    void Awake()
    {
        Game.isGameOver = false;
        Game.isMoving = false;
        hookCollider = GetComponent<Collider2D>();
        hookedFishes = new List<Fish>();
    }
 
    void Update()
    {
        Game.isMoving = Input.GetMouseButton(0);
        HookMoveX();
    }

    void HookMoveX()
    {
        if(!Game.isGameOver && Game.isMoving)
        {
            Vector3 input = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 position = transform.position;
            position.x = input.x;
            transform.position = position;
        }
    }

    public void CastAFishingLine()
    {
        ropeLength = GameManager.Instance.length - 20f;
        strength = GameManager.Instance.strength;
        fishCount = 0;

        cameraTween = mainCamera.transform.DOMoveY(ropeLength, 1 + (-ropeLength * 0.1f) * 0.25f, false)
            .OnUpdate(delegate
            {
                if (mainCamera.transform.position.y <= -11.0f)
                {
                    transform.SetParent(mainCamera.transform);
                }
            })
            .OnComplete(delegate
            {
                hookCollider.enabled = true;
                cameraTween = mainCamera.transform.DOMoveY(0, (-ropeLength * 0.1f) * 5f, false)
                .OnUpdate(delegate
                {
                    if(mainCamera.transform.position.y >= -25f)
                        StopFishing();
                });
            });

        ScreensManager.Instance.ChangeScreen(Screens.GAME);

        hookCollider.enabled = false;
        Game.isGameOver = false;
        hookedFishes.Clear();
    }

    void StopFishing()
    {
        Game.isGameOver = true;
        cameraTween.Kill(false);
        cameraTween = mainCamera.transform.DOMoveY(0, 2, false)
            .OnUpdate(delegate
            {
                if (mainCamera.transform.position.y >= -11.0f)
                {
                    transform.SetParent(null);
                    transform.position = new Vector2(transform.position.x, hookStarterPosition);
                }
            })
            .OnComplete(delegate
            {
                transform.position = Vector2.down * 6;
                hookCollider.enabled = true;

                int price = 0;
                for (int i = 0; i < hookedFishes.Count; i++)
                {
                    hookedFishes[i].transform.SetParent(null);
                    hookedFishes[i].ResetFish();
                    price += hookedFishes[i].FishData.FishPrice;
                }
                
                GameManager.Instance.totalGain = price;
                ScreensManager.Instance.ChangeScreen(Screens.END);
            });       
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Target") && fishCount != strength)
        {
            fishCount++;
            Fish caughtFish = collision.GetComponent<Fish>();
            hookedFishes.Add(caughtFish);
            caughtFish.Hooked();

            collision.transform.SetParent(transform);
            collision.transform.position = hookedTransform.position;
            collision.transform.rotation = hookedTransform.rotation;
            collision.transform.localScale = Vector3.one;

            collision.transform.DOShakeRotation(5, Vector3.forward * 45, 10, 90, false)
                .SetLoops(1, LoopType.Yoyo)
                .OnComplete(delegate
                {
                    collision.transform.rotation = Quaternion.identity; //0,0,0
                });
            if (fishCount == strength)
                StopFishing();
        }
    }
}
