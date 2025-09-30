using System.Drawing;
using UnityEngine;
using UnityEngine.AI;

public class NPCPlayerDetection : MonoBehaviour
{

    public NPCChase npcChase; // Reference to the NPCChase script to handle chasing logic7
    public NPCInvestigate NPCInvestigate; // Reference to the NPCInvestigate script to handle investigation logic

    public float detectionRadius = 10f; // Radius within which the NPC can detect the player
    public LayerMask CollisionLayers; // Layer that the player is on

    public Transform targetLocationChase; // Target location for the NPC to chase
    public Transform player;
    
    Mesh mesh;  //AI Scan Variables
    public int scanFrequency = 30;
    int count;

    private float scanInterval;
    [SerializeField]
    private float scanTimer;
    private Animator animator;
    [SerializeField]
    private UnityEngine.Color color;
    [SerializeField]
    private float SightScannerWidth;
    [SerializeField]
    private float SightScannerHeight;
    public float SightScannerDistance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        npcChase = GetComponent<NPCChase>(); // Get the NPCChase component attached to the same GameObject
        NPCInvestigate = GetComponent<NPCInvestigate>(); // Get the NPCInvestigate component attached to the same GameObject

        mesh = WedgeMesh();
        scanInterval = 1.0f / scanFrequency;
    }

    // Update is called once per frame
    void Update()
    {
        
        scanTimer -= Time.deltaTime;
        if (scanTimer <= 0.0f)
        {
            //Debug.Log(scanTimer);
            scanTimer = scanInterval;
            Detection();
        }
    }



    //This method is called when physics.OverlapSphere detects a collision with the player or any sound source

    public void Detection()
    {
        //Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, CollisionLayers);
        
        //foreach (var hitCollider in hitColliders)
        //{
        //    if(hitCollider.CompareTag("Player") && hitCollider.CompareTag("SoundSource"))
        //    {
        //        Debug.Log("Player and sound source detected within radius!");

        //        //If both types of gameObjects are inside the radius, always choose the player
        //        if (targetLocationChase != player)
        //        {
        //            targetLocationChase = player;
        //            npcChase.ChasePlayer(targetLocationChase);
        //        }

        //        return;
        //    }
        //    if (hitCollider.CompareTag("Player"))
        //    {
        //        Debug.Log("Player detected within radius!");
        //        // Add Chase logic to handle player detection
        //        targetLocationChase = hitCollider.transform; // Set the target location to the player's position

        //        npcChase.ChasePlayer(targetLocationChase); // Call the ChasePlayer method from NPCChase script
                
        //    }
        //    else if(hitCollider.CompareTag("SoundSource"))
        //    {
        //        Debug.Log("Sound source detected within radius!");
        //        // Add logic to handle sound detection
        //        targetLocationChase = hitCollider.transform;
        //        NPCInvestigate.Investiget(targetLocationChase.position); // Call the Investigate method from NPCInvestigate script
        //    }
            
        //}
        if (!InSight(player.gameObject))
        {
            targetLocationChase = null;
            return;
        }

        if (InSight(player.gameObject))
        {
            targetLocationChase = player;
            npcChase.ChasePlayer(player);
            return;
        }
    }

    public bool InSight(GameObject targetObject)
    {
        //Checking if the gameObjects are in the same height as agent
        Vector3 origin = transform.position;
        Vector3 dest = targetObject.transform.position;
        Vector3 dir = dest - origin;
        if (dir.y > SightScannerHeight)
        {
            Debug.Log("Failed Height Check");
            
            return false;
        }

        dir.y = 0;
        float deltaAngle = Vector3.Angle(transform.forward, dir);
        if (deltaAngle > SightScannerWidth)
        {
            Debug.Log("Failed Field of View Check");
            
            return false;
        }

        origin.y += SightScannerHeight / 2;
        dest.y = origin.y;
        if (Physics.Linecast(origin, dest, CollisionLayers))
        {
            
            Debug.Log("Failed Line of Sight Check");
            return false;
        }

        if(Vector3.Distance(origin, dest) > SightScannerDistance)
        {
            Debug.Log("Failed Distance Check");
            return false;
        }

        return true;
    }

    public void ForceNPCPlayerChase()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(player.transform.position);
    }

    private void OnDrawGizmos()
    {
        // Draw a sphere in the editor to visualize the detection radius
        Gizmos.color = UnityEngine.Color.red; // Set the color of the Gizmos to red
        Gizmos.DrawWireSphere(transform.position, detectionRadius); // Draw a wireframe sphere at the NPC's position with the specified radius

        if (mesh)
        {
            Gizmos.color = color;
            Gizmos.DrawMesh(mesh, transform.position, transform.rotation, transform.localScale);
        }
    }


    Mesh WedgeMesh()
    {
        Mesh mesh = new Mesh();
        int segments = 10;

        int numTriangles = (segments * 4) + 2 + 2;
        int numVertices = numTriangles * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -SightScannerWidth, 0) * Vector3.forward * SightScannerDistance;
        Vector3 bottomRight = Quaternion.Euler(0, SightScannerWidth, 0) * Vector3.forward * SightScannerDistance;

        Vector3 topCenter = bottomCenter + Vector3.up * SightScannerHeight;
        Vector3 topRight = bottomRight + Vector3.up * SightScannerHeight;
        Vector3 topLeft = bottomLeft + Vector3.up * SightScannerHeight;

        int vert = 0;

        //Left Side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;

        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;

        //Right Side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = topCenter;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCenter;

        float currentAngle = -SightScannerWidth;
        float deltaAngle = (SightScannerWidth * 2) / segments;

        for (int i = 0; i < segments; i++)
        {

            bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * SightScannerDistance;
            bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * SightScannerDistance;

            topRight = bottomRight + Vector3.up * SightScannerHeight;
            topLeft = bottomLeft + Vector3.up * SightScannerHeight;

            currentAngle += deltaAngle;

            //Far Side
            vertices[vert++] = bottomLeft;
            vertices[vert++] = bottomRight;
            vertices[vert++] = topRight;


            vertices[vert++] = topRight;
            vertices[vert++] = topLeft;
            vertices[vert++] = bottomLeft;

            //Top
            vertices[vert++] = topCenter;
            vertices[vert++] = topLeft;
            vertices[vert++] = topRight;

            //Bottom
            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomLeft;

        }

        for (int i = 0; i < numVertices; i++)
        {
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();


        return mesh;
    }

    
}
