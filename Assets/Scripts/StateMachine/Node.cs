using System.Collections.Generic;

public class Node<T>
{
    public T Data { get; set; }
    public List<Edge<T>> Edges { get; set; }

    public Node( T _data )
    {
        Data = _data;
        Edges = new List<Edge<T>>();
    }

    public Node<T> GetNextState(int _transitionState)
    {
        foreach(Edge<T> edge in Edges)
        {
            if( edge.TransitionState == _transitionState)
            {
                return edge.OutputNode;
            }
        }
        return null;
    }
}
