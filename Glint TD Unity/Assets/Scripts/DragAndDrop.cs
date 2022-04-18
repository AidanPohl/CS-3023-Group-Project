










using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    Vector3 offset;
    // Start is called before the first frame update


    void OnMouseDown()
    {
        offset = transform.position - MouseWorldPosition();
    }
    void OnMouseDrag()
    {
        transform.position = MouseWorldPosition() + offset;
    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}
