using UnityEngine;

public class RoomScript : MonoBehaviour {

    public GameObject m_northWall;
    public GameObject m_southWall;
    public GameObject m_eastWall;
    public GameObject m_westWall;

    public GameObject m_northDooredWall;
    public GameObject m_southDooredWall;
    public GameObject m_eastDooredWall;
    public GameObject m_westDooredWall;

    public void InitializeRoom( RoomDTO _room)
    {
        if ( _room.NorthRoom == null)
        {
            m_northWall.SetActive( true );
            m_northDooredWall.SetActive( false );
        }
        else
        {
            m_northWall.SetActive( false );

            m_northDooredWall.SetActive( true );
        }

        if ( _room.SouthRoom == null )
        {
            m_southWall.SetActive( true );
            m_southDooredWall.SetActive( false );
        }
        else
        {
            m_southWall.SetActive( false );

            m_southDooredWall.SetActive( true );
        }

        if ( _room.EastRoom == null )
        {
            m_eastWall.SetActive( true );
            m_eastDooredWall.SetActive( false );
        }
        else
        {
            m_eastWall.SetActive( false );
            m_eastDooredWall.SetActive( true );
        }

        if ( _room.WestRoom == null )
        {
            m_westWall.SetActive( true );
            m_westDooredWall.SetActive( false );
        }
        else
        {
            m_westWall.SetActive( false );
            m_westDooredWall.SetActive( true );
        }
        this.transform.position = new Vector3( _room.xPos, 0.0f, _room.zPos ) * Constants.ProceduralsParameters.RoomSize;
    }

    void Start () {
		
	}
	
	void Update () {
		
	}
}
