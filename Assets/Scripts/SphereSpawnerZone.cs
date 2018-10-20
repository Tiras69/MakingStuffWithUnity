using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( SphereCollider ) )]
public class SphereSpawnerZone : MonoBehaviour, ISpawnerZone
{
    private SphereCollider m_sphereCollider;
    
    // Use this for initialization
    void Start()
    {
        m_sphereCollider = GetComponent<SphereCollider>();
    }

    public Vector3 GetNextRandomPosition()
    {
        float randomRadius = Random.Range( -m_sphereCollider.radius, m_sphereCollider.radius);
        float randomAngle = Random.Range( 0.0f, 2.0f * Mathf.PI);
        return ( new Vector3( Mathf.Cos( randomAngle ), 0.0f, Mathf.Sin( randomAngle ) ) * randomRadius ) + this.transform.position;
    }
}
