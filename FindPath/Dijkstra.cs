using System;
using System.Collections.Generic;
using WPMF;


public class Dijkstra
{
    Graph graph;
    GraphVertexInfo [] infos;
    GraphVertexInfo [] _infos;
  
    public Dijkstra(Graph graph)
    {
        this.graph = graph;
    }

    
   public void InitInfo()
    {
        infos = new GraphVertexInfo[graph.Vertices.Count];

        for (int i = 0; i < graph.Vertices.Count; i++)
        {
            infos[i]= new GraphVertexInfo(graph.Vertices[i], i);

        }
    }
    void Init()
    {
        _infos = new GraphVertexInfo[ infos.Length ];
        Array.Copy(infos, _infos, infos.Length);

    }
    public GraphVertexInfo FindUnvisitedVertexWithMinSum(GraphVertexInfo graph)
    {

        var minValue = int.MaxValue;
        GraphVertexInfo minVertexInfo = null;
        for (int i = 0; i < infos.Length; i++)
        {
            if (infos[i].IsUnvisited && infos[i].EdgesWeightSum < minValue)
            {
                minVertexInfo = infos[i];
                minValue = infos[i].EdgesWeightSum;
                break;
            }
        }
        return minVertexInfo;
    }
   
    GraphVertexInfo GetVertexInfo(GraphVertex v)
    {
        for (int i = 0; i < infos.Length; i++)
        {
            if (infos[i].Vertex == v)
            {
                return infos[i];
            }
        }


        return null;
    }

    public City FindIntermediateCity(GraphVertex startVertex, GraphVertex finishVertex)
    {
        GraphVertexInfo temp = null;
        if(startVertex==null|| startVertex.number>infos.Length-1)
        {
            return null;
        }
        GraphVertexInfo first = infos[startVertex.number];
        if (first != null && finishVertex != null)
        {
            first.EdgesWeightSum = 0;

            while (true)
            {
                GraphVertexInfo current = FindUnvisitedVertexWithMinSum(temp);
                if (current == null)
                {
                    break;
                }
                SetSumToNextVertex(current);
            }

            return GetNextCity(startVertex, finishVertex);
        }
        else
        {

            return null;
        }
    }

    void SetSumToNextVertex(GraphVertexInfo info)
    {
        GraphVertexInfo nextInfo;
        int sum;
        info.IsUnvisited = false;
        foreach (var e in info.Vertex.Edges)
        {
             nextInfo = infos[e.ConnectedVertex.number];
             sum = info.EdgesWeightSum + e.EdgeWeight;
            if ( sum < nextInfo.EdgesWeightSum)
            {
                nextInfo.EdgesWeightSum = sum;
                nextInfo.PreviousVertex = info.Vertex;
            }
        }
    }
    
    
    string GetPath(GraphVertex startVertex, GraphVertex endVertex)
    {   
        var path = endVertex.ToString();
        while (startVertex != endVertex)
        {
            endVertex = infos[endVertex.number].PreviousVertex;
            path = endVertex.ToString() + path;
        }

        return path;
    }

    City GetNextCity(GraphVertex startVertex, GraphVertex endVertex)
    {
        var path = new List<City>();
        path.Add(endVertex.City);
        while (startVertex != endVertex)
        {
            if (endVertex.number > infos.Length - 1)
            {
                GraphVertexInfo temp = GetVertexInfo(endVertex);
                if (temp != null)
                {
                    GraphVertex vert = temp.Vertex;
                    endVertex = temp.PreviousVertex;
                    path.Add(endVertex.City);
                }
                else { return null; }

            }
            else
            {
                endVertex = infos[endVertex.number].PreviousVertex;
                if (endVertex == null)
                    return null;
                path.Add(endVertex.City);
            }
        }
        return path[path.Count - 2];
    }
}
