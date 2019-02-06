using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class ProceduralMesh : MonoBehaviour
{

    Mesh _mesh;

    Vector3[] _vertices;
    int[] _triangles;

    void Start() {
        _mesh = new Mesh ();
        GetComponent<MeshFilter> ().mesh = _mesh;
        CreateShape();
        UpdateMesh();
    }

    void CreateShape() {
        _vertices = new Vector3[] {
            new Vector3 (0,0,0),
            new Vector3 (0,0,1),
            new Vector3 (1,0,0),
            new Vector3 (1,0,1)
        };

        _triangles = new int[] {
            0,1,2,
            1,3,2
        };
    }

    void UpdateMesh() {
        _mesh.Clear();
        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;
        _mesh.RecalculateNormals();
    }
}
