using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ChickenController : MonoBehaviour
{
    // Start is called before the first frame update

    public List<Transform> patrollingPoint = new List<Transform>();
    Rigidbody chickbody;
    // The target marker.
     Transform target;
  
    // Angular speed in radians per sec.
    public float walkspeed = 0.2f;
    public float Runspeed = 2.0f;
    public int counter = 0;
    public int agentcounter = 0;
    public float waitingTime = 5;
    public Animation chickenAnimation;
    [HideInInspector]public NavMeshAgent chickAgent;
    public List<Transform> agentPoint = new List<Transform>();
    public bool _isAgentActive = false;
    public Transform dog;
    void Start()
    {
        chickbody = this.GetComponent<Rigidbody>();
        chickAgent = GetComponent<NavMeshAgent>();
        target = patrollingPoint[1];
      
        counter = 1;
     //   chickAgent.destination = agentPoint.position;
    }

    // Update is called once per frame
   
    
   private void FixedUpdate()
    {
        if (_isAgentActive)
        {
           
            chickAgent.speed = Runspeed;
        
            chickAgent.destination = target.position;
            chickenAnimation.Play("Arm_cock|Run");
            return;
        }
     
        if (waitingTime <= 0)
        {
            chickAgent.destination = target.position;
            chickAgent.speed = walkspeed;
            chickenAnimation.Play("Arm_cock|Walk_fast");
        }
        else
        {
            waitingTime -= 0.1f*Time.deltaTime;
            chickenAnimation.Play("Arm_cock|eat");
            //chickenAnimation.CrossFade("Arm_cock|dead");
        }
        if(Vector3.Distance(transform.position, dog.position) <= 5f && !_isAgentActive) {

            _isAgentActive = true;
            target = agentPoint[0];
        }
       
    }

   
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "points" && !_isAgentActive)
        {
            waitingTime =1;
            if (counter == 0)
            {
               
                target = patrollingPoint[1];
                counter += 1;
            }
            else if(counter == 1){
               
                target = patrollingPoint[2];
                counter += 1;
            }
            else if (counter == 2)
            {
                
                target = patrollingPoint[3];
                counter += 1;
            }
            else if (counter == 3)
            {
               
                target = patrollingPoint[0];
                counter = 0;
            }
        }

        if(other.tag == "agent")
        {
            if(agentcounter < agentPoint.Count - 1)
            {
                agentcounter += 1;
            }
            else
            {
                agentcounter = 0;
            }

            target = agentPoint[agentcounter];
        }

        else if(other.tag == "dog")
        {
            GameManager.instance._iswinner = true;
            GameManager.instance.gameoverFtn();
            this.GetComponent<BoxCollider>().enabled = false;
            chickenAnimation.CrossFade("Arm_cock|dead");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "jump")
        {
            chickbody.AddForce(new Vector3(0,4,0),ForceMode.Impulse);
        }
    }
}
