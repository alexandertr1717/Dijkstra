
public class GraphEdge
{
    public GraphVertex ConnectedVertex { get; }
    public int EdgeWeight { get; }

    public int TotalWeight;
    public GraphEdge(GraphVertex connectedVertex, int weight)
    {
        ConnectedVertex = connectedVertex;
        EdgeWeight = weight;
    }
}

