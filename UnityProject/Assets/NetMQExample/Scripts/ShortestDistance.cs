using System.Collections;
using System.Collections.Generic;
using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ShortestDistance : MonoBehaviour
{
    public GameObject clickDetector;
    public int topChoiceNum;

    System.String destination; 

    public GameObject prefab;
    public GameObject parent;
    public Text inputStream;
    public bool firstLoad = false;


    // Start is called before the first frame update
    // Dynamically instantiate all the objects 
    void Start()
    {

    }

    void CreateTarget( )
    {         // get info from the ClikcPosition script 
        ClickPosition cp = clickDetector.GetComponent<ClickPosition>();
        double lonRescale = (cp.maxLon - cp.minLon) / 10;
        double latRescale = (cp.maxLat - cp.minLat) / 10;
        double midLat = 0.5 * (cp.minLat + cp.maxLat);
        double midLon = 0.5 * (cp.minLon + cp.maxLon);

        // parse the input stream and instantiate objects 
        System.String input = inputStream.text;
        System.String[] buildingArray = input.Split(';');
        for (int i = 0; i < topChoiceNum; i++)
        {
            System.String buildingInfo = buildingArray[i];
            NumberFormatInfo provider = new NumberFormatInfo();
            System.String[] info = buildingInfo.Split(',');
            System.String name = info[0];
            double lat = Convert.ToDouble(info[1], provider);
            double lon = Convert.ToDouble(info[2], provider);

            float x = (float)((lon - midLon) / lonRescale);
            float y = (float)((lat - midLat) / latRescale);
            Vector3 location = new Vector3(x, y, 0);
            GameObject newBuilding = Instantiate(prefab,
                                                 location,
                                                 Quaternion.identity);
            BuildingLocation bl = newBuilding.GetComponent<BuildingLocation>();
            bl.name = name;
            bl.lat = lat;
            bl.lon = lon;
            newBuilding.name = name;
            // newBuilding.transform.Find("Name").GetComponent<TextMesh>().text
            //     = name;
            newBuilding.transform.SetParent(parent.transform);
        }
    }


    // Update is called once per frame 
    void Update()
    {
        if ( inputStream.text.Length > 10 && firstLoad == false)
        {
            CreateTarget();
            firstLoad = true;
        }




        if (Input.GetMouseButtonDown(0))
        {
            ClickPosition cp = clickDetector.GetComponent<ClickPosition>();
            double theta = cp.theta;
            double midLat = 0.5 * (cp.minLat + cp.maxLat);
            double midLon = 0.5 * (cp.minLon + cp.maxLon);
            double diff = 6.28;
            System.String destination = "";
            if (parent.transform.childCount != 0) 
            {
                foreach (Transform child in parent.transform)
                {
                    BuildingLocation bl = child.GetComponent<BuildingLocation>();
                    double innerLat = bl.lat - midLat;
                    double innerLon = bl.lon - midLon;
                    double innerDiff = System.Math.Abs(CoordinatesToTheta(innerLat, innerLon) - theta);
                    if (innerDiff < diff) 
                    {
                        diff = innerDiff;
                        destination = bl.name;
                    }
                }
            }
            Debug.Log("the closest destination is " + destination);
        }
    }

    
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

}
