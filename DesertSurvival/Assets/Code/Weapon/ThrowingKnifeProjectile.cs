using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowingKnifeProjectile : MonoBehaviour
{
    Vector3 direction;
    [SerializeField] float speed;
    [SerializeField] float attackSize = 0.1f;
    public int dmg = 5;
    float tt1 = 6f;
    

    public void SetDirection(float dir_x, float dir_y)
    {
        direction = new Vector3 (dir_x, dir_y);

        if (dir_x < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = scale.x * -1;
            transform.localScale = scale;
        }
    }

    bool hitDetected = false;

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if(Time.frameCount % 6 == 0)
        {
            Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, attackSize);
            foreach (Collider2D c in hit)
            {
               IDamageable enemy = c.GetComponent<IDamageable>();
                if (enemy != null)
                {
                    PostDamage(dmg, transform.position);
                    enemy.TakeDamage(dmg);
                    hitDetected = true;
                    break;
                }
            }
            if (hitDetected == true)
            {
                Destroy(gameObject);
            }
        }

        tt1 -= Time.deltaTime;
        if(tt1 < 0f)
        {
            Destroy(gameObject);
        }
    }

    public void PostDamage(int damage, Vector3 worldPosition)
    {
        MessageSystem.instance.PostMessage(dmg.ToString(),worldPosition);
    }
}
