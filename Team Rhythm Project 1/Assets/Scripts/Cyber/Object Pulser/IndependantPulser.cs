using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndependantPulser : MonoBehaviour
{
    private Transform objectTransform;

    //[SerializeField]
    //AudioDataScript audioData; // used to keep reference to the Audio Data Manager in the scene

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    int band;

    public float[] samples = new float[512]; //samples temporary solution
    public float[] frequencyBand = new float[8];
    
   [SerializeField]
    float maxScale = 300;

    [SerializeField]
    float startScale, scaleMultiplier;

    private void Awake()
    {
        objectTransform = gameObject.transform; // stores reference to the object's transform.

        /* audioData = FindObjectOfType<audioData>(); 
        * gets and sets the reference of the audio data script 
        * (first instance that occurs in the scene, should only be one)
        */

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource(audioSource); // gets data on update
        MakeFrequencyBands();
        pulse();
    }

    void GetSpectrumAudioSource(AudioSource audioToUse)
    {
        audioToUse.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }

    void pulse()
    {
        objectTransform.localScale = new Vector3(transform.localScale.x,frequencyBand[band]+startScale,transform.localScale.z);
    }

    void MakeFrequencyBands()
    {
        int count = 0;

        for(int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i)*2; //gets how many samples are to go into the bands array
            if (i == 7)
            {
                sampleCount += 2;// creates the final band totalling 512 samples
            }
            for(int j = 0; j < sampleCount; j++)
            {
                average += samples[count] * (count + 1); // adds to next average calc
                count++;
            }
            average /= count; // creates the average to next be added to the frequency bands
            frequencyBand[i] = average * 10;
        }
    }
}
