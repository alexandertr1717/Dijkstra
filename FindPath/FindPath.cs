using UnityEngine;
using WPMF;

public class FindPath : MonoBehaviour
{
    public static FindPath Instance;

    public Graph graph = new Graph();
    public Dijkstra dijkstra;
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        dijkstra = new Dijkstra(graph);
    }
   
    public void AddEdge(City c1, City c2)
    {
        graph.AddVertex(c1);
        graph.AddVertex(c2);
        graph.AddEdge(c1.name, c2.name, Distance(c1, c2));
    }

    public void DeleteEdge(City c1, City c2)
    {
        graph.DeleteEdge(c1.name, c2.name);
        for (int i = 0; i < c1.connectedCities.Count; i++)
        {
            if(c2==c1.connectedCities[i])
            {
                c1.connectedCities.Remove(c2);
            }
        }
        for (int i = 0; i < c2.connectedCities.Count; i++)
        {
            if (c1 == c2.connectedCities[i])
            {
                c2.connectedCities.Remove(c1);
            }
        }
    }

    public City GetIntermediateDestination(City current, City target)
    {
      
        City city = dijkstra.FindIntermediateCity(current.graphVertex.Count!=0? current.graphVertex[0]:null, target.graphVertex.Count != 0 ? target.graphVertex[0] : null);
       
        return city;
    }

    static float Distance(float latDec1, float lonDec1, float latDec2, float lonDec2)
    {
        float R = 6371000; // metres
        float phi1 = latDec1 * Mathf.Deg2Rad;
        float phi2 = latDec2 * Mathf.Deg2Rad;
        float deltaPhi = (latDec2 - latDec1) * Mathf.Deg2Rad;
        float deltaLambda = (lonDec2 - lonDec1) * Mathf.Deg2Rad;

        float a = Mathf.Sin(deltaPhi / 2) * Mathf.Sin(deltaPhi / 2) +
            Mathf.Cos(phi1) * Mathf.Cos(phi2) *
                Mathf.Sin(deltaLambda / 2) * Mathf.Sin(deltaLambda / 2);
        float c = 2.0f * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1.0f - a));
        return R * c;
    }

    public static int Distance(City city1, City city2)
    {
        float latDec1 = 180.0f * city1.unity2DLocation.y;
        float lonDec1 = 360.0f * (city1.unity2DLocation.x + 0.5f) - 180.0f;
        float latDec2 = 180.0f * city2.unity2DLocation.y;
        float lonDec2 = 360.0f * (city2.unity2DLocation.x + 0.5f) - 180.0f;
        return (int)Distance(latDec1, lonDec1, latDec2, lonDec2);
    }

    public static int Distance(Vector2 a, Vector2 b)
    {
        float latDec1 = 180.0f * a.y;
        float lonDec1 = 360.0f * (a.x + 0.5f) - 180.0f;
        float latDec2 = 180.0f * b.y;
        float lonDec2 = 360.0f * (b.x + 0.5f) - 180.0f;
        return (int)Distance(latDec1, lonDec1, latDec2, lonDec2);
    }
}
