using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PortalController : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        
    }

    public void OpenPortal()
    {
        spriteRenderer.enabled = true;
        anim.SetBool("Open", true);
    }

    public void ClosePortal()
    {
        anim.SetBool("Close", true);
    }

    public void HidePortal()
    {
        spriteRenderer.enabled = false;
    }
}
