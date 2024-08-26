using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmunition : MonoBehaviour
{
    [SerializeField] private int _maxCount;
    [SerializeField] private Bullet _bulletPrefab;

    private Queue<Bullet> _bulletsPool;

    private void Awake()
    {
        _bulletsPool = new Queue<Bullet>();
    }

    private void Start()
    {
        CreatBullets();
    }

    public void TryFillClip(ref Queue<Bullet> bullets, int maxBulletInClip)
    {
        if (_bulletsPool.Count == 0)
            return;

        if (_bulletsPool.Count > maxBulletInClip)
        {

            if (bullets.Count == 0)
            {
                FillClip(ref bullets, maxBulletInClip);
            }
            else
            {
                int desiredCount = maxBulletInClip - bullets.Count;

                FillClip(ref bullets, desiredCount);
            }
        }
        else
        {
            FillClip(ref bullets, _bulletsPool.Count);
        }
    }

    private void FillClip(ref Queue<Bullet> bullets, int maxBulletInClip)
    {
        for (int i = maxBulletInClip; i >= 0; i--)
        {
            bullets.Enqueue(_bulletsPool.Dequeue());
        }
    }

    private void CreatBullets()
    {
        Bullet bullet;

        for (int i = _maxCount; i >= 0; i--)
        {
            bullet = Instantiate(_bulletPrefab);
            _bulletsPool.Enqueue(bullet);
            bullet.transform.parent = transform;
            bullet.gameObject.SetActive(false);
        }
    }
}