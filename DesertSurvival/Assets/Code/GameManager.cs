using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Transform playerTransform;
    public Animate animate;

    void Awake()
    {
        instance = this;
    }
}
