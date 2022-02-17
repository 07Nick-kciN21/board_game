using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class HexMapEditor : MonoBehaviour
{
    public Color[] colors;

    public HexGrid hexGrid;

    private Color activeColor;

    [SerializeField] private Camera mainCamera;

    // Start is called before the first frame update
    void Awake()
    {
        SelectColor(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            HandleInput();
        }
    }

    private void HandleInput()
    {
        //射线起点为鼠标位置，经过主摄像机
        Ray _inputRay = mainCamera.ScreenPointToRay(Input.mousePosition);

        //检测射线是否碰撞到了collider
        RaycastHit _hit;
        if (Physics.Raycast(_inputRay, out _hit))
        {
            hexGrid.ColorCell(_hit.point, activeColor);
        }
    }

    public void SelectColor(int _index)
    {
        activeColor = colors[_index];
    }


}
