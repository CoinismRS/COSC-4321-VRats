using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGeneration : MonoBehaviour
{
    public int maxBounces = 100;
    private LineRenderer lr;
    [SerializeField]
    private Transform startPoint;
    [SerializeField]
    private bool reflectOnlyWall;

    private void Start()
    {
        //get the line renderer component
        lr = GetComponent<LineRenderer>();

        //set position 0 to be start point
        lr.SetPosition(0, startPoint.position);

    }

    private void Update()
    {
        // cast lazer forward
        CastLaser(transform.position, -transform.forward);

    }


    void CastLaser(Vector3 position, Vector3 direction)
    {
        lr.SetPosition(0, startPoint.position);

        for (int i = 0; i < maxBounces; i++)
        {
            Ray ray = new Ray(position, direction);
            RaycastHit hit;

            /*if (Physics.Raycast(ray, out hit, 300, 1))
            {
                position = hit.point;
                direction = Vector3.Reflect(direction, hit.normal);
                lr.SetPosition(i + 1, hit.point);

                if (hit.transform.tag != "Wall" && reflectOnlyWall)
                {
                    for (int j = (i + 1); j <= maxBounces; j++)
                    {
                        lr.SetPosition(j, hit.point);
                    }
                    break;
                }
            }*/

        }
    }
}
