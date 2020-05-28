using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PNJStates : MonoBehaviour
{
    [System.Serializable]
    public class BoolsInAnimator
    {
        public string boolName;
        public bool state;
    }
    public List<BoolsInAnimator> boolList = new List<BoolsInAnimator>();

    Animator animator;
    int actualClip;
    bool canChange = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(StartTimer());
    }

    void CheckState()
    {
        //actualClip = animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
        //boolList.Clear();
        for(int i = 0; i < animator.parameterCount; i++)
        {
            /*if(animator.parameters[i].type == AnimatorControllerParameterType.Bool)
            {
                //animator.parameters[i].defaultBool = boolList.Find(x => x.boolName == animator.parameters[i].name).state;
                // && boolList.Exists(x => x.boolName == animator.parameters[i].name)
                boolList.Add(new BoolsInAnimator{boolName = animator.parameters[i].name, state = animator.parameters[i].defaultBool});
            }*/
            boolList.Find(x => x.boolName == animator.parameters[i].name).state = animator.GetBool(boolList[i].boolName);
        }
    }

    void OnEnable()
    {
        if(canChange)
        {
            foreach(BoolsInAnimator bia in boolList)
            {
                animator.SetBool(bia.boolName, bia.state);
            }
        }
        //animator.Play(actualClip);
    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(0.01f);
        canChange = true;
    }
}
