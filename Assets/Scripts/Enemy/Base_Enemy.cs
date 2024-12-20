using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Base_Enemy : MonoBehaviour
{
    // public float health;
    // public float damage;
    // public float action_interval;
    // public float action_range;

    public abstract void Action(Base_Tower _tower); // attack, buff, debuff, etc.
    public abstract void TakeDamage(float damage); // take damage
    public abstract void Heal(float _value); // heal

    public abstract void Die(); // die
}
