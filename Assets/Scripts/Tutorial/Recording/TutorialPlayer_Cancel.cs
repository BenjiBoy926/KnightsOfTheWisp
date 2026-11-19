using System.Collections;
using UnityEngine;
using static UniqueCharacters;

public class TutorialPlayer_Cancel : TutorialPlayer
{
    public override string Name => "Cancel";

    public override IEnumerator Play()
    {
        yield return Wait1;
        yield return MovePlayerTo(Hayden.Position);

        Player.Grab();
        yield return Wait1;

        yield return MovePlayerTo(Goblin.Position + (Vector2.left * .25f));
        yield return Wait1;

        AnimateInputTap();
        yield return Player.CancelMove();
        yield return Wait1;
    }
}