using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Postprocess : MonoBehaviour
{
    public float bloom = 10f;
    public float saturation = 5f;

    Bloom bloomLayer = null;
    ColorGrading colorGradingLayer = null;
    PostProcessVolume volume;
    void Start()
    {
        volume = GetComponent<PostProcessVolume>();
    }

    void Update()
    {
        volume.profile.TryGetSettings(out bloomLayer);
        volume.profile.TryGetSettings(out colorGradingLayer);

        bloomLayer.enabled.value = true;
        colorGradingLayer.enabled.value = true;

        bloomLayer.intensity.value = bloom;
        colorGradingLayer.saturation.value = saturation;
    }
}
