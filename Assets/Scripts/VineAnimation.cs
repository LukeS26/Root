using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineAnimation : MonoBehaviour
{
    LineRenderer trail;
    
    public Material[] mats = new Material[2];

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
