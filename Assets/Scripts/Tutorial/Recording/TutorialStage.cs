using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Battle))]
public class TutorialStage : MonoBehaviour
{
    private BattleCondition _battleCondition;
    private PlayerInput _input;
    private BattleTurnChangeAnimation _turnChangeAnimation;
    private CharacterDetailPanel[] _detailPanels;
    private TutorialPlayer[] _players;

    private void Awake()
    {
        _battleCondition = GetComponentInChildren<BattleCondition>(true);
        _input = GetComponentInChildren<PlayerInput>(true);
        _turnChangeAnimation = GetComponentInChildren<BattleTurnChangeAnimation>(true);
        _detailPanels = GetComponentsInChildren<CharacterDetailPanel>(true);
        _players = GetComponentsInChildren<TutorialPlayer>(true);
    }

    public IEnumerator GetRecordSequence(ITutorialRecorder recorder)
    {
        _battleCondition.enabled = false;
        _input.enabled = false;
        _turnChangeAnimation.enabled = false;
        foreach (var panel in _detailPanels)
        {
            panel.gameObject.SetActive(false);
        }
        foreach (var player in _players)
        {
            player.gameObject.SetActive(false);
        }

        foreach (var player in _players)
        {
            yield return player.Setup();
            recorder.StartRecording(player.Data);
            yield return player.Play();
            recorder.StopRecording();
            yield return player.Teardown();
        }
    }
}