using TMPro;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _ammunitionText;
    [SerializeField] private Health _playerHealth;
    [SerializeField] private Ammunition _ammunition;

    private void Update()
    {
        ShowHealthCount();
        ShowAmmunitionCount();
    }

    private void ShowHealthCount() =>
        _healthText.text = $"HEALTH - {_playerHealth.CurrentHealth}";

    private void ShowAmmunitionCount() =>
       _ammunitionText.text = $"AMMUNITION \n {_ammunition.CurrentBulletCountInClip} / {_ammunition.CurrentAllBulletCount}";
}