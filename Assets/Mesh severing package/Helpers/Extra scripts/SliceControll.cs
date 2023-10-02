using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 

public class SliceControll : MonoBehaviour 
{

    private Vector3 v3StartPressPos;
    private Vector3 v3EndMousePos;
	
	private Vector3 center;
	
	public Camera ActCam;
	public GameObject point;
	
	GameObject pTarget;
	Mesh pTargetMesh;
	Plane cutExt;
	
	public Edges[] precomputedEdges;
	
	private List<GameObject> spawnedpoints = new List<GameObject>();
	public List<float> side = new List<float>();
	
    // Use this for initialization
    void Start () 
	{

    }

    void Cut()
    {

        Vector3 camStartPo = ActCam.ScreenToWorldPoint(v3StartPressPos);
        Vector3 camActPos = ActCam.ScreenToWorldPoint(v3EndMousePos);

		//Debug.Log(camStartPo);
		//Debug.Log(camActPos);
		
		center.x = (camStartPo.x + camActPos.x + ActCam.transform.position.x)/3;
		center.y = (camStartPo.y + camActPos.y + ActCam.transform.position.y)/3;
		center.z = (camStartPo.z + camActPos.z + ActCam.transform.position.z)/3;
		
		cutExt = new Plane(camStartPo, camActPos, ActCam.transform.position);
		//cutExt = new Plane(camStartPo, camActPos, ActCam.transform.position);
		
		Debug.DrawLine(camStartPo, camActPos, Color.red, 3);
		
		Debug.DrawLine(camActPos, ActCam.transform.position, Color.green, 3);
		
		Debug.DrawLine(ActCam.transform.position, camStartPo,Color.blue, 3);
		
		Debug.DrawRay(center, cutExt.normal,Color.yellow, 3);
		
	
		//Instantiate(point, camStartPo, Quaternion.identity);
		//Instantiate(point, camActPos, Quaternion.identity);
		//Instantiate(point, ActCam.transform.position, Quaternion.identity);
		
		Vector3 v3DirectionIn = (center - ActCam.transform.position).normalized;
		
        Ray rayIn = new Ray(ActCam.transform.position, v3DirectionIn);
		
		Debug.DrawRay(ActCam.transform.position, v3DirectionIn, Color.black, 6);

        RaycastHit tHitIn;

        if(Physics.Raycast(rayIn, out tHitIn))
        {

            Debug.DrawRay(ActCam.transform.position, v3DirectionIn, Color.black, 6);
			//Instantiate(point, tHitIn.point, Quaternion.identity);
            //Debug.Log("In point: " + tHitIn.point.ToString());
			
			if(tHitIn.transform.gameObject != null)
			{
	        	pTarget = tHitIn.transform.gameObject;
				MeshFilter filterExists = pTarget.GetComponent<MeshFilter>();
				
				if(filterExists == null)
				{
					pTargetMesh = pTarget.GetComponent<SkinnedMeshRenderer>().sharedMesh;
				}
				else
				{
					pTargetMesh = pTarget.GetComponent<MeshFilter>().mesh;
				}
				
				precomputedEdges = EdgeBuilder.BuildManifoldEdges(pTargetMesh);
			} 

        }
		
		
		
        
		
		//Debug.Log("Object: " + pTarget.gameObject.name + "---Cant Verts: " + pTargetMesh.vertices.Length);
			
        /*for(int i = 0 ; i < pTargetMesh.vertices.Length; i++)
        {
            Debug.Log("--Verts " + (i) + " :" + pTargetMesh.vertices[i]);

        }*/
		
		
		
		/*for(int i = 0; i < precomputedEdges.Length; i++)
		{
			//Debug.DrawLine(pTarget.transform.position + pTargetMesh.vertices[precomputedEdges[i].vertexIndex[0]], pTarget.transform.position + pTargetMesh.vertices[precomputedEdges[i].vertexIndex[1]],Color.black,3);
			
			Debug.DrawLine( pTarget.transform.TransformPoint(pTargetMesh.vertices[precomputedEdges[i].vertexIndex[0]]),
							pTarget.transform.TransformPoint(pTargetMesh.vertices[precomputedEdges[i].vertexIndex[1]]),Color.red,3);
		}*/

    }

    // Update is called once per frame
    void LateUpdate () 
    {

        if(Input.GetButtonDown("Fire1"))
        {
            v3StartPressPos = Input.mousePosition;
			v3StartPressPos.z = 10;
			foreach(GameObject go in spawnedpoints)
			{
				Destroy(go);
			}
			spawnedpoints.Clear();
        }
        else if(Input.GetButtonUp("Fire1"))
        {
			v3EndMousePos = Input.mousePosition;
			v3EndMousePos.z = 10;
            Cut();
			AddNewVertices();
        }
		
    }
	
	private void AddNewVertices()
	{
		//List<Vector3> cutsec = new List<Vector3>();
		
		for(int i = 0; i < precomputedEdges.Length; i++)
		{
			Vector3 p1 = pTarget.transform.TransformPoint(pTargetMesh.vertices[precomputedEdges[i].vertexIndex[0]]);
			Vector3 p2 = pTarget.transform.TransformPoint(pTargetMesh.vertices[precomputedEdges[i].vertexIndex[1]]);
			
			Vector3 rayDir = (p2 - p1).normalized;//dir to p2
			
			float dist = Vector3.Distance(p1, p2);//real dist between p1 and p2.
			float enter = dist;//value used for raycast
			
			Ray edgeLine = new Ray(p1, rayDir);
			
			
			/*Debug.Log("p1: " + p1 + "  p2: " + p2 + "  dist: " + dist);
			
			if(dist > 1)
			{
				Debug.DrawRay(p1, rayDir * dist, Color.red, 3);
				Debug.Log("element: " + i + " /index P1: " + precomputedEdges[i].vertexIndex[0] + " /p1: " + p1 + " /index P2: " + precomputedEdges[i].vertexIndex[1] + " /p2: " + p2 + " /dist: " + dist);
			}
			
			Instantiate(point, p1, Quaternion.identity);*/
			
			Debug.DrawRay(p1, rayDir * dist, Color.red, 3);
			//Debug.DrawLine(p1, p2, Color.red, 3);
			
			if(cutExt.Raycast(edgeLine, out enter))
			{
				if(enter <= dist)
				{
					Vector3 HitPointOnPlane = edgeLine.GetPoint(enter);
					
					
					GameObject newPointGo = Instantiate(point, HitPointOnPlane, Quaternion.identity) as GameObject;
					point.transform.name = "inst_p" + i;
					spawnedpoints.Add(newPointGo);
					
					side.Add(cutExt.GetDistanceToPoint(HitPointOnPlane));
					//Debug.DrawRay(edgeLine.origin, edgeLine.direction * enter, Color.red, 3);
					
					//cutsec.Add(HitPointOnPlane);
					
					//Debug.Log("point: " + point.name + "   p1: " + p1 + "  p2: " + p2 + "  dist: " + enter);
				}
				
			}
			
			
		}
		
	}

}