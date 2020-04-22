using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPosition : MonoBehaviour
{
    public float minLat;
    public float minLon;
    public float maxLat;
    public float maxLon;



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

            if (Physics.Raycast (ray, out hit))
            {
                clickPosition = hit.point;
            }

            float lonRescale = (float) (maxLon - minLon) / 10;
            float latRescale = (float) (maxLat - minLat) / 10;
            float newLon = (float) (clickPosition.x * lonRescale + 0.5 * (minLon + maxLon));
            float newLat = (float) (clickPosition.y * latRescale + 0.5 * (minLat + maxLat)); 

            Vector3 mapPosition = new Vector3(newLon, newLat, 0); 

            // Method 2: Raycast using Plane 
            // Plane plane = new Plane(Vector3.up, 0f);
            // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // float distanceToPlane;

            // if (plane.Raycast(ray, out distanceToPlane))
            // {
            //     clickPosition = ray.GetPoint(distanceToPlane);
            // }

            Debug.Log("click position is" + clickPosition);
            Debug.Log("longitutde is " + newLon);
            Debug.Log("latitude is " + newLat);
        }
    }
}
