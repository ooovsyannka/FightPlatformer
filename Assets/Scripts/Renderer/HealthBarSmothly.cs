using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class HealthBarSmothly : HealthRenderer
{
    [SerializeField] private float _smoothlyValue;

    private Slider _bar;
    private Coroutine _smoothlyChangeHealthCoroutine;

    private void Awake()
    {
        _bar = GetComponent<Slider>();
    }

    public override void ChangeHealthInfo(int currentHealth)
    {
        if (_smoothlyChangeHealthCoroutine != null)
            StopCoroutine(_smoothlyChangeHealthCoroutine);

        _smoothlyChangeHealthCoroutine = StartCoroutine(SmoothlyChangeHealthBarValue(GetHealthPrecentage(currentHealth)));
    }

    private IEnumerator SmoothlyChangeHealthBarValue(float currentHealth)
    {
        while (_bar.value != currentHealth)
        {
            _bar.value = Mathf.MoveTowards(_bar.value, currentHealth, _smoothlyValue);

            yield return null;
        }
    }
}
