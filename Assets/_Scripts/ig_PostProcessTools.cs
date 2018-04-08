using System.Collections;
using System.Collections.Generic;
using UnityEngine.PostProcessing;
using UnityEngine;

namespace IndieGage {
    public class ig_PostProcessTools : MonoBehaviour {

        public static ig_PostProcessTools instance;
        //public PostProcessingProfile postProcessProfile;
        public ColorGradingModel.Settings standardColorModel;

        // Use this for initialization
        void OnEnable() {
            DontDestroyOnLoad(gameObject);
            if (instance == null) {
                instance = this;
            } else Destroy(gameObject);

            standardColorModel = ig_GameManager.instance.postProcessProfile.colorGrading.settings;

        }

        public void SwitchColorModel(ColorGradingModel.ChannelMixerSettings channel, float switchTime) {
            

            //red
            iTween.ValueTo(gameObject, iTween.Hash("name", "redChannel",
                                                    "from", ig_GameManager.instance.postProcessProfile.colorGrading.settings.channelMixer.red,
                                                    "to", channel.red,
                                                    "time", switchTime,
                                                    "onupdate", "UpdateRedChannel",
                                                    "easetype", iTween.EaseType.easeInOutQuad,
                                                    "ignoretimescale", true
                                                    ));

            //green
            iTween.ValueTo(gameObject, iTween.Hash("name", "greenChannel",
                                                    "from", ig_GameManager.instance.postProcessProfile.colorGrading.settings.channelMixer.green,
                                                    "to", channel.green,
                                                    "time", switchTime,
                                                    "onupdate", "UpdateGreenChannel",
                                                    "easetype", iTween.EaseType.easeInOutQuad,
                                                    "ignoretimescale", true
                                                    ));

            //blue
            iTween.ValueTo(gameObject, iTween.Hash("name", "blueChannel",
                                                    "from", ig_GameManager.instance.postProcessProfile.colorGrading.settings.channelMixer.blue,
                                                    "to", channel.blue,
                                                    "time", switchTime,
                                                    "onupdate", "UpdateBlueChannel",
                                                    "easetype", iTween.EaseType.easeInOutQuad,
                                                    "ignoretimescale", true
                                                    ));

        }

        private void UpdateRedChannel(Vector3 value) {
            ColorGradingModel.Settings cgms = standardColorModel;

            cgms.channelMixer.red = value;

            ig_GameManager.instance.postProcessProfile.colorGrading.settings = cgms;

        }

        private void UpdateBlueChannel(Vector3 value) {
            ColorGradingModel.Settings cgms = standardColorModel;

            cgms.channelMixer.blue = value;

            ig_GameManager.instance.postProcessProfile.colorGrading.settings = cgms;
        }

        private void UpdateGreenChannel(Vector3 value) {
            ColorGradingModel.Settings cgms = standardColorModel;

            cgms.channelMixer.green = value;

            ig_GameManager.instance.postProcessProfile.colorGrading.settings = cgms;
        }

        public void StopColorModelSwitch() {
            iTween.StopByName("redChannel");
            iTween.StopByName("blueChannel");
            iTween.StopByName("greenChannel");
        }

    }
}