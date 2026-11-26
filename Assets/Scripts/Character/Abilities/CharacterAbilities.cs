using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbilities : MonoBehaviour
{
    private Character _character;

    private void Awake()
    {
        _character = GetComponentInParent<Character>();
    }

    public void Activate()
    {
        Debug.Log("Abilities activated!");
    }
}
