using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateLinearly : MonoBehaviour
{
    public Vector3 curPosition;
    public Vector3 endPosition;

    public float moveTime = 0.1f;
    private float inverseMoveTime;

    private Rigidbody2D rb2D;

    public void Start()
    {
        inverseMoveTime = 1f / moveTime;
        rb2D = GetComponent<Rigidbody2D>();
        StartCoroutine(SmoothMovement(endPosition));
    }

    protected IEnumerator SmoothMovement(Vector3 end)
    {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        yield return new WaitForSeconds(2.0f);
        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, end, inverseMoveTime * Time.deltaTime);
            transform.position = newPosition;
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }
    }

}
