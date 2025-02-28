using UnityEngine;
using UnityEngine.UI; // or using TMPro; if using TextMeshPro
using Photon.Voice.Unity;
using Photon.Voice;
using System.Collections.Generic;
using TMPro;

public class MicDeviceSelector : MonoBehaviour
{
    public Recorder recorder;
    public TMP_Dropdown micDropdown; // If using TextMeshPro dropdown, then use TMP_Dropdown instead

    // a parallel list that holds device info objects
    private List<DeviceInfo> deviceInfos = new List<DeviceInfo>();

    void Start()
    {
        // Prepare the Recorder for microphone usage
        recorder.SourceType = Recorder.InputSourceType.Microphone;
        recorder.MicrophoneType = Recorder.MicType.Unity;

        // Populate the dropdown with available devices
        PopulateDropdown();

        // (Optional) pick the first mic automatically
        if (deviceInfos.Count > 0)
        {
            recorder.MicrophoneDevice = deviceInfos[0];
            recorder.RestartRecording();
        }
    }

    private void PopulateDropdown()
    {
        micDropdown.ClearOptions();
        deviceInfos.Clear();

        string[] unityMics = Microphone.devices;

        if (unityMics.Length == 0)
        {
            micDropdown.AddOptions(new List<string> { "No Microphones Detected" });
            return;
        }

        // Build a list of device names and store each as a DeviceInfo
        List<string> options = new List<string>();
        for (int i = 0; i < unityMics.Length; i++)
        {
            var micName = unityMics[i];
            options.Add(micName);
            deviceInfos.Add(new DeviceInfo(micName));
        }

        micDropdown.AddOptions(options);
        micDropdown.value = 0; // default to first device
    }

    public void OnMicDropdownChanged(int index)
    {
        if (index >= 0 && index < deviceInfos.Count)
        {
            var deviceInfo = deviceInfos[index];
            recorder.MicrophoneDevice = deviceInfo;
            recorder.RestartRecording();

            Debug.Log("Microphone changed to: " + deviceInfo.IDString);
        }
    }
}
