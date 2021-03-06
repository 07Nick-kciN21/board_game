using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HexGrid : MonoBehaviour
{
    public int width = 6;
    public int height = 6;
    public HexCell cellPrefab;
    private Vector3[] vertices;

    public Text cellLabelPrefab;

    public Color defaultColor = Color.white;
    //public Color touchedColor = Color.magenta;

    //public Color touchedColor = Color.magenta;
    Canvas gridCanvas;
    HexMesh hexMesh;
    HexCell[] cells;

    void Awake()
    {
        this.gridCanvas = GetComponentInChildren<Canvas>();
        hexMesh = GetComponentInChildren<HexMesh>();
        cells = new HexCell[height * width];

        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateCell(x, z, i++);
            }
        }
    }

    private void Start()
    {
        hexMesh.Triangulate(cells);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleInput();
        }
    }

    private void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if(Physics.Raycast(inputRay, out hit))
        {
            //TouchCell(hit.point);
            
        }
    }

    //void TouchCell(Vector3 position)
    //{
    //    position = transform.InverseTransformPoint(position);
    //    HexCoordinates coordinates = HexCoordinates.FromPosition(position);
    //    int index = coordinates.x + coordinates.z * width + coordinates.z / 2;
    //    HexCell cell = cells[index];
    //    cell.color = touchedColor;
    //    hexMesh.Triangulate(cells);
    //    Debug.Log("touched at " + position);
    //}

    public void ColorCell(Vector3 position, Color color)
    {
        position = transform.InverseTransformPoint(position);

        HexCoordinates coordinates = HexCoordinates.FromPosition(position);

        int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;

        HexCell cell = cells[index];

        cell.color = color;

        hexMesh.Triangulate(cells);
        Debug.Log("touched at" + coordinates.ToString());
    }

    void CreateCell(int x, int z, int i)
    {
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
        position.y = 0f;
        position.z = z * (HexMetrics.outerRadius * 1.5f);

        HexCell cell = cells[i] = Instantiate(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;

        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);

        cell.color = defaultColor;
        Text label = Instantiate(cellLabelPrefab);
        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition =
               new Vector2(position.x, position.z);
        //label.text = x.ToString() + "\n" + z.ToString();

        label.text = cell.coordinates.ToStringOnSeparateLines();
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }
        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }

}
