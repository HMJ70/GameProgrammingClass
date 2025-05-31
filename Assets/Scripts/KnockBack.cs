using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    private Rigidbody rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody>();
    }

    public void knockback(Transform playertransform, float knockbackforce)
    {
        Vector2 direction = (transform.position - playertransform.position).normalized;
        rb2d.velocity = direction * knockbackforce;
        Debug.Log("kb");
    }
}
