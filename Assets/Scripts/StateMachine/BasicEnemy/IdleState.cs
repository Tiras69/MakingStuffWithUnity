using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public enum IdlePerception : int
{
    PlayerInSight = 0,
    Nothing = 1,
    Hurt = 2
}

public class IdleState : AbstractStateDescription
{
    public float m_sightRadius;

    private NavMeshAgent m_agent;
    private bool m_isHurtForNextFrame;

    public void Update()
    {
        m_agent.SetDestination( transform.position );
    }

    public override int GetPerceptionState()
    {
        if(m_isHurtForNextFrame)
        {
            return (int) IdlePerception.Hurt;
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, m_sightRadius);
        if(colliders.Where(x => x.tag == Constants.Tags.Player).Any())
        {
            return (int) IdlePerception.PlayerInSight;
        }
        else
        {
            return (int) IdlePerception.Nothing;
        }
    }

    public void OnCollisionEnter( Collision _collision )
    {
        if( _collision.gameObject.tag == Constants.Tags.Bullet)
        {
            m_isHurtForNextFrame = true;
        }
    }

    public void OnEnable()
    {
        m_isHurtForNextFrame = false;
        m_agent = GetComponent<NavMeshAgent>();
    }
}
