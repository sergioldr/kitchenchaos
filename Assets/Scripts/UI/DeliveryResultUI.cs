using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    private const string POPUP = "Popup";
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Color successColor;
    [SerializeField] private Color failedColor;
    [SerializeField] private Sprite successIcon;
    [SerializeField] private Sprite failedIcon;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeDelivered += DeliveryManager_OnRecipeDelivered;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;

        Hide();
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        backgroundImage.color = failedColor;
        iconImage.sprite = failedIcon;
        messageText.text = "Recipe\nFailed!";
        Show();
        animator.SetTrigger(POPUP);
    }

    private void DeliveryManager_OnRecipeDelivered(object sender, EventArgs e)
    {
        backgroundImage.color = successColor;
        iconImage.sprite = successIcon;
        messageText.text = "Recipe\nDelivered!";
        Show();
        animator.SetTrigger(POPUP);
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
