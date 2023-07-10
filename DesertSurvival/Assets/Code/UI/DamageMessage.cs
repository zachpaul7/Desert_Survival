using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMessage : MonoBehaviour
{
    [SerializeField] float TimeToLive = 1f;
    float tt1 = 2f;
    private void OnEnable()
    {
        tt1 = TimeToLive;
    }
    private void Update()
    {
        tt1 -= Time.deltaTime;

        if(tt1 < 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
