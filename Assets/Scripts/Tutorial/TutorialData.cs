using UnityEngine;

[CreateAssetMenu(menuName = nameof(TutorialData))]
public class TutorialData : ScriptableObject
{
    private const string ExpectedVideoFileExtension = ".mp4";

    public string Title => _title;
    public string RecorderPath => $"{Application.streamingAssetsPath}/Tutorial {_title}";
    public string VideoUrl => $"{RecorderPath}{ExpectedVideoFileExtension}";
    public string Description => _description;

    [SerializeField] private string _title;
    [TextArea, SerializeField] private string _description;
}