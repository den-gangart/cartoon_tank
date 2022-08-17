using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorCompositor : MonoBehaviour
{
    [SerializeField] private List<ArmorPart> _armorParts;
    [SerializeField] private Health _health;

    private void Start()
    {
        foreach (ArmorPart armorPart in _armorParts)
        {
            armorPart.ArmorHit += OnTakedDamage;
            armorPart.Kill += OnKillForce;
        }
    }

    private void OnTakedDamage(object sender, float damage)
    {
        _health.TakeDamage(damage);
    }

    private void OnKillForce()
    {
        _health.Kill();
    }

    private void OnDestroy()
    {
        foreach(ArmorPart armorPart in _armorParts)
        {
            armorPart.ArmorHit -= OnTakedDamage;
        }
    }
}
