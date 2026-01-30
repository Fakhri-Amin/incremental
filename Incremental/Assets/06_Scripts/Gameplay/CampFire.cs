using MoreMountains.Feedbacks;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    [SerializeField] private MMFeedbacks collectFeedbacks;

    public void Collect()
    {
        collectFeedbacks.PlayFeedbacks();
        GameManager.Instance.AddCollectable();
    }
}
