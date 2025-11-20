using UnityEngine;

public class FlashLight : MonoBehaviour
{
    float angle;
    Vector2 target, mouse;
    
   	 private void Start()
   	 {
   	     
   	 }
   	 private void Update()
   	 {
		target = transform.position;
   	     mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
   	     angle = Mathf.Atan2(mouse.y - target.y, mouse.x - target.x) * Mathf.Rad2Deg;
   	     this.transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
  	  }
}
