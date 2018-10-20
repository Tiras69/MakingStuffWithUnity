using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum LoadAttackPerception : int
{
    IsLoading = 0,
    EnemyToFar = 1,
    ReadyToLaunch = 2,
    Hurt = 3
}

public class LoadAttackState : IStateDescription
{
    public float SightRadius { get; set; }
    public float ChargeTime { get; set; }
    public GameObject GameObject { get; set; }

    private Material m_material;
    private float m_chargeTimer;
    private Color m_initialColor;
    private bool m_isHurt;
    private NavMeshAgent m_agent;

    public void ExecuteBehaviour()
    {
        m_agent.SetDestination( GameObject.transform.position );
        m_material.color = (m_chargeTimer % 0.5f) > 0.25 ? Color.white : Color.red;
        m_chargeTimer += Time.deltaTime;
    }

    public int GetPerceptionState()
    {
        if( m_isHurt)
        {
            return (int) LoadAttackPerception.Hurt;
        }

        Collider[] colliders = Physics.OverlapSphere( GameObject.transform.position, SightRadius);
        if( colliders == null || colliders.Length == 0)
        {
            return (int) LoadAttackPerception.EnemyToFar;
        }

        if (m_chargeTimer <= ChargeTime)
        {
            return (int) LoadAttackPerception.IsLoading;
        }
        else
        {
            return (int) LoadAttackPerception.ReadyToLaunch;
        }
    }

    public void OnStateLeave()
    {
        m_material.color = m_initialColor;
    }

    public void OnStateStart()
    {
        m_isHurt = false;
        m_chargeTimer = 0.0f;
        m_material = GameObject.GetComponent<Renderer>().material;
        m_initialColor = m_material.color;
        m_agent = GameObject.GetComponent<NavMeshAgent>();
    }

    public void OnCollision( Collision _collision )
    {
        if( _collision.gameObject.tag == Constants.Tags.Bullet)
        {
            m_isHurt = true;
        }
    }
}
