using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnnemiBehaviour : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] List<Transform> patrolPoints;
    Coroutine coroutActive;
    [SerializeField] Transform areaCenter;
    [SerializeField] float maxDistFromCenter;
    [SerializeField] float PlayerHitDist = 0.9f;
    IEnumerator PatrolCoroute()
    {
        for (int i = 0; i < patrolPoints.Count; i++)
        {
            agent.SetDestination(patrolPoints[i].position);
            while (Vector3.Distance(transform.position, patrolPoints[i].position)> 1) 
            {
                yield return null;
            }
            if (i == patrolPoints.Count - 1) 
            {
                i = -1;
            }
        }
       
    }

    private void Start()
    {
        coroutActive = StartCoroutine(PatrolCoroute());                                                 //active ma patrouille 
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log (other.tag);
        if (!other.transform.root.CompareTag("Player"))return;                                          //me sert a savoir quand le triger player rentrre en contacte avec le triger ennemis 
        if (coroutActive != null)                                                               
        {
            StopCoroutine(coroutActive);
            coroutActive = StartCoroutine(ChaseCorout(other.transform.root));
        }
        
    }

    IEnumerator ChaseCorout(Transform Player)
    {
        float DistFromCenter;
        float DistFromPlayer;
        do
        {
            DistFromCenter = Vector3.Distance(transform.position, areaCenter.position);                 //calcul la distrance de lennemis par raport a ma zone 
            DistFromPlayer = Vector3.Distance(transform.position, Player.position);
            if (DistFromPlayer < PlayerHitDist)
            {
                InfosPlayer infoPlayer = Player.gameObject.GetComponent<InfosPlayer>();
                infoPlayer.SetDamage(1);
                yield return new WaitForSeconds(1) ;
            }
            yield return null;
            agent.SetDestination(Player.position);
        } while (DistFromCenter < maxDistFromCenter);
          coroutActive = StartCoroutine(PatrolCoroute());
    }


   

    private void OnDrawGizmosSelected()
    {
        Handles.DrawWireArc(areaCenter.position, Vector3.up, Vector3.left, 360, maxDistFromCenter);                         //permet de visualiser la zone dans leditor
    }


}
