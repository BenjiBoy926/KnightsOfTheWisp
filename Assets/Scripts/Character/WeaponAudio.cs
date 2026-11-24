using Hellmade.Sound;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = nameof(WeaponAudio))]
public class WeaponAudio : ScriptableObject
{
    [SerializeField] private List<AudioClip> _windUp = new();
    [SerializeField] private List<AudioClip> _slash = new();
    [SerializeField] private List<AudioClip> _hit = new();

    public void PlayWindUp()
    {
        PlayRandom(_windUp);
    }

    public void PlaySlash()
    {
        PlayRandom(_slash);
    }

    public void PlayHit()
    {
        PlayRandom(_hit);
    }

    private void PlayRandom(List<AudioClip> clips)
    {
        EazySoundManager.PlaySound(Random(clips));
    }

    private AudioClip Random(List<AudioClip> clips)
    {
        if (IsNullOrEmpty(clips))
        {
            return null;
        }
        int index = UnityEngine.Random.Range(0, clips.Count);
        return clips[index];
    }

    private bool IsNullOrEmpty(List<AudioClip> clips)
    {
        return clips == null || clips.Count == 0;
    }
}
