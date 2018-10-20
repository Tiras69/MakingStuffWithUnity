using UnityEngine;

public abstract class AbstractStateMachine : MonoBehaviour
{

    private Node<IStateDescription> m_currentNode;
    protected Graph<IStateDescription> m_stateMachine;

    protected abstract void DefineStateMachine();

    private Collision m_currentCollision;

    void Start()
    {
        m_currentCollision = null;
        m_stateMachine = new Graph<IStateDescription>();
        DefineStateMachine();
        m_currentNode = m_stateMachine.GetFirstNode();
        m_currentNode.Data.OnStateStart();
    }
    
    void Update()
    {
        m_currentNode.Data.ExecuteBehaviour();
        if( m_currentCollision != null)
        {
            m_currentNode.Data.OnCollision( m_currentCollision );
            m_currentCollision = null;
        }
        Node<IStateDescription> nextNode = m_currentNode.GetNextState( m_currentNode.Data.GetPerceptionState() );
        if ( nextNode != null )
        {
            if ( nextNode != m_currentNode )
            {
                m_currentNode.Data.OnStateLeave();
                m_currentNode = nextNode;
                m_currentNode.Data.OnStateStart();
            }
        }
        else
        {
            Debug.Log( "Warning impossible to access the next state" );
        }
    }

    private void OnCollisionEnter( Collision collision )
    {
        m_currentCollision = collision;
    }
}
