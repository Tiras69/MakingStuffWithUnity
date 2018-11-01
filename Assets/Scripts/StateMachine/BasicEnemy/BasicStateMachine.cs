using UnityEngine;

[RequireComponent( typeof( IdleState ) )]
[RequireComponent( typeof( FollowPlayerState ) )]
[RequireComponent( typeof( LoadAttackState ) )]
[RequireComponent( typeof( TackleAttackState ) )]
[RequireComponent( typeof( HurtState ) )]
public class BasicStateMachine : AbstractStateMachine
{

    // public float m_attackPower;
    // public float m_attackTime;
    // 
    // public float m_stunTime;
    // public float m_sightRadius;
    // public float m_attackRadius;
    // public float m_loadAttackTime;

    protected override void DefineStateMachine()
    {
        // States
        Node<AbstractStateDescription> idleState = new Node<AbstractStateDescription>( GetComponent<IdleState>() );
        
        m_stateMachine.AddNode( idleState );

        Node<AbstractStateDescription> followPlayerState = new Node<AbstractStateDescription>( GetComponent<FollowPlayerState>() );
        m_stateMachine.AddNode( followPlayerState );

        Node<AbstractStateDescription> loadAttackState = new Node<AbstractStateDescription>( GetComponent<LoadAttackState>() );
        m_stateMachine.AddNode( loadAttackState );

        Node<AbstractStateDescription> tackleAttackState = new Node<AbstractStateDescription>( GetComponent<TackleAttackState>() );
        m_stateMachine.AddNode( tackleAttackState );

        Node<AbstractStateDescription> hurtState = new Node<AbstractStateDescription>( GetComponent<HurtState>());
        m_stateMachine.AddNode( hurtState );

        // Transitions
        m_stateMachine.AddEdge( idleState, idleState, (int) IdlePerception.Nothing );
        m_stateMachine.AddEdge( idleState, followPlayerState, (int) IdlePerception.PlayerInSight );
        m_stateMachine.AddEdge( idleState, hurtState, (int) IdlePerception.Hurt );

        m_stateMachine.AddEdge( followPlayerState, followPlayerState, (int) FollowPlayerPerception.ChasePlayer );
        m_stateMachine.AddEdge( followPlayerState, idleState, (int) FollowPlayerPerception.LostPlayer );
        m_stateMachine.AddEdge( followPlayerState, loadAttackState, (int) FollowPlayerPerception.PlayerIsCloseEnough );
        m_stateMachine.AddEdge( followPlayerState, hurtState, (int) FollowPlayerPerception.Hurt );

        m_stateMachine.AddEdge( loadAttackState, loadAttackState, (int) LoadAttackPerception.IsLoading );
        m_stateMachine.AddEdge( loadAttackState, tackleAttackState, (int) LoadAttackPerception.ReadyToLaunch );
        m_stateMachine.AddEdge( loadAttackState, followPlayerState, (int) LoadAttackPerception.EnemyToFar );
        m_stateMachine.AddEdge( loadAttackState, hurtState, (int) LoadAttackPerception.Hurt );

        m_stateMachine.AddEdge( tackleAttackState, tackleAttackState, (int) TackleAttackPerception.Attacking );
        m_stateMachine.AddEdge( tackleAttackState, idleState, (int) TackleAttackPerception.EndAttack );

        m_stateMachine.AddEdge( hurtState, hurtState, (int) HurtPerception.Stuned );
        m_stateMachine.AddEdge( hurtState, idleState, (int) HurtPerception.Recover );
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.green;
    //     Gizmos.DrawWireSphere( this.transform.position, m_sightRadius );
    // }

}
