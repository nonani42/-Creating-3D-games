using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class PostProcess_Blind : MonoBehaviour
{
    public float exposure = 0f;

    AutoExposure autoExposureLayer = null;

    PostProcessVolume volume;

    void Start()
    {
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out autoExposureLayer);

        autoExposureLayer.enabled.value = true;

    }

    void Update()
    {
        ChangeExposure();
        if (Input.GetKeyDown(KeyCode.B))
        {
            Blind();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Normal();
        }

    }

    private void Blind()
    {
        exposure = -9f;
        ChangeExposure();
        exposure = 9f;
        ChangeExposure();
    }

    private void Normal()
    {
        exposure = 0f;
        ChangeExposure();
    }
    private void ChangeExposure()
    {
        autoExposureLayer.minLuminance.value = exposure;
        autoExposureLayer.maxLuminance.value = exposure;
    }

    private void OnTriggerEnter(Collider other)
    {
        exposure = -9f;
    }

    private void OnTriggerExit(Collider other)
    {
        exposure = 9f;
    }

}
