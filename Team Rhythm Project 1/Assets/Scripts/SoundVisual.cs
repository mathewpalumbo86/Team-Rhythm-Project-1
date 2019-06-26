using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundVisual : MonoBehaviour
{
    // sample size of 1024 is constant (in this case)
    public const int SAMPLE_SIZE = 1024;

    // these are checked every frame
    // average power output of the sound
    public float rmsValue;
    // volume per frame
    public float dbValue;
    // pitch per frame
    public float pitchValue;

    // multiplies how big the cubes are scaled with the music
    public float visualModifier = 50.0f;
    // how quickly the cubes return their scale to the original size after a pulse
    public float smoothSpeed = 10.0f;

    //====================================================
    // audio being played
    private AudioSource source;

    // samples and spectrum arrays and the sampleRate from file
    public float[] samples;
    public float[] spectrum;
    public float sampleRate;


    // Visual stuff =====================================
    // list of objects spawned
    public Transform[] visualList;

    
    // the height scale of each cube based on the audio
    public float[] visualScale;
    // number of cubes
    public int amountOfVisuals;


    // Start is called before the first frame update
    void Start()
    {
        // get audio attached to this object
        source = GetComponent<AudioSource>();

        // sample size
        samples = new float[SAMPLE_SIZE];
        // spectrum size
        spectrum = new float[SAMPLE_SIZE];
        // get the sample rate from the audio file
        sampleRate = AudioSettings.outputSampleRate;

        // Visualise
        // SpawnLine();
    }

    //// spawn objects THIS NEEDS TO BE MOVED ELSEWHERE
    //private void SpawnLine()
    //{
    //    // These are the scale values which are updated based on the audio spectrum data
    //    visualScale = new float[amountOfVisuals];

    //    visualList = new Transform[amountOfVisuals];
        

    //    for (int i = 0; i < amountOfVisuals; i++)
    //    {
    //        // spawns primitives, in this case cubes 
    //        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube) as GameObject;
            
    //        // adds it to the list
    //        visualList[i] = go.transform;
    //        // gives it a position
    //        visualList[i].position = Vector3.back * i;           
    //    }
        
    //}


    // Update is called once per frame
    void Update()
    {
        // analyse sound every frame
        AnalyzeSound();
        // update visuals every frame
        // UpdateVisual();
    }

    
    //private void UpdateVisual()
    //{

    //    int visualIndex = 0;
    //    // which frequency
    //    int spectrumIndex = 0;
    //    // splits all the samples up into bins based on how many visuals there are
    //    int averageSize = SAMPLE_SIZE / amountOfVisuals;

    //    while (visualIndex < amountOfVisuals)
    //    {
    //        int j = 0;
    //        float sum = 0;
    //        while (j < averageSize)
    //        {
    //            sum += spectrum[spectrumIndex];
    //            spectrumIndex++;
    //            j++;
    //        }

    //        // 
    //        float scaleY = sum / averageSize * visualModifier;

    //        visualScale[visualIndex] -= Time.deltaTime * smoothSpeed;

    //        if (visualScale[visualIndex] < scaleY)
    //            visualScale[visualIndex] = scaleY;

    //        // modifies the scale of each object
    //        visualList[visualIndex].localScale = Vector3.one + Vector3.up * visualScale[visualIndex];
    //        visualIndex++;



    //    }
    //}


    // 
    private void AnalyzeSound()
    {
        // 
        source.GetOutputData(samples, 0);

        // Get the RMS value
        int i = 0;
        float sum = 0;
        for (; i < SAMPLE_SIZE; i++)
        {
            sum = samples[i] * samples[i];
        }

        // both of these are from a source on stack exchange, need to research further into it
        // get the rms value
        rmsValue = Mathf.Sqrt(sum / SAMPLE_SIZE);
        // get the db value
        dbValue = 20 * Mathf.Log10(rmsValue / 0.1f);

        // 
        // Get the spectrum data (requires the sample array, the channel the FFTWindow type being used to split up the frequencies up)
        source.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

        /* This isn't necessary in this script but gives an example of how to read the pitch. 
           Need to go through and comment it properly. */
        // Get the pitch
        float maxV = 0;
        var maxN = 0;
        for (i = 0; i < SAMPLE_SIZE; i++)
        {
            if (!(spectrum[i] > maxV) || !(spectrum[i] > 0.0f))
                continue;

            maxV = spectrum[i];
            maxN = i;
        }

        float freqN = maxN;
        if(maxN > 0 && maxN < SAMPLE_SIZE - 1)
        {
            // Left and right volumes? Not sure need to check
            var dL = spectrum[maxN - 1] / spectrum[maxN];
            var dR = spectrum[maxN + 1] / spectrum[maxN];

            freqN += 0.5f * (dR * dR - dL * dL);
        }

        pitchValue = freqN * (sampleRate / 2) / SAMPLE_SIZE;

    }



}
