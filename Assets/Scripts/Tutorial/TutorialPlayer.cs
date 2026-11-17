using DG.Tweening;
using System.Collections;
using UnityEngine;

public abstract class TutorialPlayer : MonoBehaviour
{
    protected readonly WaitForSeconds Wait1 = new(1);
    protected readonly WaitForSeconds Wait2 = new(2);

    public abstract string Name { get; }
    protected Battle Battle => _battle;
    protected Player Player => _player;
    protected Character Goblin => _goblin;

    [SerializeField] private float _playerMoveSpeed = 2f;
    private Battle _battle;
    private Player _player;
    private Character _goblin;
    private ButtonAnimation[] _animations;

    private void Awake()
    {
        _battle = GetComponentInParent<Battle>();
        _player = _battle.GetComponentInChildren<Player>(true);
        _animations = GetComponentsInChildren<ButtonAnimation>(true);
        gameObject.SetActive(false);
    }

    public virtual IEnumerator Setup()
    {
        gameObject.SetActive(true);
        _battle.Prepare();
        _battle.Begin();
        _player.SetPosition(Vector2.right);
        _goblin = FindGoblin();
        yield return null;
    }

    public abstract IEnumerator Play();

    public virtual IEnumerator Teardown()
    {
        gameObject.SetActive(false);
        yield return null;
    }

    protected YieldInstruction MovePlayerTo(Vector2 position)
    {
        return DOTween.To(GetPlayerPosition, SetPlayerPosition, position, _playerMoveSpeed)
            .SetSpeedBased()
            .WaitForCompletion();
    }

    private Vector2 GetPlayerPosition()
    {
        return _player.CursorPosition;
    }

    private void SetPlayerPosition(Vector2 position)
    {
        _player.SetPosition(position);
    }

    private Character FindGoblin()
    {
        foreach (var character in _battle.AllCharacters)
        {
            if (character.Faction != _player.Faction)
            {
                return character;
            }
        }
        return null;
    }

    protected void AnimateButtonPress()
    {
        foreach (var animation in _animations)
        {
            animation.Press();
        }
    }

    protected void AnimateButtonRelease()
    {
        foreach (var animation in _animations)
        {
            animation.Release();
        }
    }
}