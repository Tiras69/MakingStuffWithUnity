using UnityEngine;

[RequireComponent(typeof(GameObjectPool))]
public class Shoot : MonoBehaviour
{
    public Camera m_cameraReference;
    private GameObjectPool m_fireBallPool;

    // Use this for initialization
    void Start()
    {
        if( m_cameraReference == null)
        {
            Debug.Log( "Missing camera component in Shoot script" );
            return;
        }

        m_fireBallPool = GetComponent<GameObjectPool>();
        if( m_fireBallPool == null)
        {
            Debug.Log( "Missing fire ball pool in Shoot Script" );
        }

    }

    // Update is called once per frame
    void Update()
    {
        if( m_cameraReference == null || m_fireBallPool == null)
        { return; }

        if ( Input.GetMouseButtonDown( 0 ) )
        {
            Vector3 directionVector = Input.mousePosition - m_cameraReference.WorldToScreenPoint( this.transform.position );
            directionVector = new Vector3( directionVector.x, 0.0f, directionVector.y );
            directionVector.Normalize();

            GameObject fireBall = m_fireBallPool.GetNextAvaibleInstance();
            if ( fireBall != null )
            {
                fireBall.transform.position = this.transform.position + directionVector * 2.0f;
                fireBall.transform.rotation = Quaternion.AngleAxis( Mathf.Atan2( directionVector.x, directionVector.z ) * Mathf.Rad2Deg, Vector3.up );
            }
        }
    }
}
