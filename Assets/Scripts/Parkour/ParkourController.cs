using UnityEngine;
using System.Collections.Generic; // Required for List<>
using System.Collections; // Required for IEnumerator (coroutines)

public class ParkourController : MonoBehaviour
{
    public EnvironmentChecker environmentChecker;

    bool playerInAction;
    public Animator animator;
    public PlayerController playerScript;
    [Header("Parkour Action Area")]
    public List<NewParkourAction> newParkourAction;
     void Update()
{
    if (Input.GetButton("Jump") && !playerInAction)
    {
        var hitData = environmentChecker.CheckObstacle();

        if (hitData.hitFound)
        {
            List<NewParkourAction> validActions = new List<NewParkourAction>();

            foreach (var action in newParkourAction)
            {
                if (action.CheckIfAvailable(hitData, transform))
                {
                    validActions.Add(action);
                }
            }

            if (validActions.Count > 0)
            {
                // Choose a random valid action
                NewParkourAction chosenAction = validActions[Random.Range(0, validActions.Count)];
                StartCoroutine(PerformParkourAction(chosenAction));
            }
        }
    }
}



     IEnumerator PerformParkourAction(NewParkourAction action){
        playerInAction = true;
        playerScript.SetControl(false);

        animator.CrossFade(action.AnimationName,0.2f);
        yield return null;

        var animationState = animator.GetNextAnimatorStateInfo(0);
        if (!animationState.IsName(action.AnimationName))
        {
            Debug.Log("Wrong Animation Name");
        }

        float timerCounter = 0;
        while (timerCounter<=animationState.length)
        {
            timerCounter+= Time.deltaTime;
            if (action.LookAtObstacle)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation,action.RequiredRotation, playerScript.rotSpeed * Time.deltaTime);
            }
            if (action.AllowTargetMatchning)
            {
                CompareTarget(action);   
            }
            yield return null; 
        }      

        yield return new WaitForSeconds(action.ParkourActionDelay);  
        playerScript.SetControl(true);

        playerInAction = false;
     }


     void CompareTarget(NewParkourAction action){
        animator.MatchTarget(action.ComparePosition,transform.rotation, action.CompareBodyPart, new MatchTargetWeightMask(action.ComparePositionWeight,0),action.CompareStartTime,action.CompareEndTime);
     }
}
