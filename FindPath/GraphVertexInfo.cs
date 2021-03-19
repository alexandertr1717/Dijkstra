using System.Collections.Generic;


public class GraphVertexInfo
{
    public GraphVertex Vertex { get; set; }
    public List<GraphEdge> Edges { get; set; }
  
    public bool IsUnvisited { get; set; }

    
    public int EdgesWeightSum { get; set; }

    public GraphVertex PreviousVertex { get; set; }
    public int number;
    public GraphVertexInfo(GraphVertex vertex, int number)
    {
        Vertex = vertex;
        IsUnvisited = true;
        EdgesWeightSum = int.MaxValue;
        PreviousVertex = null;
        Vertex.number = number;
        this.number = number;
    }
}