function Start ()
{
	// Add a duplicate shoot animation which we set up to only animate the upper body
	// We use this animation when the character is running.
	// By using mixing for this we dont need to make a seperate running-shoot animation
	animation.AddClip(animation["shoot"].clip, "shootUpperBody");
	animation["shootUpperBody"].AddMixingTransform(transform.Find("mover/gun"));
	animation["shootUpperBody"].AddMixingTransform(transform.Find("mover/roothandle/spine1"));

	// Set all animations to loop
	animation.wrapMode = WrapMode.Loop;

	// Except our action animations, Dont loop those
	animation["jump"].wrapMode = WrapMode.Clamp;
	animation["shoot"].wrapMode = WrapMode.Clamp;
	animation["shootUpperBody"].wrapMode = WrapMode.Clamp;
	
	// Put idle and run in a lower layer. They will only animate if our action animations are not playing
	animation["idle"].layer = -1;
	animation["run"].layer = -1;
	
	animation.Stop();
}


function Update () {
	// Play either the run or idle animation
	if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.1)
	{
		animation.CrossFade("run");
		// Play animation backwards when running backwards
		animation["run"].speed = Mathf.Sign(Input.GetAxis("Vertical"));
	}
	else
		animation.CrossFade("idle");

	// Play the cross fade animation
	if (Input.GetButtonDown ("Jump"))
	{
		animation.CrossFade("jump", 0.3);
	}
		

	// Play the shoot animation
	if (Input.GetButtonDown ("Fire1"))
	{
		// We are running so play it only on the upper body
		if (animation["run"].weight > 0.5)
			animation.CrossFadeQueued("shootUpperBody", 0.3, QueueMode.PlayNow);
		// We are in idle so play it on the fully body
		else
			animation.CrossFadeQueued("shoot", 0.3, QueueMode.PlayNow);
	}
}