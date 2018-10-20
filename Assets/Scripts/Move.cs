using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Move : MonoBehaviour {

    public float m_speed;
    private Rigidbody m_rigidbody;

	// Use this for initialization
	void Start () {
        m_rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 directionVector = Vector3.zero;
		if(Input.GetKey(KeyCode.Z))
        {
            directionVector += Vector3.forward;
        }
        if ( Input.GetKey( KeyCode.S ) )
        {
            directionVector -= Vector3.forward;
        }
        if ( Input.GetKey( KeyCode.D ) )
        {
            directionVector += Vector3.right;
        }
        if ( Input.GetKey( KeyCode.Q ) )
        {
            directionVector -= Vector3.right;
        }

        directionVector = ( directionVector.normalized * m_speed ) - Vector3.up * m_rigidbody.velocity.y;
        m_rigidbody.velocity = directionVector;

    }

}
