using System.Collections.Generic;
using WPMF;

public class Graph
{
    public List<GraphVertex> Vertices { get; set; }

    public Graph()
    {
        Vertices = new List<GraphVertex>();
    }
    public void AddVertex(City city)
    {

            Vertices.Add(new GraphVertex(city));
        city.graphVertex.Add(FindVertex(city.name));
       
    }
    public GraphVertex FindVertex(string vertexName)
    {
        foreach (var v in Vertices)
        {
            if (v.Name.Equals(vertexName))
            {
                return v;
            }
        }

        return null;
    }
    public void AddEdge(string firstName, string secondName, int weight)
    {
        var v1 = FindVertex(firstName);
        var v2 = FindVertex(secondName);
     
        if (v2 != null && v1 != null)
        {
            v1.AddEdge(v2, weight);
            v2.AddEdge(v1, weight);            
        }        
    }
    public void DeleteEdge(string firstName, string secondName)
    {
        var v1 = FindVertex(firstName);
        var v2 = FindVertex(secondName);
        if (v1!=null&& v2!=null&&v1.City.graphVertex.Contains(v1) && v2.City.graphVertex.Contains(v2))
        {
            v1.City.graphVertex.Remove(v1);
            v2.City.graphVertex.Remove(v2);
            if (v1 != null && v2 != null)
            {
                v1.DeleteEdge(v2);
                v2.DeleteEdge(v1);
                if (v1.Edges.Count == 0)
                {
                    Vertices.Remove(v1);
                }
                if (v2.Edges.Count == 0)
                {
                    Vertices.Remove(v2);
                }
            }
        }
    }
}




