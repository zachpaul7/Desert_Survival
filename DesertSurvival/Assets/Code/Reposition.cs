using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    public string areaTag = "Area";  // Trigger ���� �±�
    public string objectTag = "Enemy";  // ���ġ�� ������Ʈ �±�
    public float moveDistance = 25f;  // �̵� �Ÿ�
    public float randomRange = 3f;  // ���� �̵� ����

    Collider2D coll;

    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(areaTag) && transform.CompareTag(objectTag))
        {
            // �÷��̾���� �Ÿ� ���
            Vector3 playerPos = GameManager.instance.playerTransform.position;
            Vector3 myPos = transform.position;
            float diffx = Mathf.Abs(playerPos.x - myPos.x);
            float diffy = Mathf.Abs(playerPos.y - myPos.y);

            // �÷��̾� ���� ���
            Vector3 playerDir = GameManager.instance.animate.inputVec;

            // ������Ʈ �̵�
            if (coll.enabled)
            {
                transform.Translate(playerDir * moveDistance +
                    new Vector3(Random.Range(-randomRange, randomRange), Random.Range(-randomRange, randomRange), 0f));
            }
        }
    }
}
