using System.Collections;
using System.Collections.Generic;
using UnityEngine.PostProcessing;
using UnityEngine;
using IndieGage;

public class ig_GameManager : MonoBehaviour {

    public static ig_GameManager instance;
    public PostProcessingProfile postProcessProfile;

    void OnEnable() {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = this;

        else Destroy(this);

        //ig_PostProcessTools.instance.standardColorModel = postProcessProfile.colorGrading.settings;
    }

    private static void UpdateRedChannel(Vector3 value) {
        ColorGradingModel.Settings cgms = instance.postProcessProfile.colorGrading.settings;

        cgms.channelMixer.red = value;

        instance.postProcessProfile.colorGrading.settings = cgms;

    }

    private static void UpdateBlueChannel(Vector3 value) {
        ColorGradingModel.Settings cgms = instance.postProcessProfile.colorGrading.settings;

        cgms.channelMixer.blue = value;

        instance.postProcessProfile.colorGrading.settings = cgms;
    }

    private static void UpdateGreenChannel(Vector3 value) {
        ColorGradingModel.Settings cgms = instance.postProcessProfile.colorGrading.settings;

        cgms.channelMixer.green = value;

        instance.postProcessProfile.colorGrading.settings = cgms;
    }
}
