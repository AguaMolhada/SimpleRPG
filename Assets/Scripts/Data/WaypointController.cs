// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WaypointController.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2018.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class WaypointController : MonoBehaviour
{
    public Path MovePath;
    private LineRenderer _linePathRenderer;

    private int CurveCount => MovePath.Points.Count;
    private int layerOrder = 0;
    private int segment_count = 20;

    public GameObject test;

    private void Start()
    {
        _linePathRenderer = GetComponent<LineRenderer>();
        _linePathRenderer.useWorldSpace = true;
        _linePathRenderer.material = new Material(Shader.Find("Particles/Additive"));
        _linePathRenderer.sortingLayerID = layerOrder;
    }

    private void Update()
    {
        _linePathRenderer.startColor = Color.blue;
        _linePathRenderer.endColor = Color.blue;
        _linePathRenderer.startWidth = 0.2f;
        _linePathRenderer.endWidth = 0.2f;

        DrawCurve();
    }

    private void DrawCurve()
    {
        for (int j = 0; j < CurveCount; j++)
        {
            for (int i = 1; i <= segment_count; i++)
            {
                Vector3 pixel;
                float t = i / (float) segment_count;
                if (j < CurveCount-1)
                {
                    pixel =
                        CalculateCubicBezierPoint(t,
                            MovePath.Points[j].StartPoint.transform.position,
                            MovePath.Points[j].ControlStartPoint.transform.position,
                            MovePath.Points[j].ControlEndPoint.transform.position,
                            MovePath.Points[j].EndPoint.transform.position);

                    _linePathRenderer.positionCount = (((j * segment_count) + i));
                    _linePathRenderer.SetPosition((j * segment_count) + (i - 1), pixel);
                }
                else
                {
                    if (MovePath.Loop)
                    {
                        pixel =
                            CalculateCubicBezierPoint(t,
                                MovePath.Points[j].StartPoint.transform.position,
                                MovePath.Points[j].ControlStartPoint.transform.position,
                                MovePath.Points[j].ControlEndPoint.transform.position,
                                MovePath.Points[0].StartPoint.transform.position);
                    }
                    else
                    {
                        pixel =
                            CalculateCubicBezierPoint(t,
                                MovePath.Points[j].StartPoint.transform.position,
                                MovePath.Points[j].ControlStartPoint.transform.position,
                                MovePath.Points[j].ControlEndPoint.transform.position,
                                MovePath.Points[j].EndPoint.transform.position);
                    }

                    _linePathRenderer.positionCount = (((j * segment_count) + i));
                    _linePathRenderer.SetPosition((j * segment_count) + (i - 1), pixel);
                }
            }
        }
    }

    private Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;

        return p;
    }

    public Vector3 SmoothPoints(Vector3 p0, Vector3 p1,Vector3 p2, float smoothness)
    {
        var temp1 = Vector3.Lerp(p0, p1, smoothness);
        var temp2 = Vector3.Lerp(p1, p2, smoothness);

        return Vector3.Lerp(temp1, temp2, smoothness);
    }

}

[Serializable]
public class Path
{
    public List<BezierPoint> Points;
    public bool Loop;

}

[Serializable]
public class BezierPoint
{
    public GameObject StartPoint;
    public GameObject EndPoint;
    public GameObject ControlStartPoint;
    public GameObject ControlEndPoint;
}