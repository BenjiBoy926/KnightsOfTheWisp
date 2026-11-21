using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    public enum Mode
    {
        Torch, Pointer, Aura, None
    }

    public Vector2 Position
    {
        get => transform.position;
        set => transform.position = value;
    }

    [SerializeField] private ParticleSystem _pointerParticles;
    [SerializeField] private float _selectionMargin = .25f;
    private Player _player;
    private PlayerCursorFire _fire;
    private PlayerSelectionAura _aura;
    private Mode _currentMode;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
        _fire = GetComponentInChildren<PlayerCursorFire>(true);
        _aura = GetComponentInChildren<PlayerSelectionAura>(true);
    }

    private void Start()
    {
        Cursor.visible = false;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            Cursor.visible = false;
        }
    }

    private void Update()
    {
        SetCurrentMode(CalculateCurrentMode());
        if (_currentMode == Mode.Pointer)
        {
            Vector2 direction = _player.CharacterAtCurrentCell.CurrentCellCenter - _player.ActiveCharacter.CurrentCellCenter;
            transform.up = direction;
        }
    }

    public void Show()
    {
        SetCurrentMode(Mode.Torch);
    }

    public void Burst()
    {
        _fire.Burst();
    }

    private Mode CalculateCurrentMode()
    {
        if (!_player.IsInputAllowed)
        {
            return Mode.None;
        }
        else if (!_player.ActiveCharacter)
        {
            return Mode.Torch;
        }

        Vector2 targetPosition = _player.ActiveCharacter.Position;
        float distance = Vector2.Distance(Position, targetPosition);
        bool isNear = distance <= _selectionMargin;
        if (isNear)
        {
            return Mode.Aura;
        }
        else if (_player.IsTargettingOtherCharacter())
        {
            return Mode.Pointer;
        }
        else
        {
            return Mode.Torch;
        }
    }

    private void SetCurrentMode(Mode currentMode)
    {
        if (currentMode == _currentMode)
            return;

        _currentMode = currentMode;
        if (_currentMode != Mode.Pointer)
        {
            transform.up = Vector3.up;
        }

        _fire.Burst();
        _fire.SetIsVisible(_currentMode == Mode.Torch);
        _aura.SetIsVisible(_currentMode == Mode.Aura);
        if (_currentMode == Mode.Pointer)
        {
            _pointerParticles.Play();
        }
        else
        {
            _pointerParticles.Stop();
        }
    }
}