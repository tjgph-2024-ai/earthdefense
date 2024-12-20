using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : Base_Enemy
{
    // test
    public Base_Tower target;

    // health
    public float max_health;
    public float current_health;

    // damage
    public float max_damage;

    // action
    public float action_interval;
    public float action_range;

    // time
    private float time = 0;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= action_interval)
        {
            Action(null);
            time = 0;
        }
        if (current_health <= 0)
        {
            Die();
        }
    }


    public override void Action(Base_Tower _target) // attack enemy
    {
        // Debug.Log("Truck is attacking to " + _target.name);

        // Co2 をだす処理
        target.TakeDamage(max_damage);
    }

    public override void TakeDamage(float damage)
    {
        current_health -= damage;
    }

    public override void Heal(float _value)
    {
        current_health += _value;
        if (current_health > max_health)
        {
            current_health = max_health;
        }
    }


    public override void Die()
    {
        // Debug.Log("Truck is dead");
        Destroy(gameObject);
    }
}