using TMPro;
using UnityEngine;

public class MainHUD : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        UpdateTimer(gameManager.GetCurrentTimer);
    }

    private void UpdateTimer(float timer)
    {
        timerText.text = timer.ToString();
    }
}
