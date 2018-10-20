using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicStateMachine : AbstractStateMachine
{

    public float m_attackPower;
    public float m_attackTime;

    public float m_stunTime;
    public float m_sightRadius;
    public float m_attackRadius;
    public float m_loadAttackTime;

    protected override void DefineStateMachine()
    {
        // States
        Node<IStateDescription> idleState = new Node<IStateDescription>( new IdleState()
        {
            GameObject = this.gameObject,
            SightRadius = m_sightRadius
        });
        m_stateMachine.AddNode( idleState );

        Node<IStateDescription> followPlayerState = new Node<IStateDescription>( new FollowPlayerState()
        {
            GameObject = this.gameObject,
            SightRadius = m_sightRadius,
            AttackRadius = m_attackRadius
        });
        m_stateMachine.AddNode( followPlayerState );

        Node<IStateDescription> loadAttackState = new Node<IStateDescription>( new LoadAttackState()
        {
            GameObject = this.gameObject,
            ChargeTime = m_loadAttackTime,
            SightRadius = m_sightRadius
        });
        m_stateMachine.AddNode( loadAttackState );

        Node<IStateDescription> tackleAttackState = new Node<IStateDescription>( new TackleAttackState()
        {
            GameObject = this.gameObject,
            AttackPower = m_attackPower,
            EndTime = m_attackTime,
            SightRadius = m_sightRadius
        } );
        m_stateMachine.AddNode( tackleAttackState );

        Node<IStateDescription> hurtState = new Node<IStateDescription>( new HurtState()
        {
            GameObject = this.gameObject,
            StunTime = m_stunTime
        });
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere( this.transform.position, m_sightRadius );
    }

}
