using UnityEngine;

[CreateAssetMenu(menuName = nameof(TutorialData))]
public class TutorialData : ScriptableObject
{
    private const string ExpectedVideoFileExtension = ".mp4";

    public string Title => _title;
    public string VideoUrl => $"{Application.streamingAssetsPath}/{_videoFileName}{ExpectedVideoFileExtension}";
    public string Description => _description;

    [SerializeField] private string _title;
    [SerializeField] private string _videoFileName;
    [TextArea, SerializeField] private string _description;
}