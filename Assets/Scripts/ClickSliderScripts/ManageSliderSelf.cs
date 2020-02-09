using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageSliderSelf : MonoBehaviour
{

    float worldSpriteWidth;
    GameObject sliderChild;
    public Vector2 endPosition;
    public float inTime;

    public void Start()
    {
        List<Transform> others = new List<Transform>();
        foreach (Transform child in transform)
        {
            if (child.name == "SliderSprite")
            {
                worldSpriteWidth = child.gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
                sliderChild = child.gameObject;
            }

            else
            {
                others.Add(child);
            }
        }


        sliderChild.transform.localScale = (new Vector3(endPosition.x, endPosition.y) - new Vector3(transform.position.x, 0)) / (worldSpriteWidth);
        // sliderChild.transform.position = sliderChild.transform.position + ((new Vector3(endPosition.x, endPosition.y) - transform.position) / 2.0f);
    }

    protected IEnumerator SmoothMovement(Vector3 end, float inverseMoveTime)
    {

        AnimateLinearly[] parts = GetComponentsInChildren<AnimateLinearly>();
        foreach (AnimateLinearly part in parts)
        {

            inverseMoveTime = 1f / inTime;
            StartCoroutine(SmoothMovement(endPosition, inverseMoveTime));
            Debug.Log("part name: " + part.name);
            part.endPosition = endPosition;
            part.moveTime = inTime;
        }


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
