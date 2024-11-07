using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    Dictionary<char, Dictionary<char, int>> vertices = new Dictionary<char, Dictionary<char, int>>();

    public void AddVertex(char vertex, Dictionary<char, int> edges)
    {
        vertices[vertex] = edges;
    }

    public List<char> ShortestPathViaDijkstra(char start, char finish)
    {
        //initialize
        List<char> path = new List<char>();
        var distances = new Dictionary<char, int>();
        var previous = new Dictionary<char, char>();
        var pending = new List<char>();

        //step 0
        foreach (var vertex in vertices)
        {
            distances[vertex.Key] = int.MaxValue;
            previous[vertex.Key] = '\0';
            pending.Add(vertex.Key);
        }
        distances[start] = 0;
        //main loop
        while (pending.Count > 0)
        {
            pending.Sort((x, y) => distances[x].CompareTo(distances[y]));
            var u = pending[0];
            pending.Remove(u);

            if (u == finish)
            {
                break;
            }

            if (vertices.ContainsKey(u))
            {
                foreach (var neighbor in vertices[u])
                {
                    char v = neighbor.Key;
                    int weight = neighbor.Value;

                    int alt = distances[u] + weight;
                    if (alt < distances[v])
                    {
                        distances[v] = alt;
                        previous[v] = u;
                    }
                }
            }
        }

        char current = finish;
        while (previous[current] != '\0')
        {
            path.Insert(0, current);
            current = previous[current];
        }

        if (distances[finish] != int.MaxValue)
        {
            path.Insert(0, start);
        }

        return path;
    }

    public List<char> DFS(char start)
    {
        List<char> visited = new List<char>();
        Stack<char> stack = new Stack<char>();
        stack.Push(start);

        while (stack.Count > 0)
        {
            char vertex = stack.Pop();

            if (!visited.Contains(vertex))
            {
                visited.Add(vertex);

                if (vertices.ContainsKey(vertex))
                {
                    foreach (var neighbor in vertices[vertex].Keys)
                    {
                        if (!visited.Contains(neighbor))
                        {
                            stack.Push(neighbor);
                        }
                    }
                }
            }
        }

        return visited;
    }
}
