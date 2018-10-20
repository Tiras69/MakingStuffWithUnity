using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kiwiii.ZombieEscape.CommonServices
{

    public class SimpleEventAggregator : Singleton<SimpleEventAggregator>
    {
        #region Fields
        private Dictionary<Type, Dictionary<object,Action<IEventMessage>>> m_delegatesDictionary;
        private Dictionary<Action<IEventMessage>, IEventMessage > m_delayedMessages;
        #endregion

        #region Unity Messages
        //---------------------------------------------------------------------------------------------
        public void Awake()
        {
            m_delegatesDictionary = new Dictionary<Type, Dictionary<object, Action<IEventMessage>>>();
            m_delayedMessages = new Dictionary<Action<IEventMessage>, IEventMessage>();
        }

        //---------------------------------------------------------------------------------------------
        public void LateUpdate()
        {
            if ( m_delayedMessages.Any() )
            {
                foreach ( Action<IEventMessage> eventDelegate in m_delayedMessages.Keys )
                {
                    eventDelegate( m_delayedMessages [ eventDelegate ] );
                }
                m_delayedMessages.Clear();
            }
        }
        #endregion

        #region Methods
        //-------------------------------------------------------------------------------------------------------------------------
        public void RegisterEvent<T>( object _targetObject, Action<T> _callBack ) where T : IEventMessage
        {
            if ( m_delegatesDictionary.ContainsKey( typeof( T ) ) )
            {
                Dictionary<object, Action<IEventMessage>> currentDelegatesDictionary = m_delegatesDictionary[typeof(T)];
                if ( currentDelegatesDictionary.ContainsKey( typeof( T ) ) )
                {
                    Debug.LogWarning( "The object " + _targetObject.ToString() + " has already registered a Call Back Method of type " + typeof( T ).ToString() );
                }
                else
                {
                    currentDelegatesDictionary.Add( _targetObject, new Action<IEventMessage>( message => _callBack( (T) message ) ) );
                }
            }
            else
            {
                Dictionary<object, Action<IEventMessage>> newDelegatesDictionary = new Dictionary<object, Action<IEventMessage>>();
                newDelegatesDictionary.Add( _targetObject, new Action<IEventMessage>( message => _callBack( (T) message ) ) );
                m_delegatesDictionary.Add( typeof( T ), newDelegatesDictionary );
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------
        public void UnregisterEvent<T>( object _targetObject, Action<T> _callBack ) where T : IEventMessage
        {
            if ( m_delegatesDictionary.ContainsKey( typeof( T ) ) )
            {
                Dictionary<object, Action<IEventMessage>> currentDelegatesDictionary = m_delegatesDictionary[typeof(T)];
                if ( currentDelegatesDictionary.ContainsKey( _targetObject ) )
                {
                    currentDelegatesDictionary.Remove( _targetObject );
                    if ( !currentDelegatesDictionary.Keys.Any() )
                    {
                        m_delegatesDictionary.Remove( typeof( T ) );
                    }
                }
                else
                {
                    Debug.LogWarning( "There's no method of type " + typeof( T ).ToString() + " to unregister in the object " + _targetObject.ToString() );
                }
            }
            else
            {
                Debug.LogWarning( "There's no method of type " + typeof( T ).ToString() + " to unregister in the object " + _targetObject.ToString() );
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------
        public void EmitMessage<T>( T _message ) where T : IEventMessage
        {
            if ( m_delegatesDictionary.ContainsKey( typeof( T ) ) )
            {
                Dictionary < object,Action < IEventMessage >> currentDelegatesDictionary = m_delegatesDictionary [ typeof( T ) ];
                foreach ( object key in currentDelegatesDictionary.Keys )
                {
                    m_delayedMessages.Add( currentDelegatesDictionary [ key ], _message );
                }
            }
        }
        #endregion
    }

}