using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeMovement : MonoBehaviour
{
    private Rigidbody rb;
    private float horizontalInput;
    private float verticalInput;
    public List<Transform> players = new List<Transform>();
    public Transform holder;
    private const float spaces = 0.9f;
    [Header("Movement Values")]

    public float velocity;
    public float quaternion;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0f, -0.5f, 0f);
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void LateUpdate()
    {
        InputActions();
        FollowMainTarget();
    }

    public void InputActions()
    {
        Quaternion rot = Quaternion.Euler(0f, horizontalInput * quaternion * Time.deltaTime, 0f);
        rb.MoveRotation(rot * rb.rotation);

        Vector3 pos = transform.forward * verticalInput * velocity;
        Vector3 posToFollow = new Vector3(pos.x, 0f, pos.z);
        rb.velocity = posToFollow;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cube"))
        {
            AddPlayerToList(collision.gameObject);
        }
    }

    private void AddPlayerToList(GameObject player)
    {
        players.Add(player.transform);
    }

    private void FollowMainTarget()
    {
        if(players.Count > 0)
        {
            for(int i = 0; i < players.Count; i++)
            {
                Transform mainTarget = i == 0 ? holder : players[i - 1].transform;
                Vector3 pos = new Vector3(mainTarget.position.x, 0.5f, mainTarget.position.z - spaces);
                players[i].transform.position = Vector3.Lerp(players[i].transform.position, pos, Time.deltaTime * 5f);
                Vector3 direction = mainTarget.transform.position - players[i].transform.position;
                if(direction != Vector3.zero)
                {
                    Quaternion rot = Quaternion.LookRotation(direction);
                    players[i].rotation = Quaternion.Slerp(players[i].rotation, rot, Time.deltaTime * quaternion);
                }
            }
        }
    }
}