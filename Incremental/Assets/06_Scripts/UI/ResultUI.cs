using UnityEngine;
using UnityEngine.UI;

public class ResultUI : UIBase
{
    [SerializeField] private Button continueButton;

    private void Awake()
    {
        continueButton.onClick.AddListener(() =>
        {
            GameManager.Instance.Unpause();
        });
    }
}
