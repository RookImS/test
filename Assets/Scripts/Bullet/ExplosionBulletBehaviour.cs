﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBulletBehaviour : BulletBehaviour
{
    public float life;

    private float attackStartTime;
    private float age;
    private float attackTime;
    private int attackNumber;

    private void Awake()
    {
        m_BulletData = GetComponent<BulletData>();
        enemyList = new List<GameObject>();

        age = 0f;
        attackTime = 0.7f;
        attackNumber = 0;
    }
    private void Start()
    {
        StartCoroutine(DestroyAttack());
    }

    private void Update()
    {
        if (age < attackTime)
        {
            age += Time.deltaTime;
            if (attackStartTime < age)
            {
                if (enemyList.Count > 0)
                    Attack();
                if (age >= attackTime)
                    hitCollider.SetActive(false);
            }
        }
    }

    public override void Init(TowerStatSystem t_stat)
    {
        m_BulletData.Init(t_stat);
        transform.localScale = new Vector3(m_BulletData.stats.stats.attackRange * 2, 1, m_BulletData.stats.stats.attackRange * 2);
    }
    private IEnumerator DestroyAttack()
    { 
        yield return new WaitForSeconds(life);
        Destroy(this.gameObject);
    }

    protected override void Attack()
    {
        if (attackNumber != enemyList.Count)
        {
            for (int i = attackNumber; i < enemyList.Count; i++)
            {
                attackNumber++;
                if (enemyList[i] == null)
                    continue;

                enemyList[i].GetComponent<EnemyBehaviour>().Damage(m_BulletData.stats.stats.damage);
            }
        }
        //Destroy(this.gameObject);
    }
}