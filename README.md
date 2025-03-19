# **Parkour System in Unity**  
A dynamic parkour system for Unity, allowing the player to **vault over obstacles** with randomized animations. The system detects obstacles and selects a suitable parkour action based on height and other factors.  

![Parkour GIF](https://i.imgur.com/oWpaBbN.gif)  

## **Features**  
✅ Randomized parkour actions for obstacles  
✅ Uses `ScriptableObject` for easy animation configuration  
✅ Smooth animations with `MatchTarget` for better alignment  
✅ Supports different obstacle heights  

## **How It Works**  
🔹 **Environment Detection**: The `EnvironmentChecker` detects obstacles in front of the player.  
🔹 **Valid Parkour Actions**: The system checks if the obstacle height matches any available `NewParkourAction`.  
🔹 **Random Action Selection**: If multiple actions are valid, one is picked at random.  
🔹 **Animation Execution**: The selected action is played, smoothly aligning the player with the obstacle.  

## **Setup Instructions**  
🔹 Attach `ParkourController` to your player object.  
🔹 Assign an `EnvironmentChecker` component to detect obstacles.  
🔹 Create parkour actions via **Assets → Create → Parkour Menu → New Parkour Action**.  
🔹 Set animations, heights, and other properties in the created **ScriptableObject**.  
🔹 Press **Jump** to trigger parkour over obstacles!  

## **Code Snippet**  
This snippet ensures a random animation is chosen from the available ones:  

```csharp
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
                NewParkourAction chosenAction = validActions[Random.Range(0, validActions.Count)];
                StartCoroutine(PerformParkourAction(chosenAction));
            }
        }
    }
}
