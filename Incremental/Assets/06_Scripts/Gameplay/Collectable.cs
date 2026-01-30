using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Collectable : MonoBehaviour
{
    [SerializeField] private float coinMoveSpeed = 5;

    public void FlyToCollector()
    {
        StartCoroutine(FlyToCollectorRoutine());
    }

    private IEnumerator FlyToCollectorRoutine()
    {
        Transform collectPoint = GameManager.Instance.CollectorPoint;
        Vector3 startPos = transform.position;
        // Vector3 startScale = transform.localScale;

        float distance = Vector3.Distance(startPos, collectPoint.position);
        float duration = distance / coinMoveSpeed; // ⏱ dynamically computed duration

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            transform.position = Vector3.Lerp(startPos, collectPoint.position, t);
            // Optional shrink:
            // transform.localScale = Vector3.Lerp(startScale, Vector3.zero, t);
            yield return null;
        }

        transform.DOScale(0, .2f).OnComplete(() =>
        {
            GameManager.Instance.AddBone();
            Destroy(gameObject);
        });

        // AudioManager.Instance.PlayAddFundSound();
    }
}
