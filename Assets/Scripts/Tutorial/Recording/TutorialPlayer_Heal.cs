using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UniqueCharacters;

public class TutorialPlayer_Heal : TutorialPlayer
{
    public override IEnumerator Setup()
    {
        yield return base.Setup();
        Robin.SetDirection(Vector2.left);
        Robin.SetHealth(1);
    }

    public override IEnumerator Play()
    {
        yield return Wait1;
        yield return MovePlayerTo(Hayden.Position);

        AnimateInputPress();
        Player.Grab();
        yield return Wait1;

        yield return MovePlayerTo(Robin.Position + (Vector2.left * .25f));
        yield return Wait1;

        AnimateInputRelease();
        yield return Player.Release();
        yield return Wait1;
    }
}
