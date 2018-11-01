using UnityEngine;
using UnityEngine.AI;

public enum LoadAttackPerception : int
{
    IsLoading = 0,
    EnemyToFar = 1,
    ReadyToLaunch = 2,
    Hurt = 3
}

public class LoadAttackState : AbstractStateDescription
{
    public float m_sightRadius;
    public float m_chargeTime;

    private Material m_material;
    private float m_chargeTimer;
    private Color m_initialColor;
    private bool m_isHurt;
    private NavMeshAgent m_agent;

    private void Update()
    {
        m_agent.SetDestination( transform.position );
        m_material.color = (m_chargeTimer % 0.5f) > 0.25 ? Color.white : Color.red;
        m_chargeTimer += Time.deltaTime;
    }

    public override int GetPerceptionState()
    {
        if( m_isHurt)
        {
            return (int) LoadAttackPerception.Hurt;
        }

        Collider[] colliders = Physics.OverlapSphere( transform.position, m_sightRadius);
        if( colliders == null || colliders.Length == 0)
        {
            return (int) LoadAttackPerception.EnemyToFar;
        }

        if (m_chargeTimer <= m_chargeTime)
        {
            return (int) LoadAttackPerception.IsLoading;
        }
        else
        {
            return (int) LoadAttackPerception.ReadyToLaunch;
        }
    }

    private void OnDisable()
    {
        m_material.color = m_initialColor;
    }

    private void OnEnable()
    {
        m_isHurt = false;
        m_chargeTimer = 0.0f;
        m_material = GetComponent<Renderer>().material;
        m_initialColor = m_material.color;
        m_agent = GetComponent<NavMeshAgent>();
    }

    private void OnCollisionEnter( Collision _collision )
    {
        if( _collision.gameObject.tag == Constants.Tags.Bullet)
        {
            m_isHurt = true;
        }
    }
}
