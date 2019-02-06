using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class ProceduralTerrainMesh : MonoBehaviour
{

    public int xSize = 20;
    public int zSize = 20;

    //Noise offset for random results.
    public float offsetX = 100;
    public float offsetZ = 100;

    //Scale of the noise map.
    public float scale = 20f;

    Mesh _mesh;

    Vector3[] _vertices;
    int[] _triangles;

    void Start() {
        _mesh = new Mesh ();
        GetComponent<MeshFilter> ().mesh = _mesh;
        RandomizeData();
        CreateShape();
        UpdateMesh();
    }

    void Update () {

        if(Input.GetKeyDown(KeyCode.Space)) {
            RandomizeData();
            CreateShape();
            UpdateMesh();
        }
    }

    void RandomizeData () {
        offsetX = Random.Range(2f, 50);
        offsetZ = Random.Range(2f, 50);
        scale = Random.Range(5f, 50f);
    }

    void CreateShape() {

        _vertices = new Vector3 [(xSize + 1) * (zSize + 1)];

        for (int i =0, z = 0; z <= zSize; z++) {
            for (int x = 0; x <= xSize; i++, x++) {
                //Perlin noise repeats at whole numbers and it needs decimal values.
                float xCoord = (float) x / xSize * scale + offsetX;
                float zCoord = (float) z / zSize * scale + offsetZ;
                float y = Mathf.PerlinNoise (xCoord, zCoord);
                _vertices[i] = new Vector3(x,y,z);
            }
        }

        _triangles = new int[6 * xSize *zSize];

        int vert =0;
        int tris = 0;

        for (int z = 0; z < zSize; z++) {

            for (int x = 0; x < xSize; x++) {
                
                _triangles[tris + 0] = vert + 0;
                _triangles[tris + 1] = vert + xSize + 1;
                _triangles[tris + 2] = vert + 1;
                _triangles[tris + 3] = vert + 1;
                _triangles[tris + 4] = vert + xSize + 1;
                _triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris+=6;
            }

            vert++;
        }
    }

    void UpdateMesh() {
        _mesh.Clear();
        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;
        _mesh.RecalculateNormals();
    }

}
