using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//控制标记点的脚本
public class WayPoints : MonoBehaviour
{
    public static Transform[] positions;
    // Start is called before the first frame update
    private void Awake()
    {
        positions = new Transform[transform.childCount];
        for(int i = 0; i < positions.Length; i++)
        {
            positions[i] = transform.GetChild(i);
        }
    }
}
