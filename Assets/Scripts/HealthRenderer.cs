using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthRenderer : MonoBehaviour
{
    [SerializeField] private Slider _healthBarPrecentage;
    [SerializeField] private Slider _healthBarSmoothly;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField]private float _smoothly ;

    private int _maxHealth;

    private Coroutine _coroutine;

    public void GetMaxHealth(int maxHealth)
    {
        _maxHealth = maxHealth;
    }

    public void ChangeAllHealthInfo(int currentHealth)
    {
        ChangeHealthBarPrecentageValue(currentHealth);
        ChangeHealthBarSmoothlyValue(currentHealth);
        ChangeHealthText(currentHealth);
    }

    private void ChangeHealthBarPrecentageValue(int currentHealth)
    {
        _healthBarPrecentage.value = GetHealthPrecentage(currentHealth);
    }

    private void ChangeHealthBarSmoothlyValue(float currentHealth)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(SmoothlyChangeHealthBarValue(GetHealthPrecentage(currentHealth)));
    }

    private void ChangeHealthText(int currentHealth)
    {
        _healthText.text = $"HEALTH - {currentHealth} / {_maxHealth}";
    }

    private float GetHealthPrecentage(float currentHeatlh)
    {
        float maxPrecentage = 100;

        return currentHeatlh / _maxHealth * maxPrecentage;
    }

    private IEnumerator SmoothlyChangeHealthBarValue(float currentHealth)
    {
        while (_healthBarSmoothly.value != currentHealth)
        {
            _healthBarSmoothly.value = Mathf.MoveTowards(_healthBarSmoothly.value, currentHealth, _smoothly );

            yield return null;
        }
    }
}
