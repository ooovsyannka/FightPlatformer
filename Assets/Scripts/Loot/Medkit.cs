using UnityEngine;

public class Medkit : Loot
{
    private int _countToRegeneration = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.GetComponent<Health>().Regeneration(_countToRegeneration);

            gameObject.SetActive(false);
        }
    }
}