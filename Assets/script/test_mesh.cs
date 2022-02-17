using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_mesh : MonoBehaviour
{
    LineRenderer lineRenderer;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        GameObject main = this.gameObject;
        GameObject triangleGameObject = GreateTriangle();
        triangleGameObject.transform.parent = main.transform;
    }

    GameObject GreateTriangle()
    {
        string name = "triangle";

        Mesh mesh = new Mesh();
        mesh.name = name;

        Vector3[] vertices = new Vector3[3]
        {
            new Vector3(0,0,0),
            new Vector3(0,10,0),
            new Vector3(10,0,0)
        };
        mesh.vertices = vertices;

        int[] triangles = new int[3] { 0, 1, 2 };
        mesh.triangles = triangles;

        GameObject triangleGameObject = new GameObject(name);
        MeshFilter mf = triangleGameObject.AddComponent<MeshFilter>();
        mf.sharedMesh = mesh;

        MeshRenderer meshRenderer = triangleGameObject.AddComponent<MeshRenderer>();
        while(index < 3)
        {
            lineRenderer.SetPosition(triangles[index], vertices[index]);
            index++;
        }
        lineRenderer.SetPositions(vertices);

        return triangleGameObject;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
