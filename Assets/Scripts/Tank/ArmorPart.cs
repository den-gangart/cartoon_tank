using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPart : MonoBehaviour
{
    [SerializeField] private Health _health;

    public void TakeDamage(float damageAmount)
    {
        _health.TakeDamage(damageAmount);
    }
}
