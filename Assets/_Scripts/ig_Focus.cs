using System.Collections;
using System.Collections.Generic;
using UnityEngine.PostProcessing;
using UnityEngine;

namespace IndieGage {
    public class ig_Focus : ig_Ability {

        public delegate void OnStartAbility();
        public delegate void OnStopAbility();
        public OnStopAbility stopFocus;
        public OnStartAbility startFocus;

        private GameObject parent;

        public float timeToFocus = 0.1f;
        public float timeToAdjust = 0.5f;
        public float focusCameraSize = 9f;
        private bool canFocus = true;

        public ColorGradingModel.ChannelMixerSettings focusColorChannel;
        public bool focusing = false;
        private float defaultCameraSize;
        public float focusTimeScale;
        public Camera _camera;

        public void FocusInit(GameObject parent) {
            this.parent = parent;

            focusing = false;
            canFocus = true;
            defaultCameraSize = _camera.orthographicSize;
        }

        public override void StartAbility() {
            focusing = true;
            iTween.StopByName("FocusTween");
            ig_PostProcessTools.instance.StopColorModelSwitch();
            //ig_Utilities.StopColorModelSwitch();
            ig_PostProcessTools.instance.SwitchColorModel(focusColorChannel, timeToAdjust);
            //ig_Utilities.FadeColorModel(ig_GameManager.instance.gameObject, focusColorChannel, timeToAdjust);
            TweenFocus(focusTimeScale, focusCameraSize);
        }

        public override void UpdateAbility() {
            base.UpdateAbility();
        }

        public override void StopAbility() {
            focusing = false;
            //ig_Utilities.StopColorModelSwitch();
            ig_PostProcessTools.instance.StopColorModelSwitch();
            //ig_Utilities.FadeColorModel(ig_GameManager.instance.gameObject, ig_Utilities.standardColorModel.channelMixer, timeToAdjust);
            ig_PostProcessTools.instance.SwitchColorModel(ig_PostProcessTools.instance.standardColorModel.channelMixer, timeToAdjust);
            iTween.StopByName("FocusTween");
            TweenFocus(1f, defaultCameraSize);
            stopFocus();
        }

        private void TweenFocus(float timeScale, float cameraSize) {

            //tween time scale
            iTween.ValueTo(gameObject, iTween.Hash("name", "FocusTween",
                                                    "from", Time.timeScale,
                                                    "to", timeScale,
                                                    "easetype", iTween.EaseType.easeInQuad,
                                                    "time", timeToFocus,
                                                    "onupdate", "SetTimeScale",
                                                    "onupdatetarget", gameObject,
                                                    "ignoretimescale", true));

            //tween camera size
            iTween.ValueTo(gameObject, iTween.Hash("name", "FocusTween",
                                                    "from", _camera.orthographicSize,
                                                    "to", cameraSize,
                                                    "easetype", iTween.EaseType.easeOutQuad,
                                                    "time", timeToAdjust,
                                                    "onupdate", "SetCameraSize",
                                                    "ignoretimescale", true));

        }

        private void SetCameraSize(float value) {
            ig_Utilities.SetCameraOrthoSize(_camera, value);
        }

        private void SetTimeScale(float value) {
            ig_Utilities.SetTimeScale(value);
        }

    }
}
