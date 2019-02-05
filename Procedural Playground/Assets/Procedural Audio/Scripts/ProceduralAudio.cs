using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ProceduralAudio : MonoBehaviour {

    [Range(-1f, 1f)]
    public float offset;
    System.Random rand = new System.Random();
    
    // If OnAudioFilterRead is implemented, Unity will insert a custom filter into the
    // audio DSP chain.                       of audio data passed to this delegate.</param>
    void OnAudioFilterRead(float[] data, int channels) {
        for (int i = 0; i < data.Length; i++) {
            data[i] = (float)(rand.NextDouble() * 2.0 - 1.0 + offset);
        }
    }

}