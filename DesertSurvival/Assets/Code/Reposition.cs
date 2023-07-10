using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    public string areaTag = "Area";  // Trigger 영역 태그
    public string objectTag = "Enemy";  // 재배치할 오브젝트 태그
    public float moveDistance = 25f;  // 이동 거리
    public float randomRange = 3f;  // 랜덤 이동 범위

    Collider2D coll;

    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(areaTag) && transform.CompareTag(objectTag))
        {
            // 플레이어와의 거리 계산
            Vector3 playerPos = GameManager.instance.playerTransform.position;
            Vector3 myPos = transform.position;
            float diffx = Mathf.Abs(playerPos.x - myPos.x);
            float diffy = Mathf.Abs(playerPos.y - myPos.y);

            // 플레이어 방향 계산
            Vector3 playerDir = GameManager.instance.animate.inputVec;

            // 오브젝트 이동
            if (coll.enabled)
            {
                transform.Translate(playerDir * moveDistance +
                    new Vector3(Random.Range(-randomRange, randomRange), Random.Range(-randomRange, randomRange), 0f));
            }
        }
    }
}
