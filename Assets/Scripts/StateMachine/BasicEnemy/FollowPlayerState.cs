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

public class FollowPlayerState : IStateDescription
{

    public float AttackRadius { get; set; }
    public float SightRadius { get; set; }
    public GameObject GameObject { get; set; }

    private NavMeshAgent m_agent;
    private FollowPlayerPerception m_currentState;

    public void ExecuteBehaviour()
    {
        Collider[] colliders = Physics.OverlapSphere( GameObject.transform.position, SightRadius);
        if ( colliders == null || colliders.Length == 0 )
        {
            m_currentState = FollowPlayerPerception.LostPlayer;
            m_agent.destination = GameObject.transform.position;
            return;
        }


        Collider playerCollider = colliders.Where(x => x.tag == Constants.Tags.Player).FirstOrDefault();
        if ( playerCollider == null )
        {
            m_currentState = FollowPlayerPerception.LostPlayer;
            m_agent.destination = GameObject.transform.position;
            return;
        }
        else
        {
            if ( Vector3.Distance( GameObject.transform.position, playerCollider.transform.position ) <= AttackRadius )
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

    public int GetPerceptionState()
    {
        return (int) m_currentState;
    }

    public void OnStateLeave()
    {
    }

    public void OnStateStart()
    {
        m_agent = GameObject.GetComponent<NavMeshAgent>();
        if ( m_agent == null )
        {
            Debug.Log( "NavMash Agent Component doesn't exists" );
        }
    }

    public void OnCollision( Collision _collision )
    {
        if(_collision.gameObject.tag == Constants.Tags.Bullet)
        {
            m_currentState = FollowPlayerPerception.Hurt;
        }
    }
}
