using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingDijkstraAlgorithm : MonoBehaviour
{
    void Start()
    {
        Graph g = new Graph();

        g.AddVertex('A', new Dictionary<char, int>() { { 'B', 10 }, { 'C', 12 }, { 'D', 4 }, { 'E', 2 } });
        g.AddVertex('B', new Dictionary<char, int>() { { 'C', 2 }, { 'D', 4 }, { 'E', 5 }, { 'F', 5 } });
        g.AddVertex('C', new Dictionary<char, int>() { { 'B', 6 }, { 'F', 2 } });
        g.AddVertex('D', new Dictionary<char, int>() { { 'B', 3 }, { 'E', 3 } });
        g.AddVertex('E', new Dictionary<char, int>() { { 'B', 3 }, { 'D', 3 }, { 'F', 9 } });
        g.AddVertex('F', new Dictionary<char, int>());

        List<char> shortestPath = g.ShortestPathViaDijkstra('A', 'F');
        string pathOutput = "Shortest path from A to F: " + string.Join(" -> ", shortestPath);
        print(pathOutput);

        List<char> dfsResult = g.DFS('A');
        string dfsOutput = "DFS starting from A: " + string.Join(" -> ", dfsResult);
        print(dfsOutput);
    }
}
