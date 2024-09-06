using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]

public class HealthBarText : HealthRenderer 
{
    private TextMeshProUGUI _bat;

    private void Awake()
    {
        _bat = GetComponent<TextMeshProUGUI>();
    }

    public override void ChangeHealthInfo(int currentHealth)
    {
        _bat.text = $"HEALTH - {currentHealth} / {MaxHealth}";
    }
}
