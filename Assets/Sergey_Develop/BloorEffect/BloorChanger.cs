using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;


[RequireComponent (typeof(Slider))]
public class BloorChanger : MonoBehaviour
{
    [SerializeField] float minDioptr;
    [SerializeField] float maxDioptr;

    private PostProcessVolume _volume;
    private DepthOfField _depthOfField;
    private Slider _dioptrSlider;

    private void Awake()
    {
        _dioptrSlider = GetComponent<Slider>();

        _volume = FindObjectOfType<PostProcessVolume>();
        _volume.profile.TryGetSettings(out _depthOfField);
    }

    public void SetDioptr (float sliderVolume)
    {
        float newDepthOfField = minDioptr + maxDioptr * sliderVolume;
        _depthOfField.focusDistance.value = newDepthOfField;
    }
}
