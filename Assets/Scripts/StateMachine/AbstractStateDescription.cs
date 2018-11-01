using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class AbstractStateDescription : MonoBehaviour, IStateDescription
{
    public abstract int GetPerceptionState();
}

