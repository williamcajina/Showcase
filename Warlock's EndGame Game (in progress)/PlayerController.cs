using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UI.Image;

public class PlayerController : NetworkBehaviour
{
    public GameObject ContextMenuPrefab;

    public GameObject ProjectilePrefab;
    public Transform transformForProjectile;
    public float xUnits = 100;
    private NavMeshAgent agent;
    private Animator animator;
    private GameObject contextItem;
    private ProjectileScript currProjn = null;

    private PlayerStats playerStats;

    private ContextItemAction sprint = new ContextItemAction()
    {
        Action = () =>
        {
            playerStats.isSprinting = true;
            float currentSpeed = agent.speed; // Get the current speed
            agent.speed = 6; // Set to sprint speed
            agent.SetDestination(origin.point);
            StartCoroutine(ResetAgentSpeedAfterDelay(10, currentSpeed)); // Pass the current speed to the coroutine

            playerStats.SpendStamina(20);
        },
        Name = $"Sprint"
    };

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            var cameraFollower = Camera.main.gameObject.GetComponent<CameraScript>();
            cameraFollower.SetTarget(transform);
        }
    }

    public void StartProjectile()
    {

        currProjn.Start = true;
    }

    private void CreateContextMenu(RaycastHit hit, RaycastHit origin)
    {
        if (contextItem != null)
        {
            Destroy(contextItem);
        }

        var instantiatePosition = hit.point;
        contextItem = Instantiate(ContextMenuPrefab, instantiatePosition, Quaternion.identity);

        var contextMenu = contextItem.GetComponent<ContextMenuScript>();

        var actionList = new List<ContextItemAction>();

        var moveTo = new ContextItemAction()
        {
            Action = () => { agent.SetDestination(origin.point); },
            Name = playerStats.isSprinting ? "Sprint" : $"Run"
        };
        actionList.Add(moveTo);

        if (playerStats.Stamina >= 20 && !playerStats.isSprinting)
        {

            actionList.Add(sprint);
        }

        var shoot = new ContextItemAction()
        {
            Action = () =>
            {
                animator.SetTrigger("CastSpell");
                var projectile = Instantiate(ProjectilePrefab, transformForProjectile.position, Quaternion.identity).GetComponent<ProjectileScript>();
                projectile.StartTarget = transformForProjectile.gameObject;
                projectile.FinalTarget = origin.point;
                currProjn = projectile;

            },
            Name = "Shoot"
        };
        actionList.Add(shoot);
        contextMenu.CreateConextMenuItems(actionList, instantiatePosition, origin.point);
    }

    private void HandleInput()
    {
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            Vector3 position;
            Vector3 originalPosition;
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                position = touch.position;
            }
            else
            {
                position = Input.mousePosition;
            }

            originalPosition = position;
            Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
            Vector2 directionToCenter = (screenCenter - new Vector2(position.x, position.y)).normalized;

            position += new Vector3(directionToCenter.x * xUnits, directionToCenter.y * xUnits, 0);

            Ray ray = Camera.main.ScreenPointToRay(position);
            RaycastHit hit;
            RaycastHit originalHit;

            if (Physics.Raycast(ray, out hit) && Physics.Raycast(Camera.main.ScreenPointToRay(originalPosition), out originalHit))
            {
                string originalHitTag = originalHit.collider.gameObject.tag;
                if (originalHitTag == "ContextItem")
                {
                    originalHit.collider.gameObject.GetComponent<ContextMenuItemScript>().ExecuteAction();
                    Debug.Log("Execution");
                    Destroy(contextItem);
                    contextItem = null;
                }
                else
                {
                    CreateContextMenu(hit, originalHit);
                }
            }
        }
    }

    private IEnumerator ResetAgentSpeedAfterDelay(float delay, float originalSpeed)
    {
        yield return new WaitForSeconds(delay);
        agent.speed = originalSpeed;
        playerStats.isSprinting = false;
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {

        HandleInput();
        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);
        Vector3 directionToSteeringTarget = (agent.steeringTarget - transform.position).normalized;
        float angle = Vector3.SignedAngle(transform.forward, directionToSteeringTarget, Vector3.up);
        float normalizedAngle = angle / 180.0f;
        animator.SetFloat("Angle", normalizedAngle);
        animator.SetBool("IsMoving", speed > 0.05);
    }
}