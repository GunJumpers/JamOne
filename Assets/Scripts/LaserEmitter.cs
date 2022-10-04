using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEmitter : MonoBehaviour
{
    public Material mat;
    public Laser beam;
    public float targetAlpha;
    public bool isEmitting;

    // Start is called before the first frame update
    void Start()
    {
        mat = Instantiate<Material>(mat);
    }

    // Update is called once per frame
    void Update()
    {
        if(beam != null && beam.laserObject != null)
        {
            Destroy(beam.laserObject);
        }

        if (isEmitting)
        {
            beam = new Laser(transform.position, transform.right, mat, targetAlpha);
        }

        
    }

    public void SetEmitStatus(bool status)
    {
        isEmitting = status;
    }
}
