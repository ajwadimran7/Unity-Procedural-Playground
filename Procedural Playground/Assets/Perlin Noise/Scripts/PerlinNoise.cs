using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This perlin noise generator will create a new perlin noise map every one second or make the in the start funtion only.

public class PerlinNoise : MonoBehaviour
{
    //Texture resolution.
    public int width = 256;
    public int height = 256;

    //Texture offset for random results.
    public float offsetX = 100;
    public float offsetY = 100;

    //Scale of the noise map.
    public float scale = 20f;

    //Flag to enable disable continuous noise mpa generation.
    public bool makeMapAtStartOnly = false;

    Renderer _renderer;

    void Start () {
        _renderer = GetComponent<Renderer> ();
        if(makeMapAtStartOnly) {
            GenerateTexture ();
        } else {
            StartCoroutine(RegenerateMap());
        }
        
    }

//Generate perlin noise map after every 1 second.
    IEnumerator RegenerateMap() {

        while (true) {
            GenerateTexture();
            yield return new WaitForSeconds(1f);
        }
        
    }

//Creates a noise texture and set's it to Quad's main texture.
    void GenerateTexture () {

        offsetX = Random.Range(0f, 99999f);
        offsetY = Random.Range(0f, 99999f);

        Texture2D texture = new Texture2D (width, height);

        //Generate perlin noise mpa here.
        for (int x = 0; x < width; x++) {
            for (int y =0; y < height; y++) {
                Color color = CalculateColor(x,y);
                texture.SetPixel(x,y, color);
            }
        }

        texture.Apply();

        _renderer.material.mainTexture = texture;
    }

//Get pixel color sample.
    Color CalculateColor(int x, int y) {    

        //Perlin noise repeats at whole numbers and it needs decimal values.
        float xCoord = (float) x / width * scale + offsetX;
        float yCoord = (float) y / height * scale + offsetY;
        //Get color sample.
        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        //Create a color 
        return new Color(sample, sample, sample);
    }

}
