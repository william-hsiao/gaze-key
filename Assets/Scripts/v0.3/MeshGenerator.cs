using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeshGenerator : MonoBehaviour {
    public int size;
    public Color baseColour, highlight;
    double startAngle, sectorAngle;
    MeshFilter meshFilter;
    MeshCollider meshCollider;
    MeshRenderer meshRenderer;

    //Initialize ==============================================================
	void Awake () {
        meshFilter = this.gameObject.AddComponent<MeshFilter>();
        meshCollider = this.gameObject.AddComponent<MeshCollider>();
        meshRenderer = this.gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material.color = baseColour;
    }

    private void Start() {
        Draw();
    }


    //Public Methods ==========================================================
    public void SetAngles(double startAngle, double sectorAngle) {
        this.startAngle = startAngle;
        this.sectorAngle = sectorAngle;
        Draw();
    }

    public void SetSize(int size) {
        this.size = size;
        Refresh();
    }

    public void Refresh () {
        Draw();
    }


    //Methods =================================================================
    void Draw () {
        int i;
        int increments = (int)sectorAngle; //Change to lower resolution
        double incAngle = sectorAngle / increments;
        Mesh temp = new Mesh();

        Vector3[] vertices = new Vector3[increments + 2];
        vertices[0] = new Vector3(0, 0, 0);
        for (i = 0; i <= increments; i++) {
            vertices[i + 1] = new Vector3((float)(size * Math.Cos((startAngle + i * incAngle) * Math.PI / 180)), (float)(size * Math.Sin((startAngle + i * incAngle) * Math.PI / 180)), 0);
        }
        temp.vertices = vertices;


        int[] triangles = new int[increments * 3];
        int count = 1;
        for (i = 0; i < triangles.Length; i += 3) {
            triangles[i] = 0;
            triangles[i + 1] = count + 1;
            triangles[i + 2] = count;
            count++;
        }
        temp.triangles = triangles;



        Vector3[] normals = new Vector3[vertices.Length];
        for (i = 0; i < vertices.Length; i++) {
            normals[i] = -Vector3.forward;
        }
        temp.normals = normals;


        //Vector2[] uv = new Vector2[vertices.Length];
        ////Coord Mapping
        //uv[0] = new Vector2(0, 0);
        //uv[1] = new Vector2(1, 0);
        //uv[2] = new Vector2(0, 1);
        //uv[3] = new Vector2(1, 1);
        //mesh.uv = uv;

        meshFilter.mesh = temp;
        meshCollider.sharedMesh = meshFilter.sharedMesh;
    }
}