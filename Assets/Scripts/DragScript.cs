using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
public class DragScript : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float onRoadSpeed = 25f;
    [SerializeField] float rotationSpeed = 500f;
    [SerializeField] bool gift;
    bool isMoving;
    bool waypointFound;
    bool hasEnteredTrigger;
    float distanceTravelled;
    Quaternion initialRotation;
    public Vector3 moveDirection;
    PathCreator pathCreator;
    ParticleSystem particles;
    Animator animator;
    Rigidbody rb;

    private void Awake()
    {
        particles = GetComponentInChildren<ParticleSystem>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        pathCreator = WaypointsHolder.instance.pathCreator;
        initialRotation = transform.rotation;
        if(gift)
            Instantiate(GameController.instance.levels.carGiftCanvas, gameObject.transform);
    }

    void UnfreezeGlobalDirection()
    {
        Vector3 globalForward = transform.rotation * Vector3.forward;

        if (Mathf.Abs(globalForward.x) > Mathf.Abs(globalForward.y) && Mathf.Abs(globalForward.x) > Mathf.Abs(globalForward.z))
            rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
        else if (Mathf.Abs(globalForward.y) > Mathf.Abs(globalForward.x) && Mathf.Abs(globalForward.y) > Mathf.Abs(globalForward.z))
            rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
        else
            rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
    }


    private void Update()
    {
        if (Input.touchCount > 0)
        {
            GetSelectedCar();
        }

        if (isMoving)
        {
            MoveCar();
        }

        if (hasEnteredTrigger)
        {
            RotateCarTowardsRoad();
        }
    }


    Vector2 touchStartPosition;
    Vector2 touchEndPosition;
    Vector2 drag;
    void GetSelectedCar()
    {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            touchStartPosition = touch.position;
        }
        else if (touch.phase == TouchPhase.Moved)
        {
            touchEndPosition = touch.position;
            drag = touchEndPosition - touchStartPosition;
            if (drag.magnitude > 0.2f)
            {
                drag.Normalize();

                Ray ray = Camera.main.ScreenPointToRay(touchStartPosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {

                    if (hit.transform.gameObject == gameObject)
                    {
                        float dotProduct = Vector3.Dot(transform.forward.normalized, new Vector3(drag.x, 0f, drag.y).normalized);
                        if (dotProduct > 0.6f || dotProduct < -0.6f)
                        { 
                           dotProduct = Mathf.Sign(dotProduct);
                           moveDirection = new Vector3(0f, 0f, dotProduct);
                            rb.isKinematic = false;
                           isMoving = true;
                        } 

                    }
                }
            }
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            //isMoving = true;
        }

    }

    void MoveCar()
    {
        if (waypointFound)
        {
            if (!hasEnteredTrigger)
            {
                distanceTravelled += moveSpeed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
            }
        }
        else
        {
            //transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            UnfreezeGlobalDirection();
            rb.velocity = transform.forward * moveDirection.z * moveSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!waypointFound)
        {
            hasEnteredTrigger = true;
            transform.position = pathCreator.path.GetClosestPointOnPath(transform.position);
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
            waypointFound = true;
            if (gift)
            {
                //BonusRewardManager.instance.UpdateBarProgress(0.1f);
                PlayerPrefs.SetFloat("bar progress", PlayerPrefs.GetFloat("bar progress") + 0.1f);
                Destroy(GetComponentInChildren<Canvas>().transform.gameObject);
            }
        }
        if (other.gameObject.CompareTag("Destroy"))
            Destroy(gameObject, 2f);
    }

    void RotateCarTowardsRoad()
    {
        Quaternion targetRotation = initialRotation * Quaternion.Euler(0f, 90f * moveDirection.z, 0f);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
        {
            hasEnteredTrigger = false;
            moveSpeed = onRoadSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            if (isMoving && !waypointFound && !collision.gameObject.GetComponent<DragScript>().waypointFound)
            {
                StartCoroutine(addForce());
                UnfreezeGlobalDirection();
                particles.gameObject.transform.position = collision.GetContact(0).point;
                particles.Play();
            }
            else if (!isMoving)
            {
                Vector3 collisionNormal = collision.contacts[0].normal;

                if (collisionNormal == transform.forward)
                {
                    print("Collision from the back");
                    animator.SetTrigger("back-hit");
                }
                else if (collisionNormal == -transform.forward)
                {
                    print("Collision from the front");
                    animator.SetTrigger("front-hit");
                }
                else if (collisionNormal == transform.right)
                {
                    print("Collision from the left");
                    animator.SetTrigger("left-hit");
                }

                else if (collisionNormal == -transform.right)
                {
                    print("Collision from the right");
                    animator.SetTrigger("right-hit");
                }
            }
        }
        else if(!waypointFound) //called when hitting the obstacles and car has not yet found the waypoint
        {
            StartCoroutine(addForce());
        }
    }


    IEnumerator addForce()
    {
        //rb.AddForce(-transform.forward * moveDirection.z * collisionForce * Time.deltaTime, ForceMode.Impulse);
        isMoving = false;
        yield return new WaitForSeconds(0.2f);
        rb.isKinematic = true;
        rb.constraints |= RigidbodyConstraints.FreezePosition;
    }


}
