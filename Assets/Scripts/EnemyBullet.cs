public class EnemyBullet : Bullet
{
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.GetComponent<Health>().TakeDamage(ChanceCrit());
            gameObject.SetActive(false);
        }
    }
}
