using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( Rigidbody ) )]
public class FireBall : AbstractTimableGameObject
{
    public float m_speed;   
    private Rigidbody m_rigidbody;
    
    // Use this for initialization
    void Start()
    {
        m_rigidbody = this.GetComponent<Rigidbody>();
    }

    public override void LifeUpdate()
    {
        m_rigidbody.velocity = this.transform.forward * m_speed;
    }

    private void OnCollisionEnter( Collision collision )
    {
        gameObject.SetActive( false );
    }

}
