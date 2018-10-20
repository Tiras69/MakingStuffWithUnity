using System.Collections.Generic;
using UnityEngine;

public class RoomDTO {
    
    public bool NorthPath { get; set; }
    public bool SouthPath { get; set; }
    public bool EastPath { get; set; }
    public bool WestPath { get; set; }

    public RoomDTO NorthRoom { get; set; }
    public RoomDTO SouthRoom { get; set; }
    public RoomDTO EastRoom { get; set; }
    public RoomDTO WestRoom { get; set; }

    public int xPos { get; set; }
    public int zPos { get; set; }

    public RoomDTO()
    {
        NorthPath = false;
        SouthPath = false;
        EastPath = false;
        WestPath = false;

        NorthRoom = null;
        SouthRoom = null;
        EastRoom = null;
        WestRoom = null;
    }

    
    public bool SetRandomEmptyRoom(RoomDTO _room)
    {
        List<int> indexOfEmptyRooms = new List<int>();
        if( !NorthPath)
        {
            indexOfEmptyRooms.Add( 0 );
        }
        if ( !SouthPath )
        {
            indexOfEmptyRooms.Add( 1 );
        }
        if ( !EastPath )
        {
            indexOfEmptyRooms.Add( 2 );
        }
        if ( !WestPath )
        {
            indexOfEmptyRooms.Add( 3 );
        }

        int roomIndex = indexOfEmptyRooms[ Random.Range(0, indexOfEmptyRooms.Count) ];
        switch (roomIndex)
        {
            case 0: // North
                NorthRoom = _room;
                NorthPath = true;
                _room.SouthRoom = this;
                _room.SouthPath = true;
                _room.zPos = zPos + 1;
                _room.xPos = xPos;
                break;
            case 1: // South
                SouthRoom = _room;
                SouthPath = true;
                _room.NorthRoom = this;
                _room.NorthPath = true;
                _room.zPos = zPos - 1;
                _room.xPos = xPos;
                break;
            case 2: // East
                EastRoom = _room;
                EastPath = true;
                _room.WestRoom = this;
                _room.WestPath = true;
                _room.zPos = zPos;
                _room.xPos = xPos + 1;
                break;
            case 3: // West
                WestRoom = _room;
                _room.EastRoom = this;
                _room.zPos = zPos;
                _room.xPos = xPos - 1;
                break;
        }

        return IsRoomFullyConnected();
    }

    public bool IsRoomFullyConnected()
    {
        return NorthPath && SouthPath && EastPath && WestPath;
    }

}
