using System.Collections;
using System.Collections.Generic;
using UnityEditor.Media;
using UnityEditor.Recorder;
using UnityEditor.Recorder.Encoder;
using UnityEditor.Recorder.Input;
using UnityEngine;

public class TutorialRecorder : ITutorialRecorder
{
    private RecorderController _recorder;

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
        RecorderSettings recorderSettings = GetRecorderSettings();
        RecorderControllerSettings controllerSettings = GetControllerSettings(recorderSettings);
        _recorder = new(controllerSettings);
    }

    // NOTE: need to change file name here
    public void StartRecording()
    {
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
        recorderSettings.FileNameGenerator.FileName = "Tutorial";

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
