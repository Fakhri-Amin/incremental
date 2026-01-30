using Eggtato.Utility;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float levelTimer = 5;
    [SerializeField] private ResultUI resultUI;

    private float currentTimer;
    private bool isGamePlaying;

    public int GetCurrentTimer => (int)currentTimer;

    void Start()
    {
        currentTimer = levelTimer;
        isGamePlaying = true;
    }

    void Update()
    {
        if (!isGamePlaying) return;

        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0)
        {
            Pause();
        }
    }

    private void Pause()
    {
        isGamePlaying = false;
        GameInput.Instance.SwitchActionMap("UI");
        resultUI.Show();
        currentTimer = levelTimer;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        resultUI.Hide();
        GameInput.Instance.SwitchActionMap("Player");
        Cursor.visible = false;
        isGamePlaying = true;
    }
}
