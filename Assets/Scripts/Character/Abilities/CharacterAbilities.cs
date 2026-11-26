using System.Collections;
using UnityEngine;

public class CharacterAbilities : MonoBehaviour
{
    private Character _character;

    private void Awake()
    {
        _character = GetComponentInParent<Character>();
    }

    public IEnumerator GetActivationSequence(Character other)
    {
        Debug.Log($"Abilities activated on: {other}", _character);
        yield break;
    }
}
