using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuniconMeshCreate : MonoBehaviour {

    Mesh _mesh;
    Vector3[] originalVertexes;

    public Mesh Mesh
    {
        get { return _mesh; }
    }

    public Vector3[] Vertexes
    {
        get
        {
            Vector3[] vtx = new Vector3[_mesh.vertices.Length - 1];
            for (int i = 0; i < vtx.Length;i++)
            {
                vtx[i] = _mesh.vertices[i];
            }

            return vtx;
        }

        set
        {
            var vtx = _mesh.vertices;
            for (int i = 0; i < value.Length;i++)
            {
                _mesh.vertices = vtx;
                _mesh.RecalculateBounds();
            }

        }
    }

    public Vector3 CenterPoint
    {
        get
        {
            return _mesh.vertices[_mesh.vertices.Length - 1];
        }
        set
        {
            var vtx = _mesh.vertices;
            vtx[vtx.Length - 1] = value;
            _mesh.vertices = vtx;
            _mesh.RecalculateBounds();
        }
    }

    public Vector3[] OriginalVertexes
    {
        get { return originalVertexes; }
    }

    //コンストラクタ
    public PuniconMeshCreate(int vertexCount, float radius)
    {
        CreateMesh(vertexCount, radius);
    }

    bool CreateMesh(int vertexCount, float radius)
    {
        _mesh = new Mesh();

        //頂点の生成
        Vector3[] points = new Vector3[vertexCount + 1];
        originalVertexes = new Vector3[vertexCount];
        var angle = Mathf.PI * 2;
        for (int i = 0; i < vertexCount; i++)
        {
            var r = angle / vertexCount * i;
            var x = Mathf.Cos(r) * radius;
            var y = Mathf.Sin(r) * radius;
            points[i] = new Vector3((float)x, (float)y, 0);
            originalVertexes[i] = points[i];
        }
        points[vertexCount] = Vector3.zero; //中心

        //生成した頂点をメッシュにセット
        _mesh.vertices = points;

        //頂点インデックスの生成
        int[] indexes = new int[vertexCount * 3];
        for (int i = 0; i < vertexCount;i++)
        {
            int index = i * 3;
            indexes[index + 0] = i;
            indexes[index + 1] = (i + 1) % vertexCount;
            indexes[index + 2] = vertexCount;
        }

        //メッシュにセット
        _mesh.triangles = indexes;

        //頂点色の生成
        Color[] colors = new Color[vertexCount + 1];
        for (int i = 0; i < vertexCount; i++)
        {
            colors[i] = Color.white;
        }
        colors[vertexCount] = Color.clear;  //中心の色

        //メッシュにカラーをセット
        _mesh.colors = colors;

        _mesh.RecalculateBounds();

        return true;
    }
}
