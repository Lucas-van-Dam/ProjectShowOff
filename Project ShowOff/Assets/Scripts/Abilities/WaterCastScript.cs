using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WaterCastScript : MonoBehaviour
{

    Vector3[] waterNodes;
    Vector3[] previousWaterNodes;

    
    Vector3 velocity;

    [SerializeField]
    int maxCasts;

    [SerializeField]
    float gravity;
    [SerializeField]
    float force;
    [SerializeField]
    float flowDamping;

    [SerializeField]
    float movementDamping;

    [SerializeField]
    ParticleSystem splash;

    [SerializeField]
    MeshFilter mF;
    [SerializeField]
    MeshRenderer mR;
    Mesh mesh;

    Vector3[] verts;
    int[] tris;

    [SerializeField]
    [Range(3, 100)]
    int subdivisions;

    int exposedNodes;

    [SerializeField]
    float loopSize;
    [SerializeField]
    float finalLoopSize;

    [SerializeField]
    bool isOn;
    bool wasOff;

    // Start is called before the first frame update
    void Start()
    {
        waterNodes = new Vector3[maxCasts];
        previousWaterNodes = new Vector3[maxCasts];

        verts = new Vector3[maxCasts * subdivisions];
        tris = new int[(maxCasts - 1) * subdivisions * 6];

        if(mF == null)
        {
            mF = GetComponent<MeshFilter>();
        }

        if(mR == null)
        {
            mR = GetComponent<MeshRenderer>();
        }

        BuildMesh();

        InitMesh();

        splash = Instantiate(splash);
        
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            isOn = !isOn;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isOn = !isOn;
        }


        waterNodes[0] = transform.position;
        previousWaterNodes[0] = transform.position;

        velocity = transform.forward * force;

        for(int i = 1; i < maxCasts; i++)
        {
            waterNodes[i] = waterNodes[i - 1] + velocity;

            velocity *= flowDamping;
            velocity += Vector3.down * gravity;

            previousWaterNodes[i] = Vector3.Lerp(previousWaterNodes[i], waterNodes[i], movementDamping / (i * 0.2f));

            //Debug.DrawRay(previousWaterNodes[i - 1], previousWaterNodes[i] - previousWaterNodes[i - 1], Color.yellow);
        }


        //RaycastHit hit;
        //if (Physics.Raycast(previousWaterNodes[1], previousWaterNodes[1] - previousWaterNodes[0], out hit, (previousWaterNodes[i] - previousWaterNodes[i - 1]).magnitude))
        //{
        //    UpdateSplash(hit, 0);
        //    i = maxCasts;
        //}
        //else
        //{
        //    splash.Stop();
        //}

        if (isOn)
        {
            if (wasOff)
            {
                InitMesh();
                mR.enabled = true;
            }

            int hitIndex;
            Vector3 hitPoint;
            UpdateRaycast(out hitIndex, out hitPoint);

            UpdateMesh(hitIndex, hitPoint);
            wasOff = false;
        }
        else
        {
            mR.enabled = false;
            wasOff = true;
            splash.Stop();
        }
       

        for(int i = 0; i < maxCasts; ++i)
        {
            //Debug.DrawLine(verts[i], verts[i] + new Vector3(0, 0.1f, 0), Color.red);
        }
    }

    void BuildMesh()
    {
        for(int node = 0; node < maxCasts; node++)
        {

            Vector3 perpendicular = Vector3.zero;
            float angle = 0;

            if (node == 0)
            {
                perpendicular = transform.right;
                angle = transform.rotation.eulerAngles.x;

                //Debug.Log("WAAAAAAAAAA");
            }
            else if (node < maxCasts - 1)
            {
                perpendicular = Vector3.Cross(previousWaterNodes[node] - transform.position, previousWaterNodes[node + 1] - previousWaterNodes[node]);
                angle = Vector3.Angle(Vector3.up, previousWaterNodes[node + 1] - previousWaterNodes[node]);
            }
            else
            {
                perpendicular = Vector3.Cross(previousWaterNodes[node] - transform.position, previousWaterNodes[node] - previousWaterNodes[node - 1]);
                angle = Vector3.Angle(Vector3.up, previousWaterNodes[node] - previousWaterNodes[node - 1]);
            }

            for (int i = 0; i < subdivisions; i++)
            {

                Vector3 offset = previousWaterNodes[node] - transform.position;
                float size = Mathf.Lerp(loopSize, finalLoopSize, node / maxCasts);
                Vector3 localPosition = Quaternion.AngleAxis((360 / subdivisions) * i, Vector3.up) * Vector3.forward * size;

                //Quaternion pointingAt = Quaternion.LookRotation(previousWaterNodes[node + 1] - previousWaterNodes[node]);
                //Debug.DrawLine(Vector3.zero , perpendicular);

                

                localPosition = Quaternion.AngleAxis(angle, perpendicular) * localPosition;

                verts[(node * subdivisions) + i] = Quaternion.Inverse(transform.rotation) * (offset + localPosition);
                //Debug.Log(verts[(node * subdivisions) + i]);
            }
        }

        for (int node = 0; node < maxCasts - 1; node++)
        {
            for (int i = 0; i < subdivisions - 1; i++)
            {
                
                int startPoint = (node * subdivisions * 6) + 6 * i;
                tris[startPoint] = node * subdivisions + i;
                tris[startPoint + 1] = node * subdivisions + i + 1;
                tris[startPoint + 2] = (node + 1) * subdivisions + i;

                tris[startPoint + 3] = node * subdivisions + i + 1;
                tris[startPoint + 4] = (node + 1) * subdivisions + i + 1;
                tris[startPoint + 5] = (node + 1) * subdivisions + i;

                //Debug.Log(tris[startPoint + 4]);
            }

            //finishing the loop
            int startPointLoop = (node * subdivisions * 6) + 6 * (subdivisions - 1);
            tris[startPointLoop] = node * subdivisions + (subdivisions - 1);
            tris[startPointLoop + 1] = node * subdivisions;
            tris[startPointLoop + 2] = (node + 1) * subdivisions + (subdivisions - 1);

            tris[startPointLoop + 3] = node * subdivisions;
            tris[startPointLoop + 4] = (node + 1) * subdivisions;
            tris[startPointLoop + 5] = (node + 1) * subdivisions + (subdivisions - 1);

        }

        mesh = new Mesh();
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.UploadMeshData(false);

        mF.mesh = mesh;
    }

    void UpdateRaycast(out int hitIndex, out Vector3 hitPoint)
    {
        for (int i = 0; i < maxCasts - 1; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(previousWaterNodes[i], previousWaterNodes[i + 1] - previousWaterNodes[i], out hit, (previousWaterNodes[i + 1] - previousWaterNodes[i]).magnitude))
            {

                if (hit.transform.gameObject.tag == "Extinguishable")
                {
                    splash.Stop();

                    ParticleSystem fire = hit.transform.gameObject.GetComponent<ParticleSystem>();
                    if (fire != null)
                    {
                        //Debug.Log("SHRINK -");
                        fire.transform.localScale = new Vector3(1, 1, fire.transform.localScale.z * 0.995f);
                        if (fire.transform.localScale.z < 0.2f)
                        {
                            Destroy(fire.gameObject);
                        }
                    }
                }
                UpdateSplash(hit, i);

                hitIndex = i;
                hitPoint = hit.point;

                Debug.DrawLine(previousWaterNodes[i], hit.point, Color.red);

                return;
            }
            else
            {
                splash.Stop();
                Debug.DrawRay(previousWaterNodes[i], previousWaterNodes[i + 1] - previousWaterNodes[i], Color.green);
            }
        }

        hitIndex = -1;
        hitPoint = Vector3.zero;
    }

    void UpdateMesh(int hitIndex, Vector3 hitPoint)
    {

        for (int node = 0; node < maxCasts; node++)
        {

            Vector3 perpendicular = Vector3.zero;
            float angle = 0;

            if (node == 0)
            {
                perpendicular = transform.right;
                angle = transform.rotation.eulerAngles.x + 90;

                //Debug.Log("WAAAAAAAAAA");
            }
            else if (node < maxCasts - 1)
            {
                perpendicular = Vector3.Cross(previousWaterNodes[node] - transform.position, previousWaterNodes[node + 1] - previousWaterNodes[node]);
                angle = Vector3.Angle(Vector3.up, previousWaterNodes[node + 1] - previousWaterNodes[node]);
            }
            else
            {
                perpendicular = Vector3.Cross(previousWaterNodes[node] - transform.position, previousWaterNodes[node] - previousWaterNodes[node - 1]);
                angle = Vector3.Angle(Vector3.up, previousWaterNodes[node] - previousWaterNodes[node - 1]);
            }


            Vector3 offset;
            float offsetModifier;

            //hitIndex;

            if(hitIndex < 0 || hitIndex > node - 1)
            {
                offset = previousWaterNodes[node] - transform.position;
                offsetModifier = 1f;
            }
            else
            {
                //offset = previousWaterNodes[hitIndex] - transform.position;
                offset = hitPoint - transform.position;
                if (hitIndex == node - 1)
                {
                    offsetModifier = 1f;
                }
                else
                {
                    offsetModifier = 0f;
                }
            }
            

            for (int i = 0; i < subdivisions; i++)
            {

                float size = Mathf.Lerp(loopSize, finalLoopSize, node / (float)maxCasts);
                Vector3 localPosition = Quaternion.AngleAxis((360 / subdivisions) * i, Vector3.up) * Vector3.forward * size * offsetModifier;

                //Quaternion pointingAt = Quaternion.LookRotation(previousWaterNodes[node + 1] - previousWaterNodes[node]);
                //Debug.DrawLine(Vector3.zero , perpendicular);



                localPosition = Quaternion.AngleAxis(angle, perpendicular) * localPosition;

                verts[(node * subdivisions) + i] = Quaternion.Inverse(transform.rotation) * (offset + localPosition);
                //Debug.Log(verts[(node * subdivisions) + i]);
            }
        }

        mesh.vertices = verts;
        mesh.RecalculateBounds();
        mesh.UploadMeshData(false);
    }

    void InitMesh()
    {
        for(int i = 0; i < previousWaterNodes.Length;i++)
        {
            previousWaterNodes[i] = transform.position + Vector3.down * i;
        }
    }

    void UpdateSplash(RaycastHit hit, int index)
    {
        splash.transform.position = hit.point;
        splash.transform.rotation = Quaternion.LookRotation(hit.normal);

        Debug.DrawRay(hit.point, hit.normal);

        splash.Play();
        splash.transform.localScale = Vector3.one * Mathf.Lerp(loopSize * 10, finalLoopSize * 7, index / (float)maxCasts);
    }

    private void OnDisable()
    {
        Destroy(splash);
    }
}






//RaycastHit hit;
//// Does the ray intersect any objects excluding the player layer
//if (Physics.Raycast(waterNodes[i - 1], velocity, out hit, velocity.magnitude))
//{
//    waterNodes[i] = hit.point;

//    Debug.DrawRay(waterNodes[i - 2], waterNodes[i - 1] - waterNodes[i - 2], Color.red);
//}
//else
//{
//    Debug.DrawRay(waterNodes[i - 1], velocity, Color.yellow);

//    waterNodes[i] = waterNodes[i - 1] + velocity;

//    velocity *= damping;
//    velocity += Vector3.down * gravity;
//}