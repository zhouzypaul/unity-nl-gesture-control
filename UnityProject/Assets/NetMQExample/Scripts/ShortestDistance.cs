using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortestDistance : MonoBehaviour
{

    public GameObject princeLab;
    public GameObject arnoldLab;
    public GameObject envLab;
    public GameObject medicalLab;
    public GameObject clickDetector;

    public System.String destination; 


    // a helper to turn coordinates to angles 
    double CoordinatesToTheta(double lat, double lon)
    {
        if (lat > 0) 
        {
            if (lon < 0) 
            {
                return System.Math.Atan(lon / lat) + 6.28;    
            }
                else 
            {
                return System.Math.Atan(lon / lat);
            }
        }
        else 
        {
            return System.Math.Atan(lon / lat) + 3.14;
        };
    }

    // a helper to calculate the distance between a distance and a line
    // double DistanceToPoint(double slope, double lat, double lon)
    // {
    //     double args1 = System.Math.Abs(lat - (slope * lon));
    //     double args2 = System.Math.Sqrt(1 + (slope * slope));
    //     return args1 / args2;
    // }

    // Start is called before the first frame update
    // void Start()
    // {
    // }

    // Update is called once per frame 
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickPosition cp = clickDetector.GetComponent<ClickPosition>();
            double theta = cp.theta;
            double midLat = 0.5 * (cp.minLat + cp.maxLat);
            double midLon = 0.5 * (cp.minLon + cp.maxLon);
            BuildingLocation bd1 = princeLab.GetComponent<BuildingLocation>();
            double lat1 = bd1.lat - midLat;
            double lon1 = bd1.lon - midLon;
            BuildingLocation bd2 = arnoldLab.GetComponent<BuildingLocation>();
            double lat2 = bd2.lat - midLat;
            double lon2 = bd2.lon - midLon;
            BuildingLocation bd3 = envLab.GetComponent<BuildingLocation>();
            double lat3 = bd3.lat - midLat;
            double lon3 = bd3.lon - midLon;
            BuildingLocation bd4 = medicalLab.GetComponent<BuildingLocation>();
            double lat4 = bd4.lat - midLat;
            double lon4 = bd4.lon - midLon;
            double dist1 = System.Math.Abs(CoordinatesToTheta(lat1, lon1) - theta);
            double dist2 = System.Math.Abs(CoordinatesToTheta(lat2, lon2) - theta);
            double dist3 = System.Math.Abs(CoordinatesToTheta(lat3, lon3) - theta);
            double dist4 = System.Math.Abs(CoordinatesToTheta(lat4, lon4) - theta);
            if (dist1 <= System.Math.Min(dist2, System.Math.Min(dist3, dist4)))
            {
                destination = "Prince Engineering Lab";
            }
            else 
            {
                if (dist2 <= System.Math.Min(dist3, dist4))
                {
                    destination = "Arnold Lab";
                }
                else 
                {
                    if (dist3 <= dist4) 
                    {
                        destination = "Urban Environment Lab";
                    }
                    else 
                    {
                        destination = "Medical Research Lab";
                    }
                }
            }
            Debug.Log("the closest destination is " + destination);
        }
    }
}
