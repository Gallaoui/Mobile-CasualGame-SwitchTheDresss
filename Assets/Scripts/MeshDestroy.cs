//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDestroy : MonoBehaviour
{

    public float cubeSize = 0.2f;
    public int cubeInRow = 5;
    public Material col;
    float cubesPivotDistance;
    Vector3 cubePivot;
    public float explosionRadius = 4f;
    public float explosionUpward = 0.4f;
    public float explosionForce = 50f;

    void Start()
    {
        cubesPivotDistance = cubeSize * cubeInRow / 2;

        cubePivot = new Vector3(0, cubesPivotDistance, cubesPivotDistance);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            explode();
        }
    }

    public void explode()
    {
        gameObject.SetActive(false);

        for(int x = 0; x < cubeInRow; x++)
        {
            for (int y = 0; y < cubeInRow; y++)
            {
                for (int z = 0; z < cubeInRow; z++)
                {
                    createPiece(x,y,z, Random.Range(0.2f,2f), Random.Range(0.2f,7f));
                }
            }
        }

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
            }
        }
    }

    void createPiece(int x, int y, int z, float ry, float rz)
    {
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

        piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubePivot;
        piece.transform.localScale = new Vector3(0.005f, ry, rz);

        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Renderer>().material = col;
        piece.GetComponent<Rigidbody>().mass = cubeSize;
    }

}