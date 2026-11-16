using UnityEngine;
using System.Collections;

public class TutorialPlayer_Attack : TutorialPlayer
{
    public override IEnumerator Play()
    {
        Debug.Log("Perform the attack tutorial!");
        yield break;
    }
}