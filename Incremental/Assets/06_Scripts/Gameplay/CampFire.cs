using MoreMountains.Feedbacks;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    [SerializeField] private Transform collectPoint;
    [SerializeField] private MMFeedbacks collectFeedbacks;

    public Transform CollectPoint => collectPoint;

    public void Collect()
    {
        collectFeedbacks.PlayFeedbacks();
        GameManager.Instance.AddCollectable();
    }
}
