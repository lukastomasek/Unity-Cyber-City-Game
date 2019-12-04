using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPointsController : MonoBehaviour
{
    public GameObject left_Hand_AttackPoint, right_Hand_AttackPoint;
    public GameObject left_Foot_Attack_Point, right_Foot_AttackPoint;

   
    public void Activate_Left_Hand()
    {
        left_Hand_AttackPoint.SetActive(true);
    }

   public void Deactivate_Left_Hand()
    {
        if (left_Hand_AttackPoint.activeInHierarchy)
            left_Hand_AttackPoint.SetActive(false);
    }

   public void Activate_Right_Hand()
    {
        right_Hand_AttackPoint.SetActive(true);
    }

    public void Deactivate_Right_Hand()
    {
        if (right_Hand_AttackPoint.activeInHierarchy)
            right_Hand_AttackPoint.SetActive(false);
    }
   public void Activate_Left_Foot()
    {
        left_Foot_Attack_Point.SetActive(true);
    }

    public void Deactivate_Left_Foot()
    {
        if (left_Foot_Attack_Point.activeInHierarchy)
            left_Foot_Attack_Point.SetActive(false);
    }

    public void Activate_Right_Foot()
    {
        right_Foot_AttackPoint.SetActive(true);
    }

    public void Deactivate_Right_Foot()
    {
        if (right_Foot_AttackPoint.activeInHierarchy)
            right_Foot_AttackPoint.SetActive(false);
    }

    

} // end
