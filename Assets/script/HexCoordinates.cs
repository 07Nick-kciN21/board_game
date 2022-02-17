using UnityEngine;

[System.Serializable]
//用来显示在Inspector上cell坐标
[SerializeField]
public struct HexCoordinates
{
    public int x, z;
    public int X {
        get
        {
            return x;
        }
    }
    public int Z {
        get
        {
            return z;
        }
    }
    public int Y
    {
        get
            {
                return -X - Z;
            }
    }

    /// <summary>
    /// 重载默认的构造函数
    /// </summary>
    /// <param name="x">为转换后的X坐标赋初始值</param>
    /// <param name="z">为转换后的Z坐标赋初始值</param>
    public HexCoordinates(int x, int z)
    {
        //X = x;
        //Z = z;

        this.x = x;
        this.z = z;
    }

    public static HexCoordinates FromPosition(Vector3 position)
    {
        float x = position.x / (HexMetrics.innerRadius * 2f);
        float y = -x;

        float offset = position.z / (HexMetrics.outerRadius * 3f);
        x -= offset;
        y -= offset;

        int ix = Mathf.RoundToInt(x);
        int iy = Mathf.RoundToInt(y);
        int iz = Mathf.RoundToInt(-x - y);

        if (ix + iy + iz != 0)
        {
            //Debug.Log("rounding error!");
            float dx = Mathf.Abs(x - ix);
            float dy = Mathf.Abs(y - iy);
            float dz = Mathf.Abs(-x - y - iz);

            if (dx > dy && dx > dz)
            {
                ix = -iy - iz;
            }
            else if (dz > dy)
            {
                iz = -ix - iy;
            }
        }
        return new HexCoordinates(ix, iz);
    }

    /// <summary>
    /// 进行X与Z的坐标转换，将X方向锯齿状的排列，改为斜向的排列
    /// </summary>
    /// <param name="x">原始cell的x轴坐标</param>
    /// <param name="z">原始cell的z轴坐标</param>
    /// <returns>目前返回传入的参数值</returns>
    public static HexCoordinates FromOffsetCoordinates(int x, int z)
    {
        return new HexCoordinates(x - z / 2, z);
    }

    /// <summary>
    /// 重载默认的ToString方法，使其返回的是X和Z的坐标值
    /// </summary>
    /// <returns>X和Z的坐标值</returns>
    public override string ToString()
    {
        return "(" + X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ")";
    }

    /// <summary>
    /// 将转换后的X和Z的坐标值添加换行符，以便显示在UGUI的每个cell上
    /// </summary>
    /// <returns>添加换行符后的X和Z，符合Text组件的富文本格式</returns>
    public string ToStringOnSeparateLines()
    {
        return X.ToString() + "\n" + Y.ToString() + "\n" + Z.ToString();
    }


    
}

