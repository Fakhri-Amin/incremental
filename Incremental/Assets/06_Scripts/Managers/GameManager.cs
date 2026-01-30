using System.Collections;
using Eggtato.Utility;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float levelTimer = 5;
    [SerializeField] private int boneCollected = 0;
    [SerializeField] private ResultUI resultUI;
    [SerializeField] private Transform collectorPoint;

    private float currentTimer;
    private bool isGamePlaying;

    public int CurrentTimer => (int)currentTimer;
    public int BoneCollected => boneCollected;
    public Transform CollectorPoint => collectorPoint;

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
            StartCoroutine(PauseRoutine());
        }
    }

    private IEnumerator PauseRoutine()
    {
        isGamePlaying = false;
        CharacterSpawner.Instance.ReturlAllObjects();
        resultUI.Show();
        currentTimer = levelTimer;
        Cursor.visible = true;
        GameInput.Instance.SwitchActionMap("UI");

        yield return null;

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

    public void AddBone()
    {
        boneCollected++;
    }
}
