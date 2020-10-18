using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject cloudSphere;
    public int numSpheresMin = 6;
    public int numSpheresMax = 10;
    public Vector3 sphereOffsetScale = new Vector3(5, 2, 1);
    public Vector2 sphereScaleRangeX = new Vector2(4, 8);
    public Vector2 sphereScaleRangeY = new Vector2(3, 4);
    public Vector2 sphereScaleRangeZ = new Vector2(2, 4);
    public float scaleYMin = 2f;

    private List<GameObject> m_Spheres;

    // Start is called before the first frame update
    void Start()
    {
        m_Spheres = new List<GameObject>();

        int num = Random.Range(numSpheresMin, numSpheresMax);
        for (int i = 0; i < num; i++)
        {
            // Instantiation
            GameObject sp = Instantiate<GameObject>(cloudSphere);
            m_Spheres.Add(sp);
            Transform spTrans = sp.transform;
            spTrans.SetParent(this.transform);

            // Randomizing pos
            Vector3 offset = Random.insideUnitSphere;
            offset.x *= sphereOffsetScale.x;
            offset.y *= sphereOffsetScale.y;
            offset.z *= sphereOffsetScale.z;
            spTrans.localPosition = offset;

            // Randomizing scale
            Vector3 scale = Vector3.one;
            scale.x = Random.Range(sphereScaleRangeX.x, sphereScaleRangeX.y);
            scale.y = Random.Range(sphereScaleRangeY.x, sphereScaleRangeY.y);
            scale.z = Random.Range(sphereScaleRangeZ.x, sphereScaleRangeZ.y);

            scale.y *= 1 - (Mathf.Abs(offset.x) / sphereOffsetScale.x);
            scale.y = Mathf.Max(scale.y, scaleYMin);

            spTrans.localScale = scale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Test code for generating many clouds over and over
        //if (Input.GetKeyDown(KeyCode.Space))
        //    Restart();
    }

    void Restart()
    {
        foreach (var sp in m_Spheres)
            Destroy(sp);

        Start();
    }
}
