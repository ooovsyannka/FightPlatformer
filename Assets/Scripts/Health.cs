using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private CharacterRenderer _characterRenderer;

    private Coroutine _regenerationDelayCoroutine;
    private float _regenerationDelay = 0.5f;

    public int CurrentHealth { get; private set; }

    public event Action Died;

    private void OnEnable()
    {
        Recovery();
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        _characterRenderer.TakeDamageColor();

        if (CurrentHealth <= 0)
        {
            Died?.Invoke();
        }
    }

    public void Recovery() => CurrentHealth = _maxHealth;

    public void Regeneration(int desiredCount)
    {
        if (_regenerationDelayCoroutine != null)
            StopCoroutine(_regenerationDelayCoroutine);

        _regenerationDelayCoroutine = StartCoroutine(RegenerationDelay(desiredCount));
    }

    private IEnumerator RegenerationDelay(int desiredCount)
    {
        for (int i = 0; i <= desiredCount; i++)
        {
            if (CurrentHealth < _maxHealth)
            {
                CurrentHealth++;
                _characterRenderer.RegenerationColor();
            }
            else
            {
                StopCoroutine(_regenerationDelayCoroutine);
            }

            yield return new WaitForSeconds(_regenerationDelay);
        }
    }
}
