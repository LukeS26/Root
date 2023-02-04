using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelArray
{
    public int[] Array = new int[19];
}

public class LevelGen : MonoBehaviour
{

    /* Y, X */
    public LevelArray[] level = new LevelArray[10];
    public GameObject[,] levelSave = new GameObject[10,19];

    public GameObject[] objects = new GameObject[5];

    // Start is called before the first frame update
    void Start()
    {
        for(int y = 0; y < level.Length; y++) {
            for(int x = 0; x < level[y].Array.Length; x++) {
                if(level[y].Array[x] == 0) {
                    continue;
                }
                levelSave[y,x] = Instantiate( objects[ level[y].Array[x] ], new Vector3( x - 9f, 4.5f - y, 0 ), Quaternion.identity, transform );
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
