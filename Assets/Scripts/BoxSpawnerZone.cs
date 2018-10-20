using UnityEngine;

[RequireComponent( typeof( BoxCollider ) )]
public class BoxSpawnerZone : MonoBehaviour, ISpawnerZone
{

    private BoxCollider m_boxCollider;

    // Use this for initialization
    void Start()
    {
        m_boxCollider = GetComponent<BoxCollider>();
    }

    public Vector3 GetNextRandomPosition()
    {
        float randomX = Random.Range(0.0f, m_boxCollider.size.x) - m_boxCollider.size.x / 2.0f;
        float randomZ = Random.Range(0.0f, m_boxCollider.size.z) - m_boxCollider.size.z / 2.0f;
        return ( this.transform.localRotation * new Vector3( randomX, 0.0f, randomZ ) ) + this.transform.position;
    }
}
