using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestVectors : MonoBehaviour
{
    public Vector3 v1, v2, v3;
    float k;
    GameObject cubev1;
    
    // Start is called before the first frame update
    void Start()
    {

        //Each component represents how much the vector moves along each axis
        //u = A->B = (xb-xa, yb-ya, zb-za )
        //x-component: Movement along the horizontal axis.
        //y-component: Movement along the vertical axis.
        //z-component: Movement along the depth axis.
        v1 = new Vector3(1, 2, 3);
        v2 = new Vector3(2, -1, 1);
        k = 1.5f; //scaler

        Vector3 v1_plus_v2 = v1 + v2;
        Vector3 v2_plus_v1 = v2 + v1;
        print($"v1_plus_v2={v1_plus_v2}, v2_plus_v1={v2_plus_v1}, diff={v1_plus_v2- v2_plus_v1}");

        //TODO:
        //Calc: v1.k, k.V1, print result
        Vector3 v1_times_k = v1 * k;
        Vector3 k_times_v1 = k * v1;
        //scaler * vector = vector * scaler //result: vector
        //ku = (ku1, ku2, ku3)
        PrintResult("v1_times_k", v1_times_k);
        PrintResult("k_times_v1", k_times_v1);

        //Calc: v1_hat (unit vector), v2_hat 
        //normalizing a vector: v_hat = v / |v|
        Vector3 v1_hat = v1.normalized;
        Vector3 v2_hat = v2.normalized;
        PrintResult("v1_hat", v1_hat);
        PrintResult("v2_hat", v2_hat);

        //Calc: v1.v2 (dot product), v1_hat.v2_hat
        //results: scaler
        //u . v = u1v1 + u2v2 + u3v3
        float v1_dot_v2 = Vector3.Dot(v1, v2);
        float v1_hat_dot_v2_hat = Vector3.Dot(v1_hat, v2_hat);
        PrintResult("v1_dot_v2", v1_dot_v2);
        PrintResult("v1_hat_dot_v2_hat", v1_hat_dot_v2_hat);

        //Calc: v1 x v2 (Cross product), v2 x v1, compare them
        //results: vector
        //u x v = (u2v3 - u3v2, u3v1 - u1v3, u1v2 - u2v1)
        Vector3 v1_cross_v2 = Vector3.Cross(v1, v2);
        Vector3 v2_cross_v1 = Vector3.Cross(v2, v1);
        PrintResult("v1_cross_v2", v1_cross_v2);
        PrintResult("v2_cross_v1", v2_cross_v1);
        PrintResult("diff", v1_cross_v2 - v2_cross_v1);

        //Calc. Magnitude, Distance, 1-2 Lerps
        //magnitude = length of vector
        //||u|| = sqrt(u1^2 + u2^2 + u3^2)
        float v1_magnitude = v1.magnitude;
        float v2_magnitude = v2.magnitude;
        float v1_v2_distance = Vector3.Distance(v1, v2);
        Vector3 v1_to_v2 = v2 - v1;
        Vector3 v1_to_v2_half = Vector3.Lerp(v1, v2, 0.5f);
        Vector3 v1_to_v2_3q = Vector3.Lerp(v1, v2, 0.75f);
        PrintResult("v1_magnitude", v1_magnitude);
        PrintResult("v2_magnitude", v2_magnitude);
        PrintResult("v1_v2_distance", v1_v2_distance);
        PrintResult("v1_to_v2", v1_to_v2);
        PrintResult("v1_to_v2_half", v1_to_v2_half);
        PrintResult("v1_to_v2_3q", v1_to_v2_3q);

        //Lab1 2024-09-04
        //Demo 4-6 of the following:
        //Vector3.Angle
        float angle = Vector3.Angle(v1, v2);
        PrintResult("Vector3.Angle", angle);

        //Vector3.Distance
        float distance = Vector3.Distance(v1, v2);
        PrintResult("Vector3.Distance", distance);

        //Vector3.Normalize
        Vector3 v1_normalized = v1.normalized;
        PrintResult("Vector3.Normalize", v1_normalized);

        //Vector3.SqrMagnitude 
        //sqrMagnitude = magnitude^2
        float v1_sqrMagnitude = v1.sqrMagnitude;
        PrintResult("Vector3.SqrMagnitude", v1_sqrMagnitude);

        //Vector3.Scale
        Vector3 v1_scaled = Vector3.Scale(v1, v2);
        PrintResult("Vector3.Scale", v1_scaled);

        //Vector3.RotateTowards
        Vector3 v1_rotated = Vector3.RotateTowards(v1, v2, 0.5f, 0.5f);
        PrintResult("Vector3.RotateTowards", v1_rotated);

        //Vector3.ClampMagnitude
        Vector3 v1_clamped = Vector3.ClampMagnitude(v1, 1.0f);
        PrintResult("Vector3.ClampMagnitude", v1_clamped);

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PrintResult(string variable, float value)
    {
        Debug.Log($"{variable} = {value}");
    }

    public void PrintResult(string variable, Vector3 value)
    {
        Debug.Log($"{variable} = {value}");
    }
}
