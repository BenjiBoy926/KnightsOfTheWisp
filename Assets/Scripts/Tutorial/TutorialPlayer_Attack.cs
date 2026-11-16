using UnityEngine;
using System.Collections;
using static UniqueCharacters;

public class TutorialPlayer_Attack : TutorialPlayer
{
    public override string Name => "Attack";

    public override IEnumerator Play()
    {
        yield return Wait1;
        yield return MovePlayerTo(Hayden.Position);
        Player.Grab();
        yield return Wait1;
        yield return MovePlayerTo(Goblin.Position + (Vector2.left * .25f));
        yield return Wait1;
        yield return Player.Release();
        yield return Wait1;
    }
}