using UnityEngine;

public abstract class HealthRenderer : MonoBehaviour
{
    public float MaxHealth {  get; private set; }

    public abstract void ChangeHealthInfo(int currentHealth);

    public void GetMaxHealth(int maxHealth)
    {
        MaxHealth = maxHealth;
    }

    public float GetHealthPrecentage(float currentHeatlh)
    {
        float maxPrecentage = 100;

        return currentHeatlh / MaxHealth * maxPrecentage;
    }
}
