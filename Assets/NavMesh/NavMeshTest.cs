using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavMeshTest : MonoBehaviour {
    // Start is called before the first frame update
    public NavMeshData navMeshData;
    RaycastHit hit;
    int layerMask;
    Ray ray;
    NavMeshHit navMeshHit;
    public GameObject target;
    NavMeshPath path = null;
    int areaMask;
    NavMeshAgent agent;
    PlayerMovement playerMovement;

    void Start() {

        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(navMeshData);
        layerMask = 1 << LayerMask.NameToLayer("Map");
        target.transform.position = new Vector3(0, 0.1f, 0);
        path = new NavMeshPath();
        int area = NavMesh.GetAreaFromName("Walkable");
        areaMask = 1 << area;

        //agent=target.GetComponent<NavMeshAgent>();
        var setting = NavMesh.CreateSettings();
        setting.agentHeight = 2;
        setting.agentRadius = 0.5f;
        setting.agentTypeID = 0;

        agent = target.GetComponent<NavMeshAgent>();
        //NavMeshAgent agent;
        //NavMeshAgent.isOnNavMesh


    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            RayCast();
        }

        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        if (path.corners.Length > 0) {
            for (int i = 0; i < path.corners.Length - 1; i++)
                Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
        }
    }

    private void OnDrawGizmos() {

    }


    void RayCast() {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100, layerMask)) {
            if (NavMesh.SamplePosition(hit.point, out navMeshHit, 1, areaMask)) {
                //print(navMeshHit.position);
                //target.transform.position=navMeshHit.position;
                agent.SetDestination(navMeshHit.position);
                //Debug.LogError($"clickpos:{navMeshHit.position}");
                //MoveTo(navMeshHit.position);
            } else {
                print("not pos");
            }
        }
    }
    private void MoveTo(Vector3 pos) {
        var sourcePos = GetSimplePos(target.transform.position);
        //Debug.LogError($"source:{sourcePos}");
        //Debug.LogError($"target:{pos}");
        if (NavMesh.CalculatePath(sourcePos, pos, areaMask, path)) {
            var len = path.corners.Length;
            playerMovement.MoveToPath(path.corners);
            Debug.LogError($"len{len}");
            //foreach (var item in path.corners) {
            //    print(item);
            //}
        } else {
            print("not path");
        }

    }

    private void OnDestroy() {
        NavMesh.RemoveAllNavMeshData();
    }

    private Vector3 GetSimplePos(Vector3 pos) {
        if (NavMesh.SamplePosition(pos, out navMeshHit, 1, areaMask)) {
            pos = navMeshHit.position;
        }

        return pos;
    }
}
