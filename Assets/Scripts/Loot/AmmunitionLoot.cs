public class AmmunitionLoot : Loot
{
    private int _maxCountAmmunitionToRecovory = 15;

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.GetComponent<Ammunition>().ReplenishmentBulletsCount(_maxCountAmmunitionToRecovory);

            gameObject.SetActive(false);
        }
    }
}
