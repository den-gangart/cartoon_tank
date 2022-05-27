using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPart : MonoBehaviour
{
    public event EventHandler<float> ArmorHit;

    public void TakeDamage(float damageAmount)
    {
        ArmorHit?.Invoke(this, damageAmount);
    }
}
