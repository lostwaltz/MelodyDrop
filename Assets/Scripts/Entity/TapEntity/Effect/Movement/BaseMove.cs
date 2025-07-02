using DG.Tweening;
using UnityEngine;

public class BaseMove : EntityComponent
{
    [Header("포물선 설정")]
    public float arcHeight = 5.5f;
    public float duration = 1.0f;

    [Header("회전 연출")]
    public Vector3 randomRotation = new Vector3(0, 360, 180);

    [SerializeField] private Rigidbody rigid;

    private void Start()
    {
        InitArc();
    }

    private void InitArc()
    {
        rigid.AddForce(new Vector3(0f, arcHeight, 0f), ForceMode.Impulse);

        // 랜덤 회전도 추가
        transform.DORotate(randomRotation, duration, RotateMode.Fast);
    }
}
