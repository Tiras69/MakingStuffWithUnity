using UnityEngine;
using UnityEngine.AI;

public class DungeonScript : MonoBehaviour
{
    public GameObject m_roomPrefab;
    public int numberOfRooms = 10;
    
    private DungeonGraph m_dungeon;
    private NavMeshSurface m_navMeshSurface;

    // Use this for initialization
    void Start()
    {
        GenerateDungeon();
        m_navMeshSurface = GetComponent<NavMeshSurface>();
        if ( m_navMeshSurface == null )
        {
            Debug.LogWarning( "Invalid room prefab missing the NavMeshSurface component" );
            return;
        }
        m_navMeshSurface.BuildNavMesh();
    }
    

    private void GenerateDungeon()
    {
        m_dungeon = new DungeonGraph();
        for(int i=0; i < numberOfRooms; i++ )
        {
            m_dungeon.AddRoom();
        }
        foreach(RoomDTO room in m_dungeon.m_rooms)
        {
            GameObject newRoom = GameObject.Instantiate(m_roomPrefab, this.transform);
            RoomScript roomScript = newRoom.GetComponent<RoomScript>();
            if ( roomScript == null )
            {
                Debug.LogWarning( "Invalid room prefab missing the Roomscript component" );
                return;
            }
            // NavMeshSurface navMeshSurface = newRoom.GetComponent<NavMeshSurface>();
            // if(navMeshSurface == null)
            // {
            //     Debug.LogWarning( "Invalid room prefab missing the NavMeshSurface component" );
            //     return;
            // }
            roomScript.InitializeRoom( room );
            // navMeshSurface.BuildNavMesh();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
