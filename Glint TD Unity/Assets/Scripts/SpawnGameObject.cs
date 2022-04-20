using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGameObject : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject tower;
    bool canplace;
    bool releasedbutton;
    Vector3 mousePos;

    void Start()
    {
        releasedbutton = true;
        canplace = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            releasedbutton = false;
            canplace = true;
            mousePos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            releasedbutton = true;
            canplace = false;
        }


        if (releasedbutton == false && canplace)
        {
            GameObject tmpObj = Instantiate(tower);

            tmpObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
            canplace = false;

        }

    }
}
