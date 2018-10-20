using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public enum TackleAttackPerception : int
{
    Attacking = 0,
    EndAttack = 1
}

public class TackleAttackState : IStateDescription
{

    public float SightRadius { get; set; }
    public float AttackPower { get; set; }
    public float EndTime { get; set; }
    public GameObject GameObject { get; set; }

    private Vector3 m_directionAttack;
    private float m_initialSpeed;
    private float m_safetyEndTimer;
    private Material m_material;
    private NavMeshAgent m_agent;
    private Rigidbody m_rigidbody;
    private TackleAttackPerception m_currentState;
    private Color m_initialColor;

    public void ExecuteBehaviour()
    {
        m_rigidbody.velocity = m_directionAttack * AttackPower;
        m_safetyEndTimer += Time.deltaTime;
        if ( m_safetyEndTimer >= EndTime )
        {
            m_currentState = TackleAttackPerception.EndAttack;
        }
    }

    public int GetPerceptionState()
    {
        return (int) m_currentState;
    }

    public void OnStateLeave()
    {
        m_material.color = m_initialColor;
        m_agent.speed = m_initialSpeed;
        m_agent.enabled = true;
        m_rigidbody.velocity = Vector3.zero;
    }

    public void OnStateStart()
    {
        m_safetyEndTimer = 0.0f;
        m_material = GameObject.GetComponent<Renderer>().material;
        m_initialColor = m_material.color;
        m_material.color = Color.yellow;
        m_rigidbody = GameObject.GetComponent<Rigidbody>();
        m_agent = GameObject.GetComponent<NavMeshAgent>();
        m_agent.enabled = false;
        m_initialSpeed = m_agent.speed;

        Collider[] colliders = Physics.OverlapSphere( GameObject.transform.position, SightRadius);
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
            m_directionAttack = playerCollider.transform.position - GameObject.transform.position;
            m_directionAttack.y = 0.0f;
            m_directionAttack.Normalize();

            m_currentState = TackleAttackPerception.Attacking;
        }
    }

    public void OnCollision( Collision _collision )
    {

    }
}
