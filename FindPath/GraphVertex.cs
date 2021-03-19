using System.Collections.Generic;
using WPMF;
using System.Linq;


public class GraphVertex
{
   
    public City City { get; }
    public string Name { get; }
    public int number;

    public List<GraphEdge> Edges { get; }
 
    public City parent;
    public int totalWeight;  
    public GraphVertex(City city)
    {
        Name = city.name;
        City = city;
        Edges = new List<GraphEdge>();
    }
    public void AddEdge(GraphEdge newEdge)
    {
        Edges.Add(newEdge);
    }
    public void AddEdge(GraphVertex vertex, int edgeWeight)
    {
        AddEdge(new GraphEdge(vertex, edgeWeight));
    }
    public void DeleteEdge(GraphVertex vertex)
    {
      
        Edges.Remove(Edges.Where(e => e.ConnectedVertex == vertex).FirstOrDefault());
    }
    public int GetEdgeCount()
    {
        return Edges.Count();
    }
    public override string ToString() => Name;
}
