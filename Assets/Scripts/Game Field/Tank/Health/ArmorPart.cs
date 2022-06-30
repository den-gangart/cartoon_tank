using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPart : MonoBehaviour
{
    public event EventHandler<float> ArmorHit;
    public event Action Kill;

    public void TakeDamage(float damageAmount)
    {
        ArmorHit?.Invoke(this, damageAmount);
    }

    public void ForceKill()
    {
        Kill?.Invoke();
    }
}
