using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class Medkit : Loot
{
    [SerializeField] private int _maxCountToRecovery = 4;

    public override TypeLoot GetTypeLoot()
    {
        return TypeLoot.Medkit;
    }

    public int CountToRecovery { get { return _maxCountToRecovery; } }
}