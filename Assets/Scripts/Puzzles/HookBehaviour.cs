using System.Collections.Generic;
using UnityEngine;

public class HookBehaviour : MonoBehaviour
{
    public int index;
    private int maxIndex = 5;

    public bool canMove;

    [SerializeField] Animator anim;

    public List<HookBehaviour> otherHooks = new List<HookBehaviour>();

    private void Awake()
    {
        canMove = true;
    }

    public void Mover()
    {
        if (canMove)
        {

            index++;

            if (index > maxIndex) index = 1;

            anim.SetInteger("State", index);

            canMove = false;
        }
    }

    public void AnimatorChanger()
    {
        canMove = true;

        if (index == 5)
        {
            foreach (var item in otherHooks)
            {
                item.canMove = true;
            }
        }
    }
}