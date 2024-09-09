using System;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    private const string NUMBER_POPUP = "NumberPopup";
    [SerializeField] private TextMeshProUGUI countdownText;

    private Animator animator;
    private int previousCountdown;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
        Hide();
    }

    private void GameManager_OnGameStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGameCountdownToStart())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameCountdownToStart())
        {
            int countdown = Mathf.CeilToInt(GameManager.Instance.GetCountdownToStartTimer());
            countdownText.text = countdown.ToString();

            if (countdown != previousCountdown)
            {
                previousCountdown = countdown;
                animator.SetTrigger(NUMBER_POPUP);
                SoundManager.Instance.PlayCountdownSound();
            }
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
