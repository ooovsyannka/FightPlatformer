using System.Collections;
using UnityEngine;

public class CharacterRenderer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _characterSprite;
    [SerializeField] private float _delay;

    private Color _characterColor;
    private Coroutine _smothlyChangeColorCoroutine;

    private void Start()
    {
        _characterColor = _characterSprite.color;
    }

    public void TakeDamageColor()
    {
        if (_smothlyChangeColorCoroutine != null)
            StopCoroutine(_smothlyChangeColorCoroutine);

        _smothlyChangeColorCoroutine = StartCoroutine(SmothlyChangeColor(Color.red));
    }

    public void RegenerationColor()
    {
        if (_smothlyChangeColorCoroutine != null)
            StopCoroutine(_smothlyChangeColorCoroutine);

        _smothlyChangeColorCoroutine = StartCoroutine(SmothlyChangeColor(Color.green));
    }

    private IEnumerator SmothlyChangeColor(Color desiredColor)
    {
        _characterSprite.color = desiredColor;

        yield return new WaitForSeconds(_delay);

        _characterSprite.color = _characterColor;
    }
}