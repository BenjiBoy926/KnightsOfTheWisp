using System.Collections;
using UnityEngine;

public abstract class TutorialPlayer : MonoBehaviour
{
    protected Battle Battle => _battle;
    protected Player Player => _player;

    private Battle _battle;
    private Player _player;

    private void Awake()
    {
        _battle = GetComponentInParent<Battle>();
        _player = _battle.GetComponentInChildren<Player>(true);
    }

    public virtual void Setup()
    {
        gameObject.SetActive(true);
        _battle.Prepare();
        _battle.Begin();
        _player.SetPosition(Vector2.zero);
    }

    public abstract IEnumerator Play();

    public virtual void Teardown()
    {
        gameObject.SetActive(false);
    }
}