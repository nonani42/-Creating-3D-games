using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class PostProcess_HotCold : MonoBehaviour
{
    public float temperature = 0f;

    ColorGrading colorGradingLayer = null;

    PostProcessVolume volume;

    void Start()
    {
        volume = GetComponent<PostProcessVolume>();

        volume.profile.TryGetSettings(out colorGradingLayer);
        colorGradingLayer.enabled.value = true;
    }

    void Update()
    {

        colorGradingLayer.temperature.value = temperature;
    }

    private void OnTriggerEnter(Collider other)
    {
        temperature = 100f;
    }

    private void OnTriggerExit(Collider other)
    {
        temperature = -100f;
    }
}
