using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Battle))]
public class TutorialStage : MonoBehaviour
{
    private Battle _battle;
    private PlayerInput _input;
    private BattleTurnChangeAnimation _turnChangeAnimation;
    private CharacterDetailPanel[] _detailPanels;
    private TutorialPlayer[] _players;

    private void Awake()
    {
        _battle = GetComponent<Battle>();
        _input = GetComponentInChildren<PlayerInput>(true);
        _turnChangeAnimation = GetComponentInChildren<BattleTurnChangeAnimation>(true);
        _detailPanels = GetComponentsInChildren<CharacterDetailPanel>(true);
        _players = GetComponentsInChildren<TutorialPlayer>(true);
    }

    private IEnumerator Start()
    {
        _input.enabled = false;
        _turnChangeAnimation.Skip = true;
        foreach (var panel in _detailPanels)
        {
            panel.gameObject.SetActive(false);
        }

        foreach (var player in _players)
        {
            yield return player.Setup();
            // Start recording!
            yield return player.Play();
            // Stop recording!
            yield return player.Teardown();
        }
    }
}