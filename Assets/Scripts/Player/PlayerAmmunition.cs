using UnityEngine;

public class PlayerAmmunition : Ammunition
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out AmmunitionLoot ammunitionLoot))
        {
            ReplenishmentBulletsCount(ammunitionLoot.AmunitionCount);
            ammunitionLoot.gameObject.SetActive(false); 
        }
    }
}