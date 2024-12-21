using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Tree_Tower : Base_Tower
{
    // test
    // public Base_Enemy target;
    public float heal_value;

    // health
    public float max_health;
    public float current_health;

    // damage
    public float max_damage;

    // action
    public float action_interval;
    public float action_range;
    private float absorbableSqrDistance;

    // level
    public int level;

    // time
    private float time;

    // game manager
    private GameManager gameManager;

    void Awake()
    {
        time = 0;
        level = 1;
        max_health = 100;
        current_health = max_health;
        max_damage = 5;
        action_interval = 1;
        action_range = 10;
        heal_value = 0.1f;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        absorbableSqrDistance = Mathf.Pow(action_range, 2);
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= action_interval)
        {
            Base_Enemy target = Find();
            if (target != null)
            {
                Debug.Log(target);
                Action(target);
            }
            time = 0;
        }
        if (current_health <= 0)
        {
            Die();
        }
    }


    public override void Action(Base_Enemy _target) // attack enemy
    {
        Debug.Log("Tree is attacking to " + _target.name);

        // Co2 を吸収する処理
        _target.TakeDamage(max_damage);
        Heal(heal_value);
    }

    public override void Heal(float _value)
    {
        current_health += _value;
        if (current_health > max_health)
        {
            current_health = max_health;
        }
    }

    public override void TakeDamage(float damage)
    {
        current_health -= damage;
    }

    [ContextMenu("Upgrade")]
    public override void Upgrade()
    {
        level++;
        max_health += 10;
        max_damage += 10;
        action_interval -= 0.1f;

        // absorbableSqrDistance を更新
        absorbableSqrDistance = Mathf.Pow(action_range, 2);
    }

    [ContextMenu("Sell")]
    public override void Sell()
    {
        Destroy(gameObject);
    }

    public override void Die()
    {
        Destroy(gameObject);
    }

    public override Base_Enemy Find()
    {
        Base_Enemy target = null;
        float targetPathPos = 0;
        for (int i = 0; i < gameManager.parent_for_enemy.childCount; i++)
        {
            Transform enemy = gameManager.parent_for_enemy.GetChild(i);
            float enemyPathPos = enemy.GetComponent<CinemachineDollyCart>().m_Position;

            if (Mathf.Abs(Vector3.Distance(transform.position, enemy.position)) < action_range && targetPathPos < enemyPathPos)
            {
                target = enemy.GetComponent<Base_Enemy>();
                targetPathPos = enemyPathPos;
            }
        }
        return target;
    }
}
