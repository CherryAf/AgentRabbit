using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class change_sprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite broken;
    private Events events;
    public GameObject canvas;

    [HideInInspector]
    public bool isBroken;
    // Start is called before the first frame update
    void Start()
    {
        events = canvas.GetComponent<Events>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(UnityEngine.Collision2D other)
    {
        if (other.gameObject.layer == 8)
        {
            Debug.Log("collision");
            spriteRenderer.sprite = broken;
            isBroken = true;
            AkSoundEngine.PostEvent("BreakEffect", null);
            events.timeLeft = -1;
        }
    }
}
