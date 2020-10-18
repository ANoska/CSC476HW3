using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : MonoBehaviour
{
    public static ProjectileLine S;

    [Header("Set in Inspector")]
    public float minDist = 0.1f;
    public bool ____;

    [Header("Set Dynamically")]
    public LineRenderer line;
    public List<Vector3> points;

    private GameObject m_Poi;

    public GameObject poi
    {
        get { return m_Poi; }
        set
        {
            m_Poi = value;
            if (m_Poi != null)
            {
                line.enabled = false;
                points = new List<Vector3>();
                AddPoint();
            }
        }
    }

    // Returns the location of the most recently added point
    public Vector3 lastPoint
    {
        get
        {
            if (points == null)
                return Vector3.zero;

            return points[points.Count - 1];
        }
    }

    void Awake()
    {
        S = this;

        line = this.GetComponent<LineRenderer>();
        line.enabled = false;

        points = new List<Vector3>();
    }

    void FixedUpdate()
    {
        if (poi == null)
        {
            if (FollowCam.POI != null)
            {
                if (FollowCam.POI.tag == "Projectile")
                {
                    poi = FollowCam.POI;
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }

        AddPoint();
        if (poi.GetComponent<Rigidbody>().IsSleeping())
            poi = null;
    }

    // Clear line directly
    public void Clear()
    {
        m_Poi = null;
        line.enabled = false;
        points = new List<Vector3>();
    }

    public void AddPoint()
    {
        Vector3 pt = m_Poi.transform.position;
        if (points.Count > 0 && (pt - lastPoint).magnitude < minDist)
            return;

        if (points.Count == 0)
        {
            Vector3 launchPosDiff = pt - Slingshot.LAUNCH_POS;

            points.Add(pt + launchPosDiff);
            points.Add(pt);
            line.positionCount = 2;

            line.SetPosition(0, points[0]);
            line.SetPosition(1, points[1]);

            line.enabled = true;
        }
        else
        {
            points.Add(pt);
            line.positionCount = points.Count;
            line.SetPosition(points.Count - 1, lastPoint);
            line.enabled = true;
        }
    }
}
