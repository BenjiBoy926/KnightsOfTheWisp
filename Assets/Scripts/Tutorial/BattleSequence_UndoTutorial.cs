using System.Collections;

[UnityEngine.RequireComponent(typeof(TutorialWindow))]
public class BattleSequence_UndoTutorial : BattleSequence
{
    private TutorialWindow _window;

    protected override void Awake()
    {
        base.Awake();
        _window = GetComponent<TutorialWindow>();
    }

    public override bool ShouldPlay(BattleEvent e)
    {
        throw new System.NotImplementedException();
    }

    protected override IEnumerator GetSequence(BattleEvent e)
    {
        yield return _window.Open();
    }
}