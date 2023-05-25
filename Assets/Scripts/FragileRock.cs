using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileRock : MonoBehaviour
{
    // Sprite Variables
    public Sprite shatteredRock;

    // AudioSource Variables
    private AudioSource shatteredRockAudio;

    // AudioClip Variables
    public AudioClip shatterSFX;

    // SpriteRenderer Variables
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        shatteredRockAudio = GetComponent<AudioSource>();
    }

    // Shatters the fragile rock, and plays the shatter sfx
    public void Shatter() {
        spriteRenderer.sprite = shatteredRock;
        shatteredRockAudio.PlayOneShot(shatterSFX, 1.0f);
    }
}
