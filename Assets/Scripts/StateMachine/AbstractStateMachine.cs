using UnityEngine;

public abstract class AbstractStateMachine : MonoBehaviour
{

    private Node<AbstractStateDescription> m_currentNode;
    protected Graph<AbstractStateDescription> m_stateMachine;

    protected abstract void DefineStateMachine();
    
    void Start()
    {
        m_stateMachine = new Graph<AbstractStateDescription>();
        DefineStateMachine();
        m_currentNode = m_stateMachine.GetFirstNode();
        m_currentNode.Data.enabled = true;
    }
    
    void LateUpdate()
    { 
        // Physics and collisions are now handle in the differents components.

        // We consider now that the current node execute Update in the frame because the
        // component is enabled.

        Node<AbstractStateDescription> nextNode = m_currentNode.GetNextState( m_currentNode.Data.GetPerceptionState() );
        if ( nextNode != null )
        {
            if ( nextNode != m_currentNode )
            {
                m_currentNode.Data.enabled = false;
                m_currentNode = nextNode;
                m_currentNode.Data.enabled = true;
            }
        }
        else
        {
            Debug.Log( "Warning impossible to access the next state" );
        }
    }
}
