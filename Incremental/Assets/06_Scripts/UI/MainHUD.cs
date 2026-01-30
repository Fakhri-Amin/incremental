using TMPro;
using UnityEngine;

public class MainHUD : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text boneCollectedText;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        UpdateTimer(gameManager.CurrentTimer);
        UpdateBoneCollected(gameManager.BoneCollected);
    }

    private void UpdateTimer(float timer)
    {
        timerText.text = timer.ToString();
    }

    private void UpdateBoneCollected(int boneCollected)
    {
        boneCollectedText.text = boneCollected.ToString();
    }
}
