using UnityEngine;
using UnityEngine.AI;

public enum HurtPerception : int
{
    Stuned = 0,
    Recover = 1
}

public class HurtState : AbstractStateDescription
{
    public float m_stunTime;

    private float m_stunTimer;
    private Color m_initialColor;
    private Material m_material;

    private void Update()
    {
        m_stunTimer += Time.deltaTime;
    }

    public override int GetPerceptionState()
    {
        if( m_stunTimer >= m_stunTime)
        {
            return (int) HurtPerception.Recover;
        }
        else
        {
            return (int) HurtPerception.Stuned;
        }
    }
    
    private void OnDisable()
    {
        m_material.color = m_initialColor;
    }

    private void OnEnable()
    {
        m_stunTimer = 0.0f;
        m_material = GetComponent<Renderer>().material;
        m_initialColor = m_material.color;
        m_material.color = Color.yellow;
        GetComponent<NavMeshAgent>().SetDestination( transform.position );
    }
}
