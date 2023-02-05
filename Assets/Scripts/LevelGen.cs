using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelGen : MonoBehaviour
{

    /* Y, X */
    public string[] levelCode = new string[10];

    public GameObject[,] levelSave = new GameObject[10,19];

    public GameObject[] objects = new GameObject[5];
 
    string[] levelBackup = new String[10];

    // Start is called before the first frame update
    void Start()
    {  
        GameObject.Find("GameManager").GetComponent<GameManager>().waterTiles = 0;

        int[,] level = ParseLevel(levelCode);
        Array.Copy(levelCode, levelBackup, levelCode.Length);
;

        for(int y = 0; y < level.GetLength(0); y++) {
            for(int x = 0; x < level.GetLength(1); x++) {
                if(level[y,x] == 0) {
                    continue;
                }
                levelSave[y,x] = Instantiate( objects[ level[y,x] ], new Vector3( x - 9f, 4.5f - y, 0 ), Quaternion.identity, transform );

                if(level[y,x] == 5) { GameObject.Find("GameManager").GetComponent<GameManager>().waterTiles ++; }
            }
        }
    }

    public void Restart() {

        foreach(Transform child in transform) {
            Destroy(child.gameObject);
        }

        Array.Copy(levelBackup, levelCode, levelBackup.Length);
        int[,] level = ParseLevel(levelCode);

        for(int y = 0; y < level.GetLength(0); y++) {
            for(int x = 0; x < level.GetLength(1); x++) {
                if(level[y,x] == 0) {
                    continue;
                }

                levelSave[y,x] = Instantiate( objects[ level[y,x] ], new Vector3( x - 9f, 4.5f - y, 0 ), Quaternion.identity, transform );

                if(level[y,x] == 5) { GameObject.Find("GameManager").GetComponent<GameManager>().waterTiles ++; }
            }
        }
    }

    public int[,] ParseLevel(string[] levelCode) {
        // o Rock
        // * Breakable
        // s Soil
        // + Splitter
        // . Empty
        // p Plant
        
        int[,] ret = new int[10,19];

        for (int y = 0; y < levelCode.Length; y++) {
            for(int x = 0; x < levelCode[y].Length; x++) {
                switch (levelCode[y][x]) {
                    case 'o':
                        ret[y,x] = 4;
                        break;
                    case '*':
                        ret[y,x] = 2;
                        break;
                    case 's':
                        ret[y,x] = 1;
                        break;
                    case '+':
                        ret[y,x] = 3;
                        break;
                    case 'g':
                        ret[y,x] = 5;
                        break;
                    default:
                        ret[y,x] = 0;
                        break;
                }
            }
        }

        return ret;
    }

}
