using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject projectile;
    [SerializeField] private LayerMask whatIsGround, whatIsPlayer;
    [Space]
    [Header("Patroling")]
    [SerializeField] private Vector3 walkPoint; // Yürüyüþ noktasý
    public bool walkPointSet;// Yürüþ noktasý varmý yokmu
    [SerializeField] private float walkPointRange;
    [Space]
    [Header("Attacking")]
    [SerializeField] private float timeBetweenAttacks;
    bool alreadyAttacked;
    [Space]
    [Header("States")]
    [SerializeField] private float sightRange, attackRange; // Görüþ ve atak menzil aralýðý
    [SerializeField] private bool playerInSightRange, playerInAttackRange;  // Görüþ menzilinde mý yoksa atak menzilinde mi

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        // Oyuncu atak mý yoksa görüþ alanýnda mý kontrol edilir
        playerInSightRange = Physics.CheckSphere(this.transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(this.transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();   // Eðer yürüyüþ alanýnda deðilse yeni yürüþ noktasý oluþtur
        if (walkPointSet)
            agent.SetDestination(walkPoint);    // Eðer yürüyüþ alanýnda ise o noktaya hareket et

        Vector3 distanceToWalkPoint = this.transform.position - walkPoint;
        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {         
        agent.SetDestination(this.transform.position);
        this.transform.LookAt(player);  // Player doðru bakar

        if (!alreadyAttacked)
        {
            // Mermi atýþ ve yok oluþu
            GameObject obj = Instantiate(projectile, this.transform.position, Quaternion.identity);
            obj.transform.GetComponent<Rigidbody>().AddForce(this.transform.forward * 32f, ForceMode.Impulse);
            obj.transform.GetComponent<Rigidbody>().AddForce(this.transform.up * 2f, ForceMode.Impulse);
            DestroyObject(obj);
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void SearchWalkPoint()
    {
        // X ve Z eksenlerinde walkPointRange aralýðýnda random nokta belirle
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        
        walkPoint = new Vector3(this.transform.position.x + randomX, this.transform.position.y, this.transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -this.transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    private void DestroyObject(GameObject destroyObject)
    {
        Destroy(destroyObject,1f);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, sightRange);        
    }    
}
