/*******************************************
* Description
*
*
*******************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : SmallMapObjectAI {

    // ------ Public Variables ------
    public float vision;
    public float distance;
    public float radius;
    public float jitter;
    
    // ------ Shared Variables ------
    
    
    // ------ Private Variables ------
    private Vector2 wanderTarget;
    
    // ------ Required Components ------
    
    // ------ Public Functions ------
    public ZombieAI(float v, float d, float r, float j){
        wanderTarget = Vector2.zero;
        vision = v;
        distance = d;
        radius = r;
        jitter = j;
    }

    public override Vector2 GetDirection(Transform obj, Transform player){

        Rigidbody2D rb2d = obj.GetComponent<Rigidbody2D>();
        Vector2 vel = rb2d.velocity;
        if(vel == Vector2.zero)
            vel = Random.onUnitSphere;

        if((obj.position - player.position).magnitude <= vision){
            return (player.position - obj.position).normalized;
        }else{
            wanderTarget += Random.insideUnitCircle * jitter;
            wanderTarget.Normalize();
            wanderTarget *= radius;

            Vector2 localTarget = wanderTarget + new Vector2(0, distance);
            Vector2 worldTarget = obj.TransformPoint(localTarget);

            return (worldTarget - (Vector2)obj.position).normalized;
        }
    }
    
    // ------ Private Functions ------
    
}
