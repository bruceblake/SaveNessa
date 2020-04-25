using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public GameObject crosshair;
    public float test;
    Vector3 target;

        void Start()
    {
       
    }
    private void Update()
    {
        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        if (!Cursor.visible)
        {
            crosshair.transform.position = new Vector2(target.x, target.y);
        }
    }


}
