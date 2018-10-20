using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge<T>{
    public Node<T> InputNode { get; set; }
    public Node<T> OutputNode { get; set; }
    public int TransitionState { get; set; }

}
