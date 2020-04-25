using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorHoverShopButton : MonoBehaviour
{
    public Crosshair crosshair;

    
    void Start()
    {
        crosshair = FindObjectOfType<Crosshair>().GetComponent<Crosshair>();
    }

    private void OnMouseOver()
    {
        crosshair.crosshair.SetActive(false);
    }
    private void OnMouseExit()
    {
        crosshair.crosshair.SetActive(true);
    }
}
