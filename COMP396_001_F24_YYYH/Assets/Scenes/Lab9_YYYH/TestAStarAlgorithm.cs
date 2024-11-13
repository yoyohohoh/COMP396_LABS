using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAStarAlgorithm : MonoBehaviour
{
    void Start()
    {
        // Test with Euclidean Distance heuristic
        Graph9 g1 = new Graph9(HeuristicStrategy9.EuclideanDistance);
        InitializeGraph(g1);
        List<char> shortestPath1 = g1.shortest_path_via_AStar_algo('A', 'Z');
        Debug.Log("Shortest path (Euclidean): " + string.Join(" -> ", shortestPath1));

        // Test with Manhattan Distance heuristic
        Graph9 g2 = new Graph9(HeuristicStrategy9.ManhatanDistance);
        InitializeGraph(g2);
        List<char> shortestPath2 = g2.shortest_path_via_AStar_algo('A', 'Z');
        Debug.Log("Shortest path (Manhattan): " + string.Join(" -> ", shortestPath2));

        // Test with Dictionary Distance heuristic
        Graph9 g3 = new Graph9(HeuristicStrategy9.DictionaryDistance);
        InitializeGraph(g3);
        List<char> shortestPath3 = g3.shortest_path_via_AStar_algo('A', 'Z');
        Debug.Log("Shortest path (Dictionary): " + string.Join(" -> ", shortestPath3));
    }

    void InitializeGraph(Graph9 graph)
    {


        // Adding vertices and edges based on the DOT graph
        graph.add_vertex_for_AStar('A', new Vector3(0, 4, 0), new Dictionary<char, int> { { 'B', 4 }, { 'X', 4 } }); // A -> B, A -> X
        graph.add_vertex_for_AStar('B', new Vector3(4, 4, 0), new Dictionary<char, int> { { 'A', 4 }, { 'C', 4 } }); // B -> A, B -> C
        graph.add_vertex_for_AStar('C', new Vector3(8, 4, 0), new Dictionary<char, int> { { 'B', 4 }, { 'Z', 8 } }); // C -> B, C -> Z
        graph.add_vertex_for_AStar('X', new Vector3(0, 0, 0), new Dictionary<char, int> { { 'A', 4 }, { 'Y', 4 } }); // X -> A, X -> Y
        graph.add_vertex_for_AStar('Y', new Vector3(4, 0, 0), new Dictionary<char, int> { { 'X', 4 }, { 'Z', 6 } }); // Y -> X, Y -> Z
        graph.add_vertex_for_AStar('Z', new Vector3(8, 0, 0), new Dictionary<char, int> { { 'C', 8 }, { 'Y', 6 }, { 'W', 4 } }); // Z -> C, Z -> Y, Z -> W
        graph.add_vertex_for_AStar('W', new Vector3(12, 0, 0), new Dictionary<char, int> { { 'Z', 4 } }); // W -> Z



    }
}
