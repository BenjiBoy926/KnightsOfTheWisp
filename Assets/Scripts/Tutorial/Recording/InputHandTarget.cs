using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandTarget : MonoBehaviour
{
    private readonly List<GameObject> _rings = new();

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            GameObject ring = child.gameObject;
            _rings.Add(ring);
            ring.SetActive(false);
        }
    }

    public void Press(float duration)
    {
        CoroutineRunner.StopAll(this);
        CoroutineRunner.Run(this, GetRingToggleSequence(duration, true));
    }
    
    public void Release(float duration)
    {
        CoroutineRunner.StopAll(this);
        CoroutineRunner.Run(this, GetRingToggleSequence(duration, false));
    }

    private IEnumerator GetRingToggleSequence(float duration, bool enable)
    {
        WaitForSeconds wait = new(duration / _rings.Count);
        foreach (var ring in _rings)
        {
            ring.SetActive(enable);
            yield return wait;
        }
    }
}