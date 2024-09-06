using System;
using System.Collections;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private CharacterRenderer _characterRenderer;
    [SerializeField] private HealthRenderer _healthRenderer;

    private float _regenerationDelay = 0.5f;
    private Coroutine _regenerationDelayCoroutine;

    public int CurrentHealth { get; private set; }

    public event Action Died;

    private void Awake ()
    {
        _healthRenderer.GetMaxHealth(_maxHealth);
    }

    private void OnEnable()
    {
        Recovery();
    }

    public void Recovery()
    {
        CurrentHealth = _maxHealth;

        _healthRenderer.ChangeAllHealthInfo(CurrentHealth);
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            damage *= -1;

        CurrentHealth = Math.Clamp(CurrentHealth - damage, 0, _maxHealth);
        _characterRenderer.TakeDamageColor();

        _healthRenderer.ChangeAllHealthInfo(CurrentHealth);

        if (CurrentHealth == 0)
        {
            Died?.Invoke();
        }
    }

    public void Regeneration(int desiredCount)
    {
        if (_regenerationDelayCoroutine != null)
            StopCoroutine(_regenerationDelayCoroutine);

        _regenerationDelayCoroutine = StartCoroutine(RegenerationDelay(desiredCount));
    }

    private IEnumerator RegenerationDelay(int desiredCount)
    {
        for (int i = 0; i < desiredCount; i++)
        {
            if (CurrentHealth < _maxHealth)
            {
                CurrentHealth++;
                _characterRenderer.RegenerationColor();
                _healthRenderer.ChangeAllHealthInfo(CurrentHealth);
            }
            else
            {
                StopCoroutine(_regenerationDelayCoroutine);
            }

            yield return new WaitForSeconds(_regenerationDelay);
        }
    }
}
