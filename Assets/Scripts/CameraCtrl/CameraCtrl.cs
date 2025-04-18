using UnityEngine;
using System.Collections;

public class CameraCtrl : MonoBehaviour {// 카메라가 따라다닐 대상
   public Transform target;

   // 카메라가 대상을 따라가는 속도
   public float smooth = 0.1f;

   // 카메라 위치 조정
   public Vector3 adjustCamPos;

   // 카메라 경계 설정
   public Vector2 minCamLimit;
   public Vector2 maxCamLimit;

   // Update is called once per frame
   void Update()
   {
      if (target == null) return;

      // 카메라의 대상 위치간의 보간
      Vector3 pos = Vector3.Lerp(transform.position, target.position, smooth);

      // 대상과 한계 위치에 따른 카메라 위치
      transform.position = new Vector3(
         Mathf.Clamp(pos.x, minCamLimit.x, maxCamLimit.x) + adjustCamPos.x,
         Mathf.Clamp(pos.y, minCamLimit.y, maxCamLimit.y) + adjustCamPos.y,
         -10f + adjustCamPos.z);
   }
}