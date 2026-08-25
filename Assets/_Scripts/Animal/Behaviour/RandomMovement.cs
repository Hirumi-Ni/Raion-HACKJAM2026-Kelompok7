using System.Collections;
using UnityEngine;
using UnityEngine.AI; //important

//if you use this code you are contractually obligated to like the YT video
public class RandomMovement : MonoBehaviour //don't forget to change the script name if you haven't
{
    [SerializeField] private float maxWaitingTime;
    [SerializeField] private float range; //radius of sphere

    private bool isWaiting = false;
    private NavMeshAgent agent;
    private Transform centrePoint; //centre of the area the agent wants to move around in
    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        centrePoint = agent.transform;
    }

    private void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        if (isWaiting) return;

        if (agent.remainingDistance <= agent.stoppingDistance) //done with path
        {
            StartCoroutine(WaitThenSetDestination());
        }

    }

    private IEnumerator WaitThenSetDestination()
    {
        isWaiting = true;
        float duration = Random.Range(0, maxWaitingTime);
        yield return new WaitForSeconds(duration);

        Vector3 point;
        if (RandomPoint(centrePoint.position, range, out point))
        {
            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
            agent.SetDestination(point);
        }

        isWaiting = false;
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}