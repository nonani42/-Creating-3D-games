using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKAnimation : MonoBehaviour
{
    [SerializeField] private Transform handObjRight;
    [SerializeField] private Transform handObjLeft;
    [SerializeField] private Transform lookObj;
    [SerializeField] private Animator animGO;
    [SerializeField] private float rightHandWeight;
    [SerializeField] private float leftHandWeight;
    [SerializeField] private float rightFootWeight;
    [SerializeField] private float leftFootWeight;

    [SerializeField] private float lookDistance;

    public LayerMask Mask;

    public Transform rightLowerLeg;
    public Transform rightFoot;
    public Vector3 rightFootPos;
    public Quaternion rightFootRot;

    private int rightHash;

    public Transform leftLowerLeg;
    public Transform leftFoot;
    public Vector3 leftFootPos;
    public Quaternion leftFootRot;

    private int leftHash;



    void Start()
    {
        animGO = GetComponent<Animator>();
        rightHash = Animator.StringToHash("RightFoot");
        leftHash = Animator.StringToHash("LeftFoot");

        rightLowerLeg = animGO.GetBoneTransform(HumanBodyBones.RightLowerLeg);
        rightFoot = animGO.GetBoneTransform(HumanBodyBones.RightFoot);

        leftLowerLeg = animGO.GetBoneTransform(HumanBodyBones.LeftLowerLeg);
        leftFoot = animGO.GetBoneTransform(HumanBodyBones.LeftFoot);

    }

    private void OnAnimatorIK(int layerIndex) //вызывается автоматически каждый кадр обновления аниматора
    {
        RaycastHit hit;

        rightFootWeight = animGO.GetFloat(rightHash);
        leftFootWeight = animGO.GetFloat(leftHash);

        if(Physics.Raycast(rightLowerLeg.position, Vector3.down, out hit, 1.2f, Mask))
        {
            rightFootPos = Vector3.Lerp(rightFoot.position, hit.point + Vector3.up * 0 / 3f, Time.deltaTime * 10f);
            rightFootRot = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        if (Physics.Raycast(leftLowerLeg.position, Vector3.down, out hit, 1.2f, Mask))
        {
            leftFootPos = Vector3.Lerp(leftFoot.position, hit.point + Vector3.up * 0 / 3f, Time.deltaTime * 10f);
            leftFootRot = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }


        animGO.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeight);
        animGO.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandWeight);
        animGO.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandWeight);
        animGO.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandWeight);

        animGO.SetLookAtWeight(1f);

        animGO.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightFootWeight);
        animGO.SetIKRotationWeight(AvatarIKGoal.RightFoot, rightFootWeight);
        animGO.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftFootWeight);
        animGO.SetIKRotationWeight(AvatarIKGoal.LeftFoot, leftFootWeight);

        animGO.SetIKPosition(AvatarIKGoal.RightFoot, rightFootPos);
        animGO.SetIKRotation(AvatarIKGoal.RightFoot, rightFootRot);
        animGO.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootPos);
        animGO.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootRot);

        if (handObjRight)
        {
            animGO.SetIKPosition(AvatarIKGoal.RightHand, handObjRight.position);
            animGO.SetIKRotation(AvatarIKGoal.RightHand, handObjRight.rotation);

            animGO.SetIKPosition(AvatarIKGoal.LeftHand, handObjLeft.position);
            animGO.SetIKRotation(AvatarIKGoal.LeftHand, handObjLeft.rotation);
        }

        if (lookObj && Vector3.Distance(lookObj.position, gameObject.transform.position) <= lookDistance)
        {
            animGO.SetLookAtPosition(lookObj.position);
        }
    }
}
