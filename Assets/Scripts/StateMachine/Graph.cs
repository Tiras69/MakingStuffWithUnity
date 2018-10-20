using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Graph<T>
{
    protected List<Node<T>> m_nodes;
    protected List<Edge<T>> m_edges;

    public Graph()
    {
        m_nodes = new List<Node<T>>();
        m_edges = new List<Edge<T>>();
    }

    public void AddNode( Node<T> _node )
    {
        m_nodes.Add( _node );
    }

    public void AddEdge( Node<T> _from, Node<T> _to, int _transitionState )
    {
        if ( !m_nodes.Exists( x => x == _from ) || !m_nodes.Exists( x => x == _to ) )
        {
            Debug.Log( "Failed to create an edge, _from or/and _to node must be registered in the graph before" );
            return;
        }
        Edge<T> newEdgeFrom = new Edge<T>() { InputNode = _from, OutputNode = _to, TransitionState = _transitionState };
        m_edges.Add( newEdgeFrom );
        _from.Edges.Add( newEdgeFrom );
    }

    public Node<T> GetFirstNode()
    {
        return m_nodes.First();
    }

}
