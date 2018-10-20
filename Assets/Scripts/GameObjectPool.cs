using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{

    public bool m_initializationWithEditor = false;
    public GameObject m_gameObjectPrefab;
    public int m_numberOfInstances = Constants.AllocatorParameters.InstanceCountMaximum;

    private List<GameObject> m_gameObjectPool;


    void Start()
    {
        if ( m_initializationWithEditor )
        {
            InitializeObjectPool( m_gameObjectPrefab, m_numberOfInstances );
        }
    }

    public GameObjectPool InitializeObjectPool( GameObject _gameObjectPrefab, int _numberOfInstances )
    {
        if ( _gameObjectPrefab == null )
        { return null; }

        m_gameObjectPool = new List<GameObject>();
        m_numberOfInstances = _numberOfInstances;
        for ( int i = 0; i < _numberOfInstances; i++ )
        {
            GameObject newGameObject = GameObject.Instantiate(_gameObjectPrefab, this.transform);
            newGameObject.SetActive( false );
            m_gameObjectPool.Add( newGameObject );
        }
        return this;
    }

    public GameObject GetNextAvaibleInstance()
    {
        for ( int i = 0; i < m_numberOfInstances; i++ )
        {
            if ( !m_gameObjectPool [ i ].activeInHierarchy )
            {
                m_gameObjectPool [ i ].SetActive( true );
                return m_gameObjectPool [ i ];
            }

        }
        return null;
    }
}
