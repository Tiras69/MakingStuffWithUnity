using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( MeshFilter ) )]
public class Moebius : MonoBehaviour
{

    public int m_twistNumber = 0;
    public float m_width = 1.0f;
    public float m_thickness = 0.2f;
    public float m_radius = 10.0f;
    public int m_detail = 10;
    private Mesh m_mesh;

    // Use this for initialization
    void Start()
    {
        m_mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = m_mesh;
        CreateMoebiusRibbon();
    }

    private void CreateMoebiusRibbon()
    {
        #region vertices creation
        float offsetCircle = 360.0f / (float)m_detail;
        float offsetTwist = ( 180.0f + 360.0f * m_twistNumber ) / (float)m_detail;
        List<Vector3> vertices = new List<Vector3>();
        for ( int i = 0; i < m_detail; i++ )
        {
            Matrix4x4 twistRotation = Matrix4x4.Rotate(Quaternion.AngleAxis(i*offsetTwist, Vector3.left));
            Matrix4x4 circleRotation = Matrix4x4.Rotate(Quaternion.AngleAxis(i*offsetCircle, Vector3.up));

            Vector3 outerTopPoint = ( Vector3.forward * m_thickness / 2.0f ) + ( Vector3.up * m_width / 2.0f );
            Vector3 innerTopPoint = ( Vector3.back * m_thickness / 2.0f ) + ( Vector3.up * m_width / 2.0f );
            Vector3 outerBottomPoint = ( Vector3.forward * m_thickness / 2.0f ) + ( Vector3.down * m_width / 2.0f );
            Vector3 innerBottomPoint = ( Vector3.back * m_thickness / 2.0f ) + ( Vector3.down * m_width / 2.0f );
            List<Vector3> pointList = new List<Vector3>()
            {
                outerTopPoint,
                innerTopPoint,
                outerBottomPoint,
                innerBottomPoint
            };

            for ( int j = 0; j < pointList.Count; j++ )
            {
                pointList [ j ] = twistRotation * pointList [ j ];
                pointList [ j ] = (Vector3.forward * m_radius) +  pointList [ j ];
                pointList [ j ] = circleRotation * pointList [ j ];
                vertices.Add( pointList [ j ] );
            }
        }
        #endregion

        #region triangles
        List<int> triangleIndices = new List<int>();
        for ( int i = 0; i < m_detail - 1; i++ )
        {
            // Top Face
            triangleIndices.Add( i * 4 + 0 );
            triangleIndices.Add( i * 4 + 4 );
            triangleIndices.Add( i * 4 + 1 );
            
            triangleIndices.Add( i * 4 + 1 );
            triangleIndices.Add( i * 4 + 4 );
            triangleIndices.Add( i * 4 + 5 );


            // Inner Face
            triangleIndices.Add( i * 4 + 1 );
            triangleIndices.Add( i * 4 + 5 );
            triangleIndices.Add( i * 4 + 3 );
            
            triangleIndices.Add( i * 4 + 3 );
            triangleIndices.Add( i * 4 + 5 );
            triangleIndices.Add( i * 4 + 7 );
            

            // Bottom Face
            triangleIndices.Add( i * 4 + 3 );
            triangleIndices.Add( i * 4 + 7 );
            triangleIndices.Add( i * 4 + 2 );
            
            triangleIndices.Add( i * 4 + 2 );
            triangleIndices.Add( i * 4 + 7 );
            triangleIndices.Add( i * 4 + 6 );
            
            
            // Outer Face
            triangleIndices.Add( i * 4 + 2 );
            triangleIndices.Add( i * 4 + 6 );
            triangleIndices.Add( i * 4 + 0 );
            
            triangleIndices.Add( i * 4 + 0 );
            triangleIndices.Add( i * 4 + 6 );
            triangleIndices.Add( i * 4 + 4 );

        }
        // Final Faces (twisted)
        // Top Face
        triangleIndices.Add( m_detail * 4 - 1 );
        triangleIndices.Add( 0 );
        triangleIndices.Add( m_detail * 4 - 2 );
        
        triangleIndices.Add( m_detail * 4 - 2 );
        triangleIndices.Add( 0 );
        triangleIndices.Add( 1 );


        // Inner Face
        triangleIndices.Add( m_detail * 4 - 2 );
        triangleIndices.Add( 1 );
        triangleIndices.Add( m_detail * 4 - 4 );
        
        triangleIndices.Add( m_detail * 4 - 4 );
        triangleIndices.Add( 1 );
        triangleIndices.Add( 3 );
        
        
        // Bottom Face
        triangleIndices.Add( m_detail * 4 - 4 );
        triangleIndices.Add( 3 );
        triangleIndices.Add( m_detail * 4 - 3 );
        
        triangleIndices.Add( m_detail * 4 - 3 );
        triangleIndices.Add( 3 );
        triangleIndices.Add( 2 );
        
        
        // Outer Face
        triangleIndices.Add( m_detail * 4 - 3 );
        triangleIndices.Add( 2 );
        triangleIndices.Add( m_detail * 4 - 1 );
        
        triangleIndices.Add( m_detail * 4 - 1 );
        triangleIndices.Add( 2 );
        triangleIndices.Add( 0 );


        #endregion
        m_mesh.vertices = vertices.ToArray();
        m_mesh.triangles = triangleIndices.ToArray();
        m_mesh.RecalculateNormals();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
