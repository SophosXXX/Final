using UnityEngine;
using TMPro;
using System.Collections;
public class HealthManager : MonoBehaviour
{
    public int maxHealth = 100;
    public float decreaseInterval = 10f;
    public float damageAmount = 0.5f;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI resetPromptText; // Reference to the reset prompt text field
    public TextMeshProUGUI rechargeText; // Reference to the recharge health text field

    private float timeSinceLastDecrease;
    private int currentHealth;
    private Coroutine rechargeCoroutine;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthText();
        StartCoroutine(DecreaseHealthOverTime());
    }

    IEnumerator DecreaseHealthOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(decreaseInterval);
            TakeDamage(damageAmount);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= Mathf.RoundToInt(damage);
        UpdateHealthText();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Your death logic goes here
        Debug.Log("Player died");
        rechargeCoroutine = StartCoroutine(ShowRechargeMessage());
    }

    IEnumerator ShowRechargeMessage()
    {
        while (currentHealth <= 0)
        {
            rechargeText.text = "Recharge Health";
            yield return null;
        }
        rechargeText.text = "";
    }

    void UpdateHealthText()
    {
        healthText.text = currentHealth.ToString();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthText();
        StartCoroutine(ShowResetPrompt());
    }

    IEnumerator ShowResetPrompt()
    {
        resetPromptText.text = "Health Reset!";
        yield return new WaitForSeconds(2f);
        resetPromptText.text = ""; // Clear the reset prompt text after 2 seconds
    }
}
