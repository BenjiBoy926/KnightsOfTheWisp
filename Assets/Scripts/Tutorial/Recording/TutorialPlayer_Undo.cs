using System.Collections;
using UnityEngine;
using static UniqueCharacters;

public class TutorialPlayer_Undo : TutorialPlayer
{
    public override IEnumerator Play()
    {
        yield return Wait1;
        yield return MovePlayerTo(Hayden.Position);

        Player.Grab();
        yield return Wait1;

        yield return MovePlayerTo(Vector2.right * 2);
        yield return Wait1;

        yield return Player.Release();
        yield return Wait1;

        AnimateInputPress();
        Player.BeginUndo();
        yield return new WaitUntil(() => BattleHistory.IsAnySequencePlaying);
        AnimateInputRelease();
        yield return new WaitUntil(() => !BattleHistory.IsAnySequencePlaying);
        yield return Wait2;
    }
}