using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeuristicStrategy9 { EuclideanDistance, ManhatanDistance, DictionaryDistance }

public class Graph9
{
    HeuristicStrategy9 strategy;

    public Graph9(HeuristicStrategy9 strategy = HeuristicStrategy9.EuclideanDistance)
    {
        this.strategy = strategy;
    }

    Dictionary<char, Dictionary<char, int>> vertices = new Dictionary<char, Dictionary<char, int>>();
    Dictionary<char, Vector3> verticesData = new Dictionary<char, Vector3>();

    public void add_vertex_for_AStar(char vertex, Vector3 pos, Dictionary<char, int> edges)
    {
        vertices[vertex] = edges;
        verticesData[vertex] = pos;
    }

    public float EuclideanDistance(Vector3 v1, Vector3 v2)
    {
        return Vector3.Distance(v1, v2);
    }

    public float ManhatanDistance(Vector3 v1, Vector3 v2)
    {
        return Mathf.Abs(v1.x - v2.x) + Mathf.Abs(v1.y - v2.y) + Mathf.Abs(v1.z - v2.z);
    }

    public float GoalDistanceEstimate(char node, char finish)
    {
        switch (strategy)
        {
            case HeuristicStrategy9.EuclideanDistance:
                return EuclideanDistance(verticesData[node], verticesData[finish]);
            case HeuristicStrategy9.ManhatanDistance:
                return ManhatanDistance(verticesData[node], verticesData[finish]);
            case HeuristicStrategy9.DictionaryDistance:
                return verticesData[node].x;
            default:
                return 0f;
        }
    }

    public List<char> shortest_path_via_AStar_algo(char start, char finish)
    {
        List<char> path = new List<char>();
        var previous = new Dictionary<char, char>();
        var distances = new Dictionary<char, float>();
        var gScore = new Dictionary<char, float>();
        var Pending = new List<char> { start };
        var Closed = new HashSet<char>();

        gScore[start] = 0;
        distances[start] = gScore[start] + GoalDistanceEstimate(start, finish);
        previous[start] = '-';

        while (Pending.Count > 0)
        {
            Pending.Sort((x, y) => distances[x].CompareTo(distances[y]));
            char u = Pending[0];
            Pending.Remove(u);

            if (u == finish)
            {
                while (previous[u] != '-')
                {
                    path.Add(u);
                    u = previous[u];
                }
                path.Add(start);
                path.Reverse();
                return path;
            }

            Closed.Add(u);

            foreach (var neighbor in vertices[u])
            {
                char v = neighbor.Key;
                int weight = neighbor.Value;

                if (Closed.Contains(v))
                    continue;

                float tentative_gScore = gScore[u] + weight;

                if (!Pending.Contains(v))
                    Pending.Add(v);
                else if (tentative_gScore >= gScore.GetValueOrDefault(v, float.MaxValue))
                    continue;

                previous[v] = u;
                gScore[v] = tentative_gScore;
                distances[v] = gScore[v] + GoalDistanceEstimate(v, finish);
            }
        }

        return path;
    }
}
