using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Recorder;
using UnityEditor.Recorder.Encoder;
using UnityEditor.Recorder.Input;
using UnityEditor.SceneManagement;
using UnityEngine;

public class TutorialRecorder : ITutorialRecorder
{
    private const string TutorialScenePath = "Assets/Scenes/TutorialScene.unity";

    private RecorderSettings _recorderSettings;
    private RecorderController _recorder;

    [MenuItem("Tools/Record All Tutorials")]
    private static void RecordAllTutorials()
    {
        EditorApplication.EnterPlaymode();
        EditorSceneManager.OpenScene(TutorialScenePath, OpenSceneMode.Single);
        Initialize();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Initialize()
    {
        TutorialStage stage = Object.FindAnyObjectByType<TutorialStage>();
        if (stage)
        {
            stage.Record(new TutorialRecorder());
        }
    }

    public TutorialRecorder()
    {
        _recorderSettings = GetRecorderSettings();
        RecorderControllerSettings controllerSettings = GetControllerSettings(_recorderSettings);
        _recorder = new(controllerSettings);
    }

    // NOTE: need to change file name here
    public void StartRecording(string tutorialName)
    {
        _recorderSettings.FileNameGenerator.FileName = $"Tutorial_{tutorialName}";
        _recorder.PrepareRecording();
        _recorder.StartRecording();
    }

    public void StopRecording()
    {
        _recorder.StopRecording();
    }

    private RecorderSettings GetRecorderSettings()
    {
        MovieRecorderSettings recorderSettings = ScriptableObject.CreateInstance<MovieRecorderSettings>();
        recorderSettings.Enabled = true;

        // Input
        ImageInputSettings inputSettings = new GameViewInputSettings();
        inputSettings.OutputWidth = 700;
        inputSettings.OutputHeight = 500;
        recorderSettings.ImageInputSettings = inputSettings;

        // Output encoder? Idk how to configure. Hopefully the default is good enough

        // Output File
        recorderSettings.FileNameGenerator.Root = OutputPath.Root.StreamingAssets;
        recorderSettings.FileNameGenerator.Leaf = "";

        return recorderSettings;
    }

    private RecorderControllerSettings GetControllerSettings(RecorderSettings recorderSettings)
    {
        RecorderControllerSettings controllerSettings = ScriptableObject.CreateInstance<RecorderControllerSettings>();
        controllerSettings.SetRecordModeToManual();
        controllerSettings.FrameRatePlayback = FrameRatePlayback.Constant;
        controllerSettings.FrameRate = 30;
        controllerSettings.CapFrameRate = true;
        controllerSettings.AddRecorderSettings(recorderSettings);
        return controllerSettings;
    }
}
