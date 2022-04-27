/**** 
 * Created by: Garron Denney
 * Date Created: April 18, 2022
 * 
 * Last Edited by: Aidan Pohl
 * Last Edited: April 23, 2022
 * 
 * Description: projects a circle around the given object, visual to indicate radius
****/


using UnityEngine;
using System.Collections;
[RequireComponent(typeof(LineRenderer))]
public class Radius : MonoBehaviour
{
    [Range(0, 50)]
    public int segments = 50;
    [Range(0, 5)]
    public float xradius = 5;
    [Range(0, 5)]
    public float yradius = 5;
    LineRenderer line;

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();

        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        CreatePoints();
    }

    void CreatePoints()
    {
        float x;
        float y;
        //float z;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)//Generates each point int he circle
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius; //sets the x position
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius; //sets the y position

            line.SetPosition(i, new Vector3(x, y, 0));

            angle += (360f / segments);
        }
    }
}
