using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser
{
    public Vector3 position, direction;

    public GameObject laserObject;
    public LineRenderer lineRenderer;
    public List<Vector3> laserIndices = new List<Vector3>();
    public LayerMask layers;

    public Laser(Vector3 pos, Vector3 direction, Material mat, float alpha, LayerMask layers)
    {
        this.lineRenderer = new LineRenderer();
        this.laserObject = new GameObject();
        this.laserObject.name = "LaserBeam";
        this.position = pos;
        this.direction = direction;
        this.layers = layers;

        this.lineRenderer = this.laserObject.AddComponent(typeof(LineRenderer)) as LineRenderer;
        this.lineRenderer.startWidth = 0.1f;
        this.lineRenderer.endWidth = 0.1f;
        this.lineRenderer.material = mat;
        mat.SetColor("_BaseColor", new Color(1,0,0,alpha));

        this.lineRenderer.startColor = new Color(1,0,0,alpha);
        this.lineRenderer.endColor = new Color(1, 0, 0, alpha);

        CastRay(pos, direction, lineRenderer, layers);
    }

    void CastRay(Vector3 pos, Vector3 dir, LineRenderer laser, LayerMask layers)
    {
        laserIndices.Add(pos);

        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 30, layers))
        {
            CheckHit(hit, dir, laser, ray);
        }
        else
        {
            laserIndices.Add(ray.GetPoint(30));
            UpdateLaser();
        }

        
    }

    void CheckHit(RaycastHit info, Vector3 direction, LineRenderer laser, Ray ray)
    {
        if(info.collider.gameObject.tag == "Reflective")
        {
            Vector3 pos = info.point;
            Vector3 newDirection = Vector3.Reflect(direction, info.normal);

            CastRay(pos, newDirection, laser, layers);

        }
        else if (info.collider.gameObject.tag == "RedirectionCube")
        {
            Ray newRay = new Ray(ray.GetPoint(100000), -ray.direction);
            RaycastHit newHit;
            info.collider.Raycast(newRay, out newHit, Mathf.Infinity);

            info.collider.gameObject.transform.GetComponent<LaserRedirectionCube>().EnableLaser();

            CastRay(newHit.point, direction, laser, layers);

        }
        else if (info.collider.gameObject.tag == "LaserReciever")
        {
            Ray newRay = new Ray(ray.GetPoint(100000), -ray.direction);
            RaycastHit newHit;
            info.collider.Raycast(newRay, out newHit, Mathf.Infinity);

            info.collider.gameObject.transform.GetComponent<LaserReciever>().EnableReciever();

            CastRay(newHit.point, direction, laser, layers);

        }
        else
        {
            laserIndices.Add(info.point);
            UpdateLaser();
        }
    }

    void UpdateLaser()
    {
        int count = 0;
        lineRenderer.positionCount = laserIndices.Count;

        foreach (Vector3 index in laserIndices)
        {
            lineRenderer.SetPosition(count, index);
            count++;
        }
    }
}
