using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPosition : MonoBehaviour
{
    public double minLat;
    public double minLon;
    public double maxLat;
    public double maxLon;
    public double theta; 
    public double slope;

    public LayerMask clickMask;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // if click is off the map, returns (-1,-1,-1)
            Vector3 clickPosition = -Vector3.one;

            // Method 1: Raycast using colliders 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // remember to tag the camera as main camera!!!
            RaycastHit hit;

            if (Physics.Raycast (ray, out hit, 100f, clickMask))
            {
                clickPosition = hit.point;
            }

            // getting the latitude and longitude 
            double lonRescale = (double) (maxLon - minLon) / 10;
            double latRescale = (double) (maxLat - minLat) / 10;
            double newLon = (double) (clickPosition.x * lonRescale + 0.5 * (minLon + maxLon));
            double newLat = (double) (clickPosition.y * latRescale + 0.5 * (minLat + maxLat)); 

            // the angle of the vector from the positive y axis 
            if (clickPosition.y > 0) 
            {
                if (clickPosition.x < 0) 
                {
                    theta = (System.Math.Atan(clickPosition.x / clickPosition.y) + 6.28);    
                }
                else 
                {
                    theta = System.Math.Atan(clickPosition.x / clickPosition.y);
                }
            }
            else 
            {
                theta = (System.Math.Atan(clickPosition.x / clickPosition.y) + 3.14) ;
            }

            slope = (double) clickPosition.y / clickPosition.x;

            // float newX = (float) (clickPosition.x * lonRescale);
            // float newY = (float) (clickPosition.y * latRescale);
            // float normalize = (float) (1 / (newX * newX + newY * newY));

            // Method 2: Raycast using Plane 
            // Plane plane = new Plane(Vector3.up, 0f);
            // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // float distanceToPlane;

            // if (plane.Raycast(ray, out distanceToPlane))
            // {
            //     clickPosition = ray.GetPoint(distanceToPlane);
            // }

            // Debug.Log("click position is" + clickPosition);
            Debug.Log("longitutde is " + newLon);
            Debug.Log("latitude is " + newLat);
            // Debug.Log("point vector is" + (clickPosition.x, clickPosition.y));
            // Debug.Log("angle theta is" + theta);
            // Debug.Log("slope is " + slope);
        }
    }
}
