using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundVisual : MonoBehaviour
{
    // sample size of 1024 is constant (in this case)
    private const int SAMPLE_SIZE = 1024;

    // these are checked every frame
    // average power output of the sound
    public float rmsValue;
    // volume per frame
    public float dbValue;
    // pitch per frame
    public float pitchValue;

    //
    public float visualModifier = 50.0f;
    // 
    public float smoothSpeed = 10.0f;

    //====================================================
    // audio being played
    private AudioSource source;

    // samples and spectrum arrays and sampleRate from file
    private float[] samples;
    private float[] spectrum;
    private float sampleRate;


    // Visual stuff =====================================
    // array of objects spawned
    private Transform[] visualList;
    // how far apart they will be (based on scale)
    private float[] visualScale;
    // number of cubes
    public int amnVisual;


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
        SpawnLine();
    }

    // spawn objects
    private void SpawnLine()
    {
        // These are the scale values which are updated based on the audio spectrum data
        visualScale = new float[amnVisual];
        visualList = new Transform[amnVisual];

        for (int i = 0; i < amnVisual; i++)
        {
            // spawns primitives, in this case cubes 
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube) as GameObject;

            visualList[i] = go.transform;
            visualList[i].position = Vector3.back * i;
        }
          
    }


    // Update is called once per frame
    void Update()
    {
        // analyse sound every frame
        AnalyzeSound();
        // update visuals every frame
        UpdateVisual();
    }

    // This applys the scale values to the list of objects, updating the visuals
    private void UpdateVisual()
    {
        int visualIndex = 0;
        int spectrumIndex = 0;
        int averageSize = SAMPLE_SIZE / amnVisual;

        while (visualIndex < amnVisual)
        {
            int j = 0;
            float sum = 0;
            while (j < averageSize)
            {
                sum += spectrum[spectrumIndex];
                spectrumIndex++;
                j++;
            }

            float scaleY = sum / averageSize * visualModifier;
            visualScale[visualIndex] -= Time.deltaTime * smoothSpeed;
            if (visualScale[visualIndex] < scaleY)
                visualScale[visualIndex] = scaleY;

            visualList[visualIndex].localScale = Vector3.one + Vector3.up * visualScale[visualIndex];
            visualIndex++;
        }
    }


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
