using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Slider))]

public class HealthBarPrecentage : HealthRenderer
{
    private Slider _bar;

    private void Awake()
    {
        _bar = GetComponent<Slider>();
    }

    public override void ChangeHealthInfo(int currentHealth)
    {
        _bar.value = GetHealthPrecentage(currentHealth);
    }
}
