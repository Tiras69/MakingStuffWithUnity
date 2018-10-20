using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    public float m_height;
    public GameObject m_targetGameObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if( m_targetGameObject != null)
        {
            Vector3 finalPosition = m_targetGameObject.transform.position;
            finalPosition.y = m_height;
            this.transform.position = finalPosition;
        }
	}
}
