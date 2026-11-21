using UnityEngine;

public class PlayerCursorFire : MonoBehaviour
{
    [SerializeField] private ParticleSystem _fire;
    [SerializeField] private ParticleSystem _burst;

    public void SetIsVisible(bool isVisible)
    {
        if (isVisible)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    public void Show()
    {
        Burst();
        _fire.Play();
    }

    public void Hide()
    {
        Burst();
        _fire.Stop();
    }

    public void Burst()
    {
        _burst.Play();
    }
}