using UnityEngine;
using TMPro;
using System.Collections;
public class HydrationManager : MonoBehaviour
{
    public int maxHydration = 100;
    public float decreaseInterval = 10f;
    public float damageAmount = 0.5f;
    public TextMeshProUGUI HydrationText;
    public TextMeshProUGUI resetPromptText; // Reference to the reset prompt text field
    public TextMeshProUGUI rechargeText; // Reference to the recharge health text field

    private float timeSinceLastDecrease;
    private int currentHydration;
    private Coroutine rechargeCoroutine;

    void Start()
    {
        currentHydration = maxHydration;
        UpdateHydrationText();
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
        currentHydration -= Mathf.RoundToInt(damage);
        UpdateHydrationText();

        if (currentHydration <= 0)
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
        while (currentHydration <= 0)
        {
            rechargeText.text = "Recharge Hydration. Drink Something!!!";
            yield return null;
        }
        rechargeText.text = "";
    }
    void UpdateHydrationText()
    {
        HydrationText.text = currentHydration.ToString();
    }
    public void ResetHealth()
    {
        currentHydration = maxHydration;
        UpdateHydrationText();
        StartCoroutine(ShowResetPrompt());

    }
        IEnumerator ShowResetPrompt()
    {
        resetPromptText.text = "Hydration Reset!";
        yield return new WaitForSeconds(2f);
        resetPromptText.text = ""; // Clear the reset prompt text after 2 seconds
    }
}
