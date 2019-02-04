using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This proceudral terrain generator uses perlin noise to offset the mesh.
//Recommendation: Turn of the makeTerrainAtStartOnly and once you are happy with your terrain just stop the game to apply that height data to terrain.
public class ProceduralTerrain : MonoBehaviour
{
    //Terrain size.
    public int width = 256; //X
    public int height = 256; //Z
    public int depth = 20; //Y

    //Texture offset for random results.
    public float offsetX = 100;
    public float offsetZ = 100;

    //Scale of the noise map.
    public float scale = 20f;

    //Flag to enable disable continuous noise mpa generation.
    public bool makeTerrainAtStartOnly = false;
    //Super creative naming :p
    public bool playCoolAnimationX = false;
    public bool playCoolAnimationZ = false;
    public float animationMultiplier = 5f;

    Terrain _terrain;
    TerrainData _terrainData;

    void Start () {
        _terrain = GetComponent<Terrain> ();
        _terrainData = _terrain.terrainData;

        if(playCoolAnimationX || playCoolAnimationX) {
            GenerateTerrain ();
            return;
        }

        if(makeTerrainAtStartOnly) {
            RandomizeData ();
            GenerateTerrain();
        } else {
            StartCoroutine(RegenerateTerrain());
        }
    }

    void Update() {
        if(playCoolAnimationX)
            offsetX += animationMultiplier * Time.deltaTime;

        if(playCoolAnimationZ)
            offsetZ += animationMultiplier * Time.deltaTime;

        if(playCoolAnimationX || playCoolAnimationX)
            GenerateTerrain();
    }

    //Re-generate terrain after every 1 second.
    IEnumerator RegenerateTerrain() {
        while (true) {
            RandomizeData ();
            GenerateTerrain();
            yield return new WaitForSeconds(1f);
        }
    }

    //Randomizing values to get different terrain mesh each time.
    void RandomizeData () {
        offsetX = Random.Range(0f, 99999f);
        offsetZ = Random.Range(0f, 99999f);
        scale = Random.Range(15f, 30f);
    }

    void GenerateTerrain () {
        //Setting terrain data values to accomodate for the procedural generation
        _terrainData.heightmapResolution = width + 1;
        _terrainData.size = new Vector3 (width, depth, height);
        _terrainData.SetHeights(0,0, GenerateHeights ());
    }

    float[,] GenerateHeights () {

        float[,] Heights = new float[width, height];

        for (int x = 0; x < width; x++) {
            for (int y =0; y < height; y++) {
                Heights[x, y] = CalculateHeights(x,y);
            }
        }
        
        return Heights;
    }

    //Get pixel height sample based on perlin noise.
    float CalculateHeights(int x, int y) {    

        //Perlin noise repeats at whole numbers and it needs decimal values.
        float xCoord = (float) x / width * scale + offsetX;
        float yCoord = (float) y / height * scale + offsetZ;
        //Get a height sample.
        return Mathf.PerlinNoise(xCoord, yCoord);
    }


}
