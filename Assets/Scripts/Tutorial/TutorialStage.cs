using UnityEngine;

[RequireComponent(typeof(Battle))]
public class TutorialStage : MonoBehaviour
{
    private Battle _battle;
    private CharacterDetailPanel[] _detailPanels;

    private void Awake()
    {
        _battle = GetComponent<Battle>();
        _detailPanels = GetComponentsInChildren<CharacterDetailPanel>(true);
    }

    private void Start()
    {
        foreach (var panel in _detailPanels)
        {
            panel.gameObject.SetActive(false);
        }
        _battle.Begin();
    }
}