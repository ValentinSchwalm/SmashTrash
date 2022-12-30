using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    private Vector3 originPoint;
    private Vector3 bezierPoint1;
    private Vector3 bezierPoint2;
    private float timer;

    public void SetTrashCurve(Vector3 origin, Vector3 bezier1, Vector3 bezier2)
    {
        this.originPoint = origin;
        this.bezierPoint1 = bezier1;
        this.bezierPoint2 = bezier2;
    }

    public Vector3 Origin
    {
        get { return this.originPoint; }
    }

    public Vector3 Bezier1
    {
        get { return this.bezierPoint1; }
    }
    public Vector3 Bezier2
    {
        get { return this.bezierPoint2; }
    }

    public float Timer
    {
        get { return this.timer; }
        set { this.timer = value; }
    }
}
