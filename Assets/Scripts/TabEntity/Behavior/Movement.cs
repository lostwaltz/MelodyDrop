using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Movement : MonoBehaviour, IPause
{
    [SerializeField] private Rigidbody rb;
    
    [SerializeField] private float minForce = 6f;
    [SerializeField] private float maxForce = 9f;
    [SerializeField] private float angleSpread = 60f;
    [SerializeField] private float maxTorque = 8f;

    private void Start()
    {
        Rigidbody component = GetComponent<Rigidbody>();

        float angle = Random.Range(-angleSpread, angleSpread);
        Vector3 dir = Quaternion.Euler(0, angle, 0) * Vector3.up;

        float power = Random.Range(minForce, maxForce);
        component.AddForce(dir * power, ForceMode.Impulse);

        Vector3 torque = new Vector3(
            Random.Range(-maxTorque, maxTorque),
            Random.Range(-maxTorque, maxTorque),
            Random.Range(-maxTorque, maxTorque)
        );
        component.AddTorque(torque, ForceMode.Impulse);
    }
}
