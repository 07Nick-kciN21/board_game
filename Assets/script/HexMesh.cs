using System.Collections;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{
    //存储计算生成后的mesh
    private Mesh hexMesh;

    //存储所有正六边形的顶点位置信息
    private List<Vector3> vertices;

    //索引，每个三角面片顶点的渲染顺序
    private List<int> triangles;

    private List<Color> colors;
    private MeshCollider meshCollider;
    void Awake()
    {
        //初始化MeshFilter组件的，实例化hexMesh，并给其命名
        GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
        hexMesh.name = "Hex Mesh";

        meshCollider = gameObject.AddComponent<MeshCollider>();
        //初始化vertices和triangles组件
        vertices = new List<Vector3>();
        triangles = new List<int>();

        colors = new List<Color>();
    }

    public void Triangulate(HexCell[] cells)
    {
        hexMesh.Clear();
        vertices.Clear();
        triangles.Clear();
        colors.Clear();

        for (int i = 0; i < cells.Length; i++)
        {
            Triangulate(cells[i]);
        }

        hexMesh.vertices = vertices.ToArray();
        hexMesh.triangles = triangles.ToArray();

        hexMesh.RecalculateNormals();
        hexMesh.colors = colors.ToArray();
        meshCollider.sharedMesh = hexMesh;
    }

    private void Triangulate(HexCell cell)
    {
        //获取单个cell的中点位置
        Vector3 center = cell.transform.localPosition;

        //根据中点位置计算出其余的顶点位置信息
        for (int i = 0; i < 6; i++)
        {
            AddTriangle(
                center,
                center + HexMetrics.corners[i],
                center + HexMetrics.corners[i + 1]
            );
            AddTriangleColor(cell.color);
        }
    }

    private void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        //获取当前vertices链表中已经录入的数量
        int vertexIndex = vertices.Count;

        //在vertices链表中添加新增的顶点位置信息
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);

        //在triangles链表中添加新增顶点的索引
        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
    }

    private void AddTriangleColor(Color color)
    {
        colors.Add(color);
        colors.Add(color);
        colors.Add(color);
    }
}
