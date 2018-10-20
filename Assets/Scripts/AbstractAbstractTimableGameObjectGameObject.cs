using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTimableGameObject : MonoBehaviour {

    public float m_timeToDie;
    private float m_timerToDie = 0.0f;

    public abstract void LifeUpdate();

    private void Update()
    {
        if ( m_timerToDie < m_timeToDie )
        {
            LifeUpdate();
            m_timerToDie += Time.deltaTime;
        }
        else
        {
            this.gameObject.SetActive( false );
            m_timerToDie = 0.0f;
        }
    }

}
