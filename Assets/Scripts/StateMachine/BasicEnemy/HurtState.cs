using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum HurtPerception : int
{
    Stuned = 0,
    Recover = 1
}

public class HurtState : IStateDescription {

    public GameObject GameObject { get; set; }
    public float StunTime { get; set; }

    private float m_stunTimer;
    private Color m_initialColor;
    private Material m_material;

    public void ExecuteBehaviour()
    {
        m_stunTimer += Time.deltaTime;
    }

    public int GetPerceptionState()
    {
        if( m_stunTimer >= StunTime)
        {
            return (int) HurtPerception.Recover;
        }
        else
        {
            return (int) HurtPerception.Stuned;
        }
    }

    public void OnCollision( Collision _collision )
    {

    }

    public void OnStateLeave()
    {
        m_material.color = m_initialColor;
    }

    public void OnStateStart()
    {
        m_stunTimer = 0.0f;
        m_material = GameObject.GetComponent<Renderer>().material;
        m_initialColor = m_material.color;
        m_material.color = Color.yellow;
        GameObject.GetComponent<NavMeshAgent>().SetDestination( GameObject.transform.position );
    }
}
