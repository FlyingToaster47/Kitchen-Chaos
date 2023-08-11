using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeliveryResultUI : MonoBehaviour {

    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Color successColor;
    [SerializeField] private Color failColor;
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failSprite;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        DeliveryManager.Instance.OnRecipeSuccess += RecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += RecipeFailed;

        gameObject.SetActive(false);
    }

    private void RecipeSuccess(object sender, System.EventArgs e) {
        gameObject.SetActive(true);

        backgroundImage.color = successColor;
        iconImage.sprite = successSprite;
        messageText.text = "Delivery\nSuccess";

        animator.SetTrigger("Popup");
    }
    private void RecipeFailed(object sender, System.EventArgs e) {
        gameObject.SetActive(true);

        backgroundImage.color = failColor;
        iconImage.sprite = failSprite;
        messageText.text = "Delivery\nFailed";

        animator.SetTrigger("Popup");
    }

}
