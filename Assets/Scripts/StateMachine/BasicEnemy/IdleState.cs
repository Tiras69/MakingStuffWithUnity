using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public enum IdlePerception : int
{
    PlayerInSight = 0,
    Nothing = 1,
    Hurt = 2
}

public class IdleState : IStateDescription {

    public float SightRadius {get; set;}
    public GameObject GameObject { get; set; }

    private NavMeshAgent m_agent;
    private bool m_isHurtForNextFrame;

    public void ExecuteBehaviour()
    {
        m_agent.SetDestination( GameObject.transform.position );
    }

    public int GetPerceptionState()
    {
        if(m_isHurtForNextFrame)
        {
            return (int) IdlePerception.Hurt;
        }

        Collider[] colliders = Physics.OverlapSphere(GameObject.transform.position, SightRadius);
        if(colliders.Where(x => x.tag == Constants.Tags.Player).Any())
        {
            return (int) IdlePerception.PlayerInSight;
        }
        else
        {
            return (int) IdlePerception.Nothing;
        }
    }

    public void OnCollision( Collision _collision )
    {
        if( _collision.gameObject.tag == Constants.Tags.Bullet)
        {
            m_isHurtForNextFrame = true;
        }
    }

    public void OnStateLeave()
    {

    }

    public void OnStateStart()
    {
        m_isHurtForNextFrame = false;
        m_agent = GameObject.GetComponent<NavMeshAgent>();
    }
}
