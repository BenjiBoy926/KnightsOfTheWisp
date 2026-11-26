using System.Collections;
using UnityEngine;
using TMPro;

public class BattleTurnChangeAnimation : MonoBehaviour
{
    public bool IsPlaying => _isPlaying;

    private Battle _battle;
    private Overlay _overlay;
    private BattleTurnChangeBanner _banner;
    private BattleTurnChangeLabel _label;
    private bool _isPlaying = false;

    private void Awake()
    {
        _battle = GetComponentInParent<Battle>();
        _overlay = GetComponentInChildren<Overlay>(true);
        _banner = GetComponentInChildren<BattleTurnChangeBanner>(true);
        _label = GetComponentInChildren<BattleTurnChangeLabel>(true);
    }

    public void Play(Faction faction)
    {
        gameObject.SetActive(true);
        CoroutineRunner.StopAll(this);
        CoroutineRunner.Run(this, GetPlaySequence(faction));
    }

    private IEnumerator GetPlaySequence(Faction faction)
    {
        if (!enabled)
            yield break;

        _isPlaying = true;
        yield return _overlay.FadeIn();
        yield return _banner.AnimateIn(faction);
        yield return _label.Animate(faction);
        yield return _banner.AnimateOut();
        yield return _overlay.FadeOut();
        _isPlaying = false;

        yield return _battle.GetSequence(BattleEvent.TurnStart());
    }
}
