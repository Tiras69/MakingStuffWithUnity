using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public enum TackleAttackPerception : int
{
    Attacking = 0,
    EndAttack = 1
}

public class TackleAttackState : AbstractStateDescription
{
    public float m_sightRadius;
    public float m_attackPower;
    public float m_endTime;

    private Vector3 m_directionAttack;
    private float m_initialSpeed;
    private float m_safetyEndTimer;
    private Material m_material;
    private NavMeshAgent m_agent;
    private Rigidbody m_rigidbody;
    private TackleAttackPerception m_currentState;
    private Color m_initialColor;

    private void Update()
    {
        m_rigidbody.velocity = m_directionAttack * m_attackPower;
        m_safetyEndTimer += Time.deltaTime;
        if ( m_safetyEndTimer >= m_endTime )
        {
            m_currentState = TackleAttackPerception.EndAttack;
        }
    }

    public override int GetPerceptionState()
    {
        return (int) m_currentState;
    }

    private void OnDisable()
    {
        m_material.color = m_initialColor;
        m_agent.speed = m_initialSpeed;
        m_agent.enabled = true;
        m_rigidbody.velocity = Vector3.zero;
    }

    private void OnEnable()
    {
        m_safetyEndTimer = 0.0f;
        m_material = GetComponent<Renderer>().material;
        m_initialColor = m_material.color;
        m_material.color = Color.yellow;
        m_rigidbody = GetComponent<Rigidbody>();
        m_agent = GetComponent<NavMeshAgent>();
        m_agent.enabled = false;
        m_initialSpeed = m_agent.speed;

        Collider[] colliders = Physics.OverlapSphere( transform.position, m_sightRadius);
        if ( colliders == null || colliders.Length == 0 )
        {
            m_currentState = TackleAttackPerception.EndAttack;
            m_directionAttack = Vector3.zero;
            return;
        }

        Collider playerCollider = colliders.FirstOrDefault( x => x.gameObject.tag == Constants.Tags.Player );
        if ( playerCollider == null )
        {
            m_currentState = TackleAttackPerception.EndAttack;
            m_directionAttack = Vector3.zero;
        }
        else
        { 
            m_directionAttack = playerCollider.transform.position - transform.position;
            m_directionAttack.y = 0.0f;
            m_directionAttack.Normalize();

            m_currentState = TackleAttackPerception.Attacking;
        }
    }
}
