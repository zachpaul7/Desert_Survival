using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSystem : MonoBehaviour
{
    public static MessageSystem instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] GameObject dmgMessage;

    List<TMPro.TextMeshPro> messagePool;

    int objectCount = 10;
    int count;

    private void Start()
    {
        messagePool = new List<TMPro.TextMeshPro> ();

        for (int i = 0; i < objectCount; i++)
        {
            Populate();
        }
    }

    public void Populate()
    {
        GameObject go = Instantiate(dmgMessage, transform);
        messagePool.Add(go.GetComponent<TMPro.TextMeshPro>());
        go.SetActive(false);
    }

    public void PostMessage(string text,Vector3 worldPostion)
    {
        messagePool[count].gameObject.SetActive(true);
        messagePool[count].transform.position = worldPostion;
        messagePool[count].text = text;
        count += 1;

        if (count >= objectCount)
        {
            count = 0;
        }
    }
}
