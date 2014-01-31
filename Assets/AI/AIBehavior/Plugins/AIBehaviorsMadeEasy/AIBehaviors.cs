using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using System.Reflection;
#endif


using Random = UnityEngine.Random;


[RequireComponent(typeof(AIBehaviorsAnimationStates))]
public class AIBehaviors : MonoBehaviour
{
	private GameObject[] playerGameObjects = new GameObject[0];
	private Transform[] playerTransforms = new Transform[0];

	/// Is this AI active (Read Only)?
	public bool isActive { get; private set; }

	/// <summary>
	/// Is this AI in defensive mode?
	/// </summary>
	public bool isDefending = false;
	public float damageMultiplier = 1.0f;

	/// <summary>
	/// The number of possible states this AI has.
	/// </summary>
	public int stateCount
	{
		get
		{
			return states.Length;
		}
	}

	Transform thisTFM;
	Vector3 thisPos;
	NavMeshAgent navMeshAgent;

	/// <summary>
	/// This is the state the AI is in once the game is playing.
	/// </summary>
	public BaseState initialState;

	/// <summary>
	/// This is the state the AI is currently in (Read Only).
	/// </summary>
	public BaseState currentState { get; private set; }
	public BaseState previousState { get; private set; }

	/// <summary>
	/// An array of all the states that belong to this AI.
	/// </summary>
	public BaseState[] states = new BaseState[0];

	// === Player Properties === //

	/// <summary>
	/// A list of tags that the AI will consider to be players.
	/// </summary>
	public string[] playerTags = new string[1] { "Player" };

	/// <summary>
	/// The rate at which the AI looks if new players have spawned (a value of zero means it will only check once)
	/// </summary>
	public float checkForNewPlayersInterval = 0.0f;

	// === General Properties === //

	public LayerMask raycastLayers = -1;
	public bool useSightFalloff = true;
	public float sightFOV = 180.0f;
	public float sightDistance = 50.0f;
	public Vector3 eyePosition = new Vector3(0.0f, 1.5f, 0.0f);
	public float health = 100.0f;

	public GameObject statesGameObject = null;

	// === Animation Callback Info === //

	public Component animationCallbackComponent = null;
	public string animationCallbackMethodName = "";
	public AIBehaviorsAnimationStates animationStates;

	// === Delegates === //

	public delegate void StateChangedDelegate(BaseState newState, BaseState previousState);
	public StateChangedDelegate onStateChanged = null;

	public delegate void AnimationCallbackDelegate(AIBehaviorsAnimationState animationState);
	public AnimationCallbackDelegate onPlayAnimation = null;

	public delegate void ExternalMoveDelegate(Vector3 targetPoint, float targetSpeed, float rotationSpeed);
	public ExternalMoveDelegate externalMove = null;

	// === Targetting and Rotation === //

	public Vector3 currentDestination { get; private set; }
	Transform lastKnownTarget;
	Vector3 targetRotationPoint;
	float rotationSpeed;


	void Awake()
	{
		animationStates = GetComponent<AIBehaviorsAnimationStates>();

		thisTFM = transform;
		navMeshAgent = GetComponent<NavMeshAgent>();

		currentDestination = transform.position;

		isActive = true;
	}


	void Start()
	{
		ChangeActiveState(initialState);

		if ( checkForNewPlayersInterval > 0 )
		{
			InvokeRepeating("CheckForPlayers", Random.value * checkForNewPlayersInterval, checkForNewPlayersInterval);
		}
		else
		{
			CheckForPlayers();
		}
	}


	/// <summary>
	/// Sets the array of tags this AI will look for that it considers to be enemies
	/// </summary>
	public void SetPlayerTags(string[] tags)
	{
		playerTags = tags;
		CheckForPlayers();
	}


	/// <summary>
	/// This method causes the AI to update it's list of players that it considers in it's behaviors
	/// The GameObjects it finds will be based on the playerTags property
	/// </summary>
	public void CheckForPlayers()
	{
		Dictionary<GameObject, Transform> playersList = new Dictionary<GameObject, Transform>();
		int i = 0;

		foreach ( string tag in playerTags )
		{
			GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);

			foreach ( GameObject go in gos )
			{
				playersList[go] = go.transform;
			}
		}

		playerGameObjects = new GameObject[playersList.Count];
		playerTransforms = new Transform[playersList.Count];

		foreach ( GameObject key in playersList.Keys )
		{
			playerGameObjects[i] = key;
			playerTransforms[i] = playersList[key];
			i++;
		}
	}


	/// <summary>
	/// Returns an array of the Player GameObjects it has found using the CheckForPlayers method
	/// </summary>
	public GameObject[] GetPlayerGameObjects()
	{
		return playerGameObjects;
	}


	/// <summary>
	/// Returns an array of the Player Transforms it has found using the CheckForPlayers method
	/// </summary>
	public Transform[] GetPlayerTransforms()
	{
		return playerTransforms;
	}


	void Update()
	{
		if ( isActive && Time.timeScale > 0 )
		{
			if ( currentState.RotatesTowardTarget() )
			{
				RotateAgent();
			}

			thisPos = thisTFM.position;

			// If the state remained the same, do the action
			if ( currentState.HandleReason(this) )
			{
				currentState.HandleAction(this);
			}
		}
	}


	/// <summary>
	/// Sets whether this AI is actively evaluating its' behaviors
	/// </summary>
	public void SetActive(bool isActive)
	{
		this.isActive = isActive;
	}


	/// <summary>
	/// Tells the AI that it got hit and it will subtract the amount of health passed from its' total health
	/// </summary>
	public void GotHit(float damage)
	{
		// We don't want to enable this state if the FSM is dead
		GotHitState gotHitState = GetState<GotHitState>();

		if ( gotHitState.CanGetHit(this) )
		{
			ChangeActiveState(gotHitState);
			SubtractHealthValue(damage * damageMultiplier);
		}
	}


	// === GetStates === //

	public BaseState[] GetAllStates()
	{
		return states;
	}


	public BaseState GetStateByIndex(int stateIndex)
	{
		return states[stateIndex];
	}


	public BaseState GetStateByName(string stateName)
	{
		foreach ( BaseState state in states )
		{
			if ( state.name.Equals(stateName) )
			{
				return state;
			}
		}

		return null;
	}


	public T GetState<T> () where T : BaseState
	{
		foreach ( BaseState state in states )
		{
			if ( state is T )
			{
				return state as T;
			}
		}

		return null;
	}


	public BaseState GetState(System.Type type)
	{
		foreach ( BaseState state in states )
		{
			if ( state.GetType() == type )
			{
				return state;
			}
		}

		return null;
	}


	// === GetStates === //

	public T[] GetStates<T> () where T : BaseState
	{
		List<T> stateList = new List<T>();

		foreach ( BaseState state in states )
		{
			if ( state is T )
			{
				stateList.Add(state as T);
			}
		}

		return stateList.ToArray();
	}


	public BaseState[] GetStates(System.Type type)
	{
		List<BaseState> stateList = new List<BaseState>();

		foreach ( BaseState state in states )
		{
			if ( state.GetType() == type )
			{
				stateList.Add(state);
			}
		}

		return stateList.ToArray();
	}


	// === Replace States === //

	public void ReplaceAllStates(BaseState[] newStates)
	{
		states = newStates;
	}


	public void ReplaceStateAtIndex(BaseState newState, int index)
	{
		states[index] = newState;
	}


	// === Change Current Active State === //

	public void ChangeActiveStateByName(string stateName)
	{
		foreach ( BaseState state in states )
		{
			if ( state.name.Equals(stateName) )
			{
				ChangeActiveState(state);
				return;
			}
		}
	}


	public void ChangeActiveStateByIndex(int index)
	{
		ChangeActiveState(states[index]);
	}


	public void ChangeActiveState(BaseState newState)
	{
		previousState = currentState;

		if ( previousState != null )
		{
			previousState.EndState(this);
		}

		if ( newState != null )
		{
			currentState = newState;
			newState.InitState(this);
		}

		if ( onStateChanged != null )
		{
			onStateChanged(newState, previousState);
		}
	}


	[System.Obsolete("ChangeState is obsolete, use ChangeActiveState instead.")]
	public void ChangeState(BaseState newState)
	{
		ChangeActiveState(newState);
	}


	// TODO Deprecate this and use the current states properties
	public void MoveAgent(Transform target, float targetSpeed, float rotationSpeed)
	{
		if ( target != null )
		{
			RotateAgent(target, rotationSpeed);
		}

		MoveAgent(target.position, targetSpeed, rotationSpeed);
	}


	// TODO Deprecate this and use the current states properties
	public void MoveAgent(Vector3 targetPoint, float targetSpeed, float rotationSpeed)
	{
		Vector3 pos = thisTFM.position;
		bool isNavAgent = navMeshAgent != null;

		if ( !isNavAgent )
		{
			targetPoint.y = pos.y;
		}

		currentDestination = targetPoint;
		targetRotationPoint = targetPoint;
		this.rotationSpeed = rotationSpeed;

		if ( isNavAgent )
		{
			navMeshAgent.speed = targetSpeed;
			navMeshAgent.angularSpeed = rotationSpeed;
			navMeshAgent.destination = targetPoint;
		}

		if ( externalMove != null )
		{
			externalMove(targetPoint, targetSpeed, rotationSpeed);
		}
	}


	// TODO Deprecate this and refactor to a better name
	public void RotateAgent(Transform target, float rotationSpeed)
	{
		targetRotationPoint = target.position;
		this.lastKnownTarget = target;
		this.rotationSpeed = rotationSpeed;
	}


	private void RotateAgent()
	{
		bool isNavAgent = navMeshAgent != null;
		bool updateRot = false;

		Vector3 pos = thisTFM.position;
		Quaternion curRotation = thisTFM.rotation;
		Vector3 sightPosition = thisTFM.TransformPoint(eyePosition);
		Vector3 targetPos;

		if ( lastKnownTarget != null )
		{
			targetPos = lastKnownTarget.position;

			if ( Physics.Linecast(sightPosition, targetPos, raycastLayers) )
				lastKnownTarget = null;
			else
				targetRotationPoint = targetPos;
		}

		targetRotationPoint.y = pos.y;
		thisTFM.LookAt(targetRotationPoint);

		if ( isNavAgent )
		{
			updateRot = navMeshAgent.updateRotation;
			navMeshAgent.updateRotation = false;
		}

		if ( rotationSpeed > 0.0f && Time.deltaTime > 0.0f )
		{
			thisTFM.rotation = Quaternion.RotateTowards(curRotation, thisTFM.rotation, rotationSpeed * Time.deltaTime);
		}

		if ( isNavAgent )
		{
			navMeshAgent.updateRotation = updateRot;
		}
	}


	public void PlayAnimation(AIBehaviorsAnimationState animState)
	{
		if ( onPlayAnimation != null )
		{
			onPlayAnimation(animState);
		}
		else if ( animationCallbackComponent != null && animState != null )
		{
			if ( !string.IsNullOrEmpty(animationCallbackMethodName) )
			{
				animationCallbackComponent.SendMessage(animationCallbackMethodName, animState);
			}
		}
	}


	public float GetHealthValue()
	{
		return health;
	}


	public void SetHealthValue(float healthAmount)
	{
		health = healthAmount;
	}


	public void AddHealthValue(float healthAmount)
	{
		health += healthAmount;
	}


	public void SubtractHealthValue(float healthAmount)
	{
		health -= healthAmount;
	}


	public Transform GetClosestPlayer()
	{
		float sqrDist;
		return GetClosestPlayer(out sqrDist);
	}


	public Transform GetClosestPlayer(out float squareDistance)
	{
		float sqrSightDistance = sightDistance * sightDistance;
		int closestPlayerIndex = -1;

		squareDistance = Mathf.Infinity;

		for ( int i = 0; i < playerTransforms.Length; i++ )
		{
			Vector3 playerPosition = playerTransforms[i].position;
			Vector3 targetDifference = playerPosition - thisPos;

			// is the target within distance?
			if ( targetDifference.sqrMagnitude < sqrSightDistance )
			{
				squareDistance = targetDifference.sqrMagnitude;
				closestPlayerIndex = i;
			}
		}

		if ( closestPlayerIndex == -1 )
			return null;
		else
			return playerTransforms[closestPlayerIndex];
	}


	public Transform GetClosestPlayerWithinSight()
	{
		return GetClosestPlayerWithinSight(false);
	}


	public Transform GetClosestPlayerWithinSight(bool includeSightFalloff)
	{
		float dist = 0.0f;
		return GetClosestPlayerWithinSight(out dist, includeSightFalloff);
	}


	// TODO: Refactor this method to not use random values.
	public Transform GetClosestPlayerWithinSight(out float squareDistance, bool includeSightFalloff)
	{
		float sqrSightDistance = sightDistance * sightDistance;
		int closestPlayerIndex = -1;

		squareDistance = Mathf.Infinity;

		for ( int i = 0; i < playerTransforms.Length; i++ )
		{
			Vector3 playerPosition = playerTransforms[i].position;
			Vector3 targetDifference = playerPosition - thisPos;
			float targetSqrMagnitude = targetDifference.sqrMagnitude;
			float angle = Vector3.Angle(targetDifference, thisTFM.forward);
			Vector3 sightPosition = thisTFM.TransformPoint(eyePosition);

			// If we already have a player in sight
			if ( closestPlayerIndex != -1 )
			{
				// Is the new one closer than the previous closest one?
				if ( targetSqrMagnitude > squareDistance )
				{
					continue;
				}
			}

			// is the target within distance?
			if ( targetSqrMagnitude < sqrSightDistance )
			{
				float halfFOV = sightFOV / 2.0f;

				if ( angle < halfFOV )
				{
					if ( useSightFalloff && includeSightFalloff )
					{
						float anglePercentage = Mathf.InverseLerp(0.0f, halfFOV, angle);

						if ( Random.value < anglePercentage )
						{
							continue;
						}
					}

					RaycastHit hit;

					// Make sure there isn't anything between the player
					if ( !Physics.Linecast(sightPosition, playerPosition, out hit, raycastLayers) )
					{
						squareDistance = targetSqrMagnitude;
						closestPlayerIndex = i;
					}
				}
			}
		}

		if ( closestPlayerIndex == -1 )
			return null;
		else
			return playerTransforms[closestPlayerIndex];
	}


	public void PlayAudio()
	{
		if ( currentState.audioClip == null )
		{
			return;
		}

		if ( audio == null )
		{
			gameObject.AddComponent<AudioSource>();
		}

		audio.loop = currentState.loopAudio;
		audio.volume = currentState.audioVolume;
		audio.pitch = currentState.audioPitch + ((Random.value * currentState.audioPitchRandomness) - (currentState.audioPitchRandomness / 2.0f)) * 2.0f;
		audio.clip = currentState.audioClip;

		audio.Play();
	}
}