using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[Serializable]
public class EnemyStats
{
    public int hp = 999;
    public int dmg = 1;
    public int experience_reward = 400;
    public float moveSpeed = 1f;
    private EnemyStats stats;

    public EnemyStats(EnemyStats stats)
    {
        this.hp = stats.hp;
        this.dmg = stats.dmg;
        this.experience_reward = stats.experience_reward;
        this.moveSpeed = stats.moveSpeed;
    }

    internal void ApplyProgress(float progress)
    {
        this.hp = (int)(hp * progress);
        this.dmg = (int)(dmg * progress);
    }
}

public class Enemy : MonoBehaviour, IDamageable
{
    Transform targetDestination;
    GameObject targetGameObject;
    Character targetCharacter;

    public EnemyStats stats;

    SpriteRenderer spriter;
    Rigidbody2D rgb2d;
    Collider2D coll;
    Animator animator;

    bool isLive = true;

    WaitForFixedUpdate wait;

    void Awake()
    {
        coll = GetComponent<Collider2D>();
        rgb2d = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
    }

    public void SetTarget(GameObject target)
    {
        targetGameObject = target;
        targetDestination = target.transform;
    }

    void FixedUpdate()
    {
        if (!isLive || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        Vector3 direction = (targetDestination.position - transform.position).normalized;
        rgb2d.velocity = direction * stats.moveSpeed;

        spriter.flipX = targetDestination.position.x < rgb2d.position.x; 
    }

    internal void SetStats(EnemyStats stats)
    {
        this.stats = new EnemyStats(stats);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == GameManager.instance.playerTransform.gameObject)
        {
            Attack();
        }
    }

    void Attack()
    {
        if (targetCharacter == null)
        {
            targetCharacter = targetGameObject.GetComponent<Character>();
        }

        targetCharacter.TakeDamage(stats.dmg);
    }

    public void TakeDamage(int dmg)
    {
        stats.hp -= dmg;

        StartCoroutine(KnockBack());

        if (stats.hp > 0)
        {
            EnemyAudioManager.instance.PlayHit();
            animator.SetTrigger("Hit");
        }
        else 
        {
            isLive = false;
            coll.enabled = false;
            rgb2d.simulated = false;
            spriter.sortingOrder = 1;
            EnemyAudioManager.instance.PlayDead();
            animator.SetBool("Dead",true);
        }
    }

    IEnumerator KnockBack()
    {
        yield return wait; // 다음 물리 프레임 까지

        Vector3 playerPos = GameManager.instance.playerTransform.transform.position;
        Vector3 dirVec = transform.position - playerPos;

        rgb2d.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse);
    }

    public void Dead()
    {
        GetComponent<DropOnDestroy>().CheckDrop();
        Destroy(gameObject);
    }

    internal void UpdateStatsForProgress(float progress)
    {
        stats.ApplyProgress(progress);
    }
}
