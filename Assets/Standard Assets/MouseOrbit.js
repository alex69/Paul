var rotJoy : GameObject;

var target : Transform;
var distance = 10.0;

var xSpeed = 250.0;
var ySpeed = 120.0;

var yMinLimit = -20;
var yMaxLimit = 80;

private var x = 0.0;
private var y = 0.0;

@AddComponentMenu("Camera-Control/Mouse Orbit")
partial class MouseOrbit { }

function Start () {
    var angles = transform.eulerAngles;
    // Make it dynamic to place camera behind the ball
    x = 90;
    y = 20;

	// Make the rigid body not change rotation
   	if (rigidbody)
		rigidbody.freezeRotation = true;
}

function LateUpdate () {
    if (target) {
    	if(Application.platform == RuntimePlatform.Android)
    	{
	    	var other : Joystick;
		    other = rotJoy.GetComponent("Joystick");
		    var tmpRotJoyX = other.position.x * 0.6f;
		    x += Time.deltaTime * tmpRotJoyX * xSpeed;
		    
		    var tmpRotJoyY = other.position.y * 0.4f;
		    y -= Time.deltaTime * tmpRotJoyY * ySpeed;
    		y = ClampAngle(y, yMinLimit, yMaxLimit);
    	}
    	else
    	{
    		// Check to see if the right or left keys are being pressed
		    if (Input.GetAxis("Horizontal")) {
		        x += Input.GetAxis("Horizontal");
		    }
    		//x += Time.deltaTime * Input.GetAxis("Mouse X") * xSpeed;
    		//y -= Time.deltaTime * Input.GetAxis("Mouse Y") * ySpeed;
    		//y = ClampAngle(y, yMinLimit, yMaxLimit);
    	}

        var rotation = Quaternion.EulerAngles(y * Mathf.Deg2Rad, x * Mathf.Deg2Rad, 0);
        var position = rotation * Vector3(0.0, 0.0, -distance) + target.position;
        
        transform.rotation = rotation;
        transform.position = position;
    }
}

static function ClampAngle (angle : float, min : float, max : float) {
	if (angle < -360)
		angle += 360;
	if (angle > 360)
		angle -= 360;
	return Mathf.Clamp (angle, min, max);
}