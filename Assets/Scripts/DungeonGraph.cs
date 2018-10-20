using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGraph
{
    private List<RoomDTO> m_roomBoundaries;
    public List<RoomDTO> m_rooms;

    public DungeonGraph()
    {
        m_roomBoundaries = new List<RoomDTO>();
        m_rooms = new List<RoomDTO>();
    }

    public void AddRoom()
    {
        // First Room.
        RoomDTO newRoom = new RoomDTO();
        if ( m_roomBoundaries.Count == 0 )
        {
            newRoom.xPos = 0;
            newRoom.zPos = 0;
        }
        else
        {
            int boundaryRoomIndex = Random.Range( 0, m_roomBoundaries.Count);
            RoomDTO boundaryRoom = m_roomBoundaries[boundaryRoomIndex];
            if ( boundaryRoom.SetRandomEmptyRoom( newRoom ) )
            {

                m_roomBoundaries.Remove( boundaryRoom );
            }
        }
        m_roomBoundaries.Add( newRoom );
        m_rooms.Add( newRoom );
        CheckBoundaryRooms();
    }

    private void CheckBoundaryRooms()
    {
        List<RoomDTO> toDeleteRooms = new List<RoomDTO>();
        foreach ( RoomDTO currentRoom in m_roomBoundaries )
        {
            RoomDTO eastRoom = m_rooms.Find( x => x.xPos == currentRoom.xPos + 1 && x.zPos == currentRoom.zPos );
            if ( eastRoom != null )
            {
                eastRoom.WestPath = true;
                currentRoom.EastPath = true;
            }

            RoomDTO westRoom = m_rooms.Find( x => x.xPos == currentRoom.xPos - 1 && x.zPos == currentRoom.zPos );
            if ( westRoom != null )
            {
                westRoom.EastPath = true;
                currentRoom.WestPath = true;
            }

            RoomDTO northRoom = m_rooms.Find( x => x.xPos == currentRoom.xPos && x.zPos == currentRoom.zPos + 1 );
            if ( northRoom != null )
            {
                northRoom.SouthPath = true;
                currentRoom.NorthPath = true;
            }

            RoomDTO southRoom = m_rooms.Find( x => x.xPos == currentRoom.xPos && x.zPos == currentRoom.zPos - 1 );
            if ( southRoom != null )
            {
                southRoom.NorthPath = true;
                currentRoom.SouthPath = true;
            }
            if ( currentRoom.IsRoomFullyConnected() )
            {
                toDeleteRooms.Add( currentRoom );
            }
        }
        foreach(RoomDTO room in toDeleteRooms)
        {
            m_roomBoundaries.Remove( room );
        }
    }
}
