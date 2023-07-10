using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOnDestroy : MonoBehaviour
{
    [SerializeField] List<GameObject> dropItemPrefab;
    [SerializeField] [Range(0f,1f)] float chance = 1f;

    bool isQuitting = false;

    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    public void CheckDrop()
    {
        if (isQuitting) { return; }

        if(dropItemPrefab.Count <= 0)
        {
            Debug.Log("no item drop");
            return;
        }

        if(Random.value < chance)
        {
            GameObject toDrop = dropItemPrefab[Random.Range(0, dropItemPrefab.Count)];

            if (toDrop == null)
            {
                Debug.Log("DropOnDestroy");
                return;
            }

            SpawnManager.instance.SpawnObject(transform.position, toDrop);
        }

    }
}
