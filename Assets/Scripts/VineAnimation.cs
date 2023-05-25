using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineAnimation : MonoBehaviour
{
    // LineRenderer Variables
    LineRenderer trail;
    
    // Material Variables
    public Material[] mats = new Material[2];

    // Integer Variables
    int curAnimation = 0;

    void Awake()
    {
        trail = gameObject.GetComponent<LineRenderer>();
    }

    public void Animate() {
        curAnimation ++;
        curAnimation %= 2;

        trail.material = mats[curAnimation];
    }
    
}
