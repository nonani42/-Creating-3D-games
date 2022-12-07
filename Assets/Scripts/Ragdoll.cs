using System.Collections;
using UnityEngine.AI;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;


public class Ragdoll : MonoBehaviour
{
    private Rigidbody[] Rbs;
    private Collider[] Colls;

    public float killForce = 5f;
    private Animator anim;
    private NavMeshAgent AI;
    private AICharacterControl _char;
    [SerializeField] private Transform hips;

    void Start()
    {
        anim = GetComponent<Animator>();
        AI = GetComponent<NavMeshAgent>();
        Rbs = GetComponentsInChildren<Rigidbody>();
        Colls = GetComponentsInChildren<Collider>();
        _char = GetComponent<AICharacterControl>();
        Revive();
    }

    private void Kill()
    {
        SetRagdoll(true);
        SetMain(false);
    }

    private void Revive()
    {
        if (hips)
        {
            transform.position = hips.position;
        }
        SetRagdoll(false);
        SetMain(true);
    }

    void SetRagdoll(bool active)
    {
        for (int i = 0; i < Rbs.Length; i++)
        {
            Rbs[i].isKinematic = !active;
            if (active)
            {
                Rbs[i].AddForce(Vector3.forward * killForce, ForceMode.Impulse);
            }
        }
        for (int i = 0; i < Colls.Length; i++)
        {
            Colls[i].enabled = active;
        }
    }

    void SetMain(bool active)
    {
        anim.enabled = active;
        AI.enabled = active;
        _char.enabled = active;
        Rbs[0].isKinematic = !active;
        Colls[0].enabled = active;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Kill();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Revive();
        }

    }
}
