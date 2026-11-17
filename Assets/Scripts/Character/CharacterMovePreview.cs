using Hellmade.Sound;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class CharacterMovePreview : MonoBehaviour
{
    [SerializeField] private AudioClip _previewSound;
    private Character _character;
    private readonly List<Character> _activePreviews = new();
    private Vector2Int _characterCell;

    private void Awake()
    {
        _character = GetComponent<Character>();
        RefreshIsEnabled();
    }

    private void Update()
    {
        Vector2Int currentCell = _character.CurrentCell;
        if (currentCell != _characterCell)
        {
            EazySoundManager.PlaySound(_previewSound);
            _characterCell = currentCell;
        }
    }

    public void PreviewMove(Character other)
    {
        Clear();

        InteractionResult result = InteractionResult.Calculate(_character, other);
        _character.Preview(result.Interactor);
        other.Preview(result.Target);

        _activePreviews.Add(_character);
        _activePreviews.Add(other);

        EazySoundManager.PlaySound(_previewSound);

        _characterCell = _character.CurrentCell;
        RefreshIsEnabled();
    }

    public void Clear()
    {
        for (int i = 0; i < _activePreviews.Count; i++)
        {
            Character character = _activePreviews[i];
            character.ClearPreview();
        }
        _activePreviews.Clear();
        RefreshIsEnabled();
    }

    private void RefreshIsEnabled()
    {
        enabled = ShouldBeEnabled();
    }

    private bool ShouldBeEnabled()
    {
        return _activePreviews.Count > 0;
    }
}