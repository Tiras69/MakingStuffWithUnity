using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{

    public int numberMaxOfEnemies = Constants.AllocatorParameters.InstanceCountMaximum;
    public List<GameObject> m_agentPrefabs;
    [Header("GameObjects per seconds")]
    public float m_spawnRate = 1.0f;

    private float m_spawnTime;
    private float m_spawnerTimer;
    private List<ISpawnerZone> m_spawnerZones;
    private Dictionary<GameObject, GameObjectPool> m_gameObjectPools;

    // Use this for initialization
    void Start()
    {
        m_spawnTime = m_spawnRate == 0.0f ? 0.0f : 1.0f / m_spawnRate;
        m_spawnerTimer = 0.0f;
        m_spawnerZones = GetComponentsInChildren<ISpawnerZone>().ToList();

        m_gameObjectPools = new Dictionary<GameObject, GameObjectPool>();
        foreach( GameObject agent in m_agentPrefabs)
        {
            GameObjectPool pool = this.gameObject.AddComponent<GameObjectPool>();
            pool.m_initializationWithEditor = false;
            pool.InitializeObjectPool( agent.gameObject, numberMaxOfEnemies );
            m_gameObjectPools.Add( agent.gameObject, pool );
        }

    }

    // Update is called once per frame
    void Update()
    {
        if ( m_spawnerZones == null || m_spawnerZones.Count == 0 )
        { return; }

        float numberOfSpawnDuringThisFrameRaw = (Time.deltaTime + m_spawnerTimer) / m_spawnTime;
        int numberOfSpawnDuringThisFrame = (int)(numberOfSpawnDuringThisFrameRaw);
        for(int i = 0; i < numberOfSpawnDuringThisFrame; i++ )
        {
            InstanciateAgent();
        }

        m_spawnerTimer = numberOfSpawnDuringThisFrameRaw - numberOfSpawnDuringThisFrame;
    }

    private bool InstanciateAgent()
    {
        int randObjectIndex = Random.Range(0, m_agentPrefabs.Count);
        int randZoneIndex = Random.Range(0, m_spawnerZones.Count);
        GameObject prefab = m_gameObjectPools[ m_agentPrefabs[ randObjectIndex ].gameObject ].GetNextAvaibleInstance();
        if ( prefab != null )
        {
            prefab.transform.position = m_spawnerZones [ randZoneIndex ].GetNextRandomPosition();
            return true;
        }
        return false;
    }
}
