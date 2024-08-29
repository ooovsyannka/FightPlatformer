public class PlayerBullet : Bullet 
{
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.GetComponent<Health>().TakeDamage(ChanceCrit());
            gameObject.SetActive(false);
        }
    }
}