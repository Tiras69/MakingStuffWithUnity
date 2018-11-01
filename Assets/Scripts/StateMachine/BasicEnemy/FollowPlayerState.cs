using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public enum FollowPlayerPerception : int
{
    ChasePlayer = 0,
    LostPlayer = 1,
    PlayerIsCloseEnough = 2,
    Hurt = 3
}

public class FollowPlayerState : AbstractStateDescription
{

    public float m_attackRadius;
    public float m_sightRadius;

    private NavMeshAgent m_agent;
    private FollowPlayerPerception m_currentState;

    private void Update()
    {
        if( m_currentState == FollowPlayerPerception.Hurt)
        { return; }

        Collider[] colliders = Physics.OverlapSphere( transform.position, m_sightRadius);
        if ( colliders == null || colliders.Length == 0 )
        {
            m_currentState = FollowPlayerPerception.LostPlayer;
            m_agent.destination = transform.position;
            return;
        }


        Collider playerCollider = colliders.Where(x => x.tag == Constants.Tags.Player).FirstOrDefault();
        if ( playerCollider == null )
        {
            m_currentState = FollowPlayerPerception.LostPlayer;
            m_agent.destination = transform.position;
            return;
        }
        else
        {
            if ( Vector3.Distance( transform.position, playerCollider.transform.position ) <= m_attackRadius )
            {
                m_currentState = FollowPlayerPerception.PlayerIsCloseEnough;
            }
            else
            {
                m_currentState = FollowPlayerPerception.ChasePlayer;
            }
        }

        m_agent.destination = playerCollider.transform.position;
    }

    public override int GetPerceptionState()
    {
        return (int) m_currentState;
    }

    private void OnEnable()
    {
        m_currentState = FollowPlayerPerception.ChasePlayer;
        m_agent = GetComponent<NavMeshAgent>();
        if ( m_agent == null )
        {
            Debug.Log( "NavMash Agent Component doesn't exists" );
        }
    }

    private void OnCollisionEnter( Collision _collision )
    {
        if(_collision.gameObject.tag == Constants.Tags.Bullet)
        {
            m_currentState = FollowPlayerPerception.Hurt;
        }
    }
}
