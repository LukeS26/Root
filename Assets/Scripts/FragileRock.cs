using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileRock : MonoBehaviour
{

    public Sprite shatteredRock;
    private AudioSource shatteredRockAudio;
    public AudioClip shatterSFX;

    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        shatteredRockAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shatter() {
        spriteRenderer.sprite = shatteredRock;
        shatteredRockAudio.PlayOneShot(shatterSFX, 1.0f);
    }
}
