using UnityEngine;
using UnityEngine.AI;

public class ChefMover : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    Ray ray;
    RaycastHit hit;
    NavMeshAgent agent;
    int rotationSpeed = 20;
    Chef chef;
    IInteractable lastInteractable = null;
    bool canInteract = true; //Changes on mouse click and interact

    private void Start()
    {
        chef = GetComponent<Chef>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        SendRaycast();
        LookInteractable(hit);
    }


    private void SendRaycast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100, layerMask))
            {
                canInteract = true;
                GoToInteract(hit);
            }
        }
    }

    private void GoToInteract(RaycastHit hit)
    {
        agent.destination = FindChildByName(hit).position;
        agent.updateRotation = true;
    }

    private Transform FindChildByName(RaycastHit hit)
    {
        foreach (Transform transform in hit.transform) 
        {
            if (transform.name == "Stand Point")
            {
                return transform;
            }
        }
        return null;
    }

    private void LookInteractable(RaycastHit hit)
    {
        if (hit.transform == null) return;

        Vector3 standPointPosition = FindChildByName(hit).position;
        float standPointDistance = Vector3.Distance(standPointPosition, transform.position);

        float distance = Vector3.Distance(hit.transform.position, transform.position);
        Vector3 direction = hit.transform.position - transform.position;
        direction.y = 0;

        if (distance < 3f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 3f)
            {
                transform.rotation = targetRotation;

                if (standPointDistance < 1.5f)
                {
                    Interact();
                }
            }
        }
    }

    private void Interact()
    {
        IInteractable interactable = hit.collider.GetComponent<IInteractable>();
        
        if (interactable != null && canInteract) // && interactable != lastInteractable)
        {
            chef.InteractWith(interactable);
            canInteract = false;
        }

        lastInteractable = interactable;
    }
}
