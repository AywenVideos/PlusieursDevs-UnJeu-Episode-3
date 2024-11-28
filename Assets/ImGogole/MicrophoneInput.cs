using UnityEngine;
using UnityEngine.UI;

public class MicrophoneInput : MonoBehaviour
{
    [SerializeField] private Slider microphoneValueSlider;
    
    public static MicrophoneInput Instance { get; private set; }

    private AudioClip microphoneClip;
    private string microphoneName;
    private const int sampleWindow = 128;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (Microphone.devices.Length > 0)
        {
            microphoneName = Microphone.devices[0];
            microphoneClip = Microphone.Start(microphoneName, true, 1, 44100);
        }
        else
        {
            Debug.Log("Aucun microphone n'a �t� trouv�.");
        }
    }

    void Update()
    {
        float volume = GetMicrophoneVolume();
        microphoneValueSlider.value = volume;
    }


    /// <summary>
    /// R�cup�re le niveau du microphone. Cette valeur est instable et doit �tre am�liorer.
    /// Utilisez MicrophoneInput.Instance.GetMicrophoneVolume() pour r�cuperer la valeur o� vous voulez.
    /// </summary>
    /// <returns></returns>
    public float GetMicrophoneVolume()
    {
        float[] data = new float[sampleWindow];
        int microphonePosition = Microphone.GetPosition(microphoneName) - sampleWindow + 1;
        if (microphonePosition < 0)
            return 0;

        microphoneClip.GetData(data, microphonePosition);

        float sum = 0;
        for (int i = 0; i < sampleWindow; i++)
        {
            sum += data[i] * data[i];
        }

        return Mathf.Sqrt(sum / sampleWindow);
    }

    void OnDisable()
    {
        Microphone.End(microphoneName);
    }
}

