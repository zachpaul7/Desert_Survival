using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExpPickUpObject : MonoBehaviour, IPickUpObject
{
    [SerializeField] int amount;

    public void OnPickUp(Character character)
    {
        character.level.AddExperience(amount);
    }
}