using System.Collections;
using System.Collections.Generic;
using Saidus2;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{

    //By Xernas78
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider ambiantSlider;

    void Start()
    {
        masterSlider.onValueChanged.AddListener((v =>
        {
            VolumeManager.MasterVolume = v;
        }));
        masterSlider.onValueChanged.AddListener((v =>
        {
            VolumeManager.AmbientVolume = v;
        }));
    }
}
