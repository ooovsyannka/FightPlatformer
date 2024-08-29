using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyMover), typeof(EnemyHealth))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private VolumeByDistance _volumeByDistance;
    [SerializeField] private EnemyCombat _enemyCombat;
    [SerializeField] private EnemyAnimation _enemyAnimation;
    [SerializeField] private Sounds _sounds;

    private float _timeDieDelay = 1.5f;
    private bool _isDie = false;
    private EnemyMover _enemyMover;
    private EnemyHealth _enemyHealth;
    private Loot _loot;
    private CapsuleCollider2D _enemyCollider;
    private EnemyPool _enemyPool;

    private void Awake()
    {
        _enemyMover = GetComponent<EnemyMover>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _enemyCollider = transform.GetComponent<CapsuleCollider2D>();
    }

    private void OnEnable()
    {
        _isDie = false;
        _enemyHealth.Died += Die;
        _enemyCollider.enabled = true;
    }

    private void OnDisable()
    {
        _enemyHealth.Died -= Die;
    }

    private void Update()
    {
        if (_isDie == false)
        {
            bool isCombat = _enemyCombat.IsCombat;

            if (isCombat)
            {
                _enemyMover.SetTarget(_enemyCombat.Player.transform.position);
                _enemyCombat.TryAttackPlayer();
            }

            _enemyMover.SetCombateState(isCombat);
        }

        _volumeByDistance.SetVolumeValue(_enemyCombat.Player);
        _sounds.PlaySound(_enemyMover.EnemyState);
        _enemyAnimation.PlayAnimation(_enemyMover.IsMove, _isDie);
    }

    public void GetLoot(Loot loot) => _loot = loot;

    public void GetEnemyPool(EnemyPool enemyPool)
    {
        _enemyPool = enemyPool;
    }

    private void Die()
    {
        if (_isDie == false)
        {
            _isDie = true;

            StartCoroutine(DieDelay());
        }
    }

    private IEnumerator DieDelay()
    {
        _enemyMover.StopMove();
        transform.GetComponent<CapsuleCollider2D>().enabled = false;

        yield return new WaitForSeconds(_timeDieDelay);

        _enemyPool.ReturnEnemyInPool(this);
        DropLoot();
        gameObject.SetActive(false);
    }

    private void DropLoot()
    {
        if (_loot == null)
            return;

        _loot.gameObject.SetActive(true);
        _loot.transform.position = transform.position;
        _loot = null;
    }
}
