using System.Collections;
using UnityEngine;
using static UniqueCharacters;

public class TutorialPlayer_AttackDirection : TutorialPlayer
{
    public override IEnumerator Setup()
    {
        yield return base.Setup();
        Player.SetPosition(Hayden.Position);
        Player.Grab();
        Player.SetPosition(Goblin.Position + Vector2.left * .25f);
        yield return Wait1; // Don't want to capture the select audio in the video
    }

    public override IEnumerator Play()
    {
        yield return WaitPoint5;
        yield return MovePlayerTo(Goblin.Position + Vector2.down * .25f);

        yield return WaitPoint5;
        yield return MovePlayerTo(Goblin.Position + Vector2.right * .25f);

        yield return WaitPoint5;
        yield return MovePlayerTo(Goblin.Position + Vector2.up * .25f);

        yield return WaitPoint5;
        yield return MovePlayerTo(Goblin.Position + Vector2.left * .25f);
    }

    public override IEnumerator Teardown()
    {
        yield return base.Teardown();
        // Deselect the goblin in the center
        Player.SetPosition(Vector2.right * 5);
    }
}