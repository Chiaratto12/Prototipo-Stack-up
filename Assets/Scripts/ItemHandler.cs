using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float followSpeed;
    [SerializeField] public PileHandler pileHandler;
    [SerializeField] public float followRotation;
    [SerializeField] public Transform follow;
    [SerializeField] public bool start;

    private void Update()
    {
        StartFollowingToLastCubePosition(follow, start);
    }

    public void UpdateCubePosition(Transform followedCube, bool isFollowStart)
    {
        follow = followedCube;
        start = isFollowStart;
    }

    public void StartFollowingToLastCubePosition(Transform followedCube, bool isFollowStart)
    {
        if (isFollowStart) { 
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, followedCube.position.x, followSpeed * Time.deltaTime),
                transform.position.y,
                Mathf.Lerp(transform.position.z, followedCube.position.z, followSpeed * Time.deltaTime));
            if(transform.tag != "Pile") transform.eulerAngles = new Vector3(Mathf.Lerp(-PlayerController.instance.moveAction.action.ReadValue<Vector2>().y * 5f, followRotation, followRotation * Time.deltaTime),
                transform.eulerAngles.y,
                Mathf.Lerp(PlayerController.instance.moveAction.action.ReadValue<Vector2>().x * 5f, followRotation, followRotation * Time.deltaTime));
        }
    }
}