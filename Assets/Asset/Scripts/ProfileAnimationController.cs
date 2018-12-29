using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class ProfileAnimationController : MonoBehaviour {
    #region Public Members

    public PostProcessingProfile profile;
    public Vector2 BloomIntensityRange = new Vector2(1.7f, 7.7f);
    public float BloomIntensityLoopDuration = 2f;

    #endregion

    #region Private Members

    BloomModel.Settings bloomSettings;

    Interpolation bloomInterpolation;
    #endregion

    // Use this for initialization
    void Start () {
        bloomSettings = profile.bloom.settings;
        bloomSettings.bloom.intensity = BloomIntensityRange.x;
        bloomInterpolation = new Interpolation(BloomIntensityLoopDuration);
        bloomInterpolation.AddKeyFrame(0, BloomIntensityRange.x);
        bloomInterpolation.AddKeyFrame(BloomIntensityLoopDuration/1.5f, BloomIntensityRange.y);
        bloomInterpolation.AddKeyFrame(BloomIntensityLoopDuration, BloomIntensityRange.x);
        bloomInterpolation.FrameCallback += (kf) =>
        {
            bloomSettings.bloom.intensity = kf.value;
            profile.bloom.settings = bloomSettings;
        };
    }
	
	// Update is called once per frame
	void Update () {
        if (bloomInterpolation.HasEnded) bloomInterpolation.Reset();
        bloomInterpolation.Next();
    }
}
