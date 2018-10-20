using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public static class FileNames
    {
        public const string CommandFileName = "CommandsSpecifier.xml";
    }

    public static class ProceduralsParameters
    {
        public const float RoomSize = 32.0f;
    }

    public static class Tags
    {
        public const string Player = "Player";
        public const string Bullet = "Bullet";
    }

    public static class AllocatorParameters
    {
        public const int InstanceCountMaximum = 200;
    }
}
