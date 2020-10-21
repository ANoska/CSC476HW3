using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject windowShard;
    public int numShards = 100;

    private List<GameObject> m_activeShards;
    private bool m_IsShattering;

    // Start is called before the first frame update
    void Start()
    {
        m_activeShards = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsShattering)
            this.GetComponent<Rigidbody>().Sleep();
    }

    void OnCollisionEnter(Collision other)
    {
        // Return if we collided with something other than a projectile
        if (other.gameObject.tag != "Projectile")
            return;

        // Return if the collision velocity is too small
        if (other.relativeVelocity.x < 3.0f && other.relativeVelocity.y < 3.0f)
            return;

        m_IsShattering = true;
        this.GetComponent<Rigidbody>().Sleep();

        Shatter();
    }

    void Shatter()
    {
        for (int i = 0; i <= numShards; i++)
        {
            var ws = Instantiate(windowShard);
            ws.transform.position = this.transform.position;
            m_activeShards.Add(ws);
        }

        Invoke("DisposeShards", 5f);
        this.gameObject.SetActive(false);
    }

    void DisposeShards()
    {
        foreach (GameObject shard in m_activeShards)
            Destroy(shard);

        Destroy(this.gameObject);
    }
}
