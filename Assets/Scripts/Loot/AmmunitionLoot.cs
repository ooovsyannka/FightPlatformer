using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class AmmunitionLoot : Loot
{
    [SerializeField] private int _maxAmunitionCount = 15;

    public int AmunitionCount { get {  return _maxAmunitionCount; } }

    public override TypeLoot GetTypeLoot()
    {
        return TypeLoot.AmmunitionLoot;
    }
}