using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class LootPool
    {
        public int MaxSize;
        public Loot Loot;
        public TypeLoot TypeLoot;
    }

    [SerializeField] private LootHolder _lootHolder;
    [SerializeField] private List<LootPool> _lootPrefabs;

    [Header("Enemy")]
    [SerializeField] private int _maxEnemyCount;
    [SerializeField] private List<Transform> _wayPoints;
    [SerializeField] private Enemy _enemyPrefab;

    private Dictionary<TypeLoot, Queue<Loot>> _lootPoolDictionary;
    private Queue<Enemy> _enemyPool;

    private void Start()
    {
        FillEnemyPool();
        FillLootPool();
    }

    public bool TryGetEnemy(out Enemy enemy)
    {
        enemy = _enemyPool.Dequeue();
        _enemyPool.Enqueue(enemy);

        if (enemy.gameObject.activeInHierarchy == true)
        {
            return false;
        }
        else
        {
            enemy.gameObject.SetActive(true);
        }

        return true;
    }

    public bool TryGetDesiredLoot(out Loot loot, TypeLoot desiredType)
    {
        loot = null;

        if (_lootPoolDictionary.ContainsKey(desiredType))
        {
            if (_lootPoolDictionary[desiredType].Count > 0)
            {
                loot = _lootPoolDictionary[desiredType].Dequeue();

                if (loot.gameObject.activeInHierarchy)
                    loot.gameObject.SetActive(true);

                _lootPoolDictionary[desiredType].Enqueue(loot);

                return true;
            }
        }

        return false;
    }

    private void FillEnemyPool()
    {
        _enemyPool = new Queue<Enemy>();

        Enemy currentEnemy;

        for (int i = 0; i < _maxEnemyCount; i++)
        {
            currentEnemy = Instantiate(_enemyPrefab);
            currentEnemy.gameObject.TryGetComponent(out EnemyMover enemyMover);
            enemyMover.GetWayPoints(_wayPoints);
            currentEnemy.gameObject.SetActive(false);
            _enemyPool.Enqueue(currentEnemy);
        }
    }

    private void FillLootPool()
    {
        _lootPoolDictionary = new Dictionary<TypeLoot, Queue<Loot>>();

        foreach (LootPool lootPool in _lootPrefabs)
        {
            Queue<Loot> newLootPool = new Queue<Loot>();

            for (int i = 0; i < lootPool.MaxSize; i++)
            {
                Loot currentLoot = Instantiate(lootPool.Loot);
                currentLoot.transform.SetParent(_lootHolder.transform);
                currentLoot.gameObject.SetActive(false);
                newLootPool.Enqueue(currentLoot);
            }

            _lootPoolDictionary.Add(lootPool.TypeLoot, newLootPool);
        }
    }
}