using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

namespace IndieGage {
    static class ig_Utilities {

        public static ColorGradingModel.Settings standardColorModel;

        //public static Camera camera;

        //Time Control
        public static void SetTimeScale(float timeScale) {
            Time.timeScale = timeScale;
            
        }

        //Post Processing Control
        public static void FadeColorModel(GameObject target, ColorGradingModel.ChannelMixerSettings channel, float switchTime) {
            //red
            iTween.ValueTo(target, iTween.Hash("name", "redChannel",
                                                    "from", ig_GameManager.instance.postProcessProfile.colorGrading.settings.channelMixer.red,
                                                    "to", channel.red,
                                                    "time", switchTime,
                                                    "onupdate", "UpdateRedChannel",
                                                    "onupdatetarget", target,
                                                    "easetype", iTween.EaseType.easeInOutQuad,
                                                    "ignoretimescale", true
                                                    ));

            //green
            iTween.ValueTo(target, iTween.Hash("name", "greenChannel",
                                                    "from", ig_GameManager.instance.postProcessProfile.colorGrading.settings.channelMixer.green,
                                                    "to", channel.green,
                                                    "time", switchTime,
                                                    "onupdate", "UpdateGreenChannel",
                                                    "onupdatetarget", target,
                                                    "easetype", iTween.EaseType.easeInOutQuad,
                                                    "ignoretimescale", true
                                                    ));

            //blue
            iTween.ValueTo(target, iTween.Hash("name", "blueChannel",
                                                    "from", ig_GameManager.instance.postProcessProfile.colorGrading.settings.channelMixer.blue,
                                                    "to", channel.blue,
                                                    "time", switchTime,
                                                    "onupdate", "UpdateBlueChannel",
                                                    "onupdatetarget", target,
                                                    "easetype", iTween.EaseType.easeInOutQuad,
                                                    "ignoretimescale", true
                                                    ));
        }

        private static void UpdateRedChannel(Vector3 value) {
            ColorGradingModel.Settings cgms = ig_GameManager.instance.postProcessProfile.colorGrading.settings;
            

            cgms.channelMixer.red = value;

            ig_GameManager.instance.postProcessProfile.colorGrading.settings = cgms;

        }

        private static void UpdateBlueChannel(Vector3 value) {
            ColorGradingModel.Settings cgms = ig_GameManager.instance.postProcessProfile.colorGrading.settings;

            cgms.channelMixer.blue = value;

            ig_GameManager.instance.postProcessProfile.colorGrading.settings = cgms;
        }

        private static void UpdateGreenChannel(Vector3 value) {
            ColorGradingModel.Settings cgms = ig_GameManager.instance.postProcessProfile.colorGrading.settings;

            cgms.channelMixer.green = value;

            ig_GameManager.instance.postProcessProfile.colorGrading.settings = cgms;
        }

        public static void StopColorModelSwitch() {
            iTween.StopByName("redChannel");
            iTween.StopByName("blueChannel");
            iTween.StopByName("greenChannel");
        }


        //Camera Control
        public static void SetCameraOrthoSize(Camera camera, float cameraSize) {
            camera.orthographicSize = cameraSize;
        }


    }
}
