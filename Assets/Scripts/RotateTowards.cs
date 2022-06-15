using UnityEngine;

public class RotateTowards : MonoBehaviour
{
    public string targetTag = "Player";
    public float offset = 90;

    public bool isArm;

    void Update()
    {
        Vector3 target = GameObject.FindGameObjectWithTag(targetTag).transform.position;
        transform.right = target - transform.position;

        if (isArm) 
        {
            if (transform.position.x < target.x) 
            {
                transform.Rotate(0, 0, offset);
            }
            else if (transform.position.x > target.x) 
            {
                transform.Rotate(0, 0, offset+180);
            }
        }
        else {
            transform.Rotate(0, 0, offset);
        }
    }
}
