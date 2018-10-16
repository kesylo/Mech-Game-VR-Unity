/*
 * Copyright (C) 2012-2018 Motion Systems
 * 
 * This file is part of ForceSeat motion system.
 *
 * www.motionsystems.eu
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.Runtime.InteropServices;

namespace MotionSystems
{
	/*
	 * This interface can used to get motion data from application or game.
	 *
	 * When the ForceSeatMI telemetry is implemented in application, any motion software is able to gather simulation data and
	 * control motion platform (2DOF, 3DOF or 6DOF). This structure should be filled by the application and sent to motion processing 
	 * system. It is require to fill "structSize" and "mask", other fields are OPTIONAL. It means that the application does not
	 * have to support or provide all parameters mentioned below, but it is good to provide as much
	 * as possible.
	 *
	 * FIELDS LEGEND:
	 * ==============
	 * All values are in local vehicle coordinates.
	 *
	 *   YAW   - rotation along vertical axis,
	 *          > 0 when vehicle front is rotating right,
	 *          < 0 when vehicle front is rotating left
	 *   PITCH - rotation along lateral axis,
	 *          > 0 when vehicle front is rotating up
	 *          > 0 when vehicle front is rotating down
	 *   ROLL  - rotation along longitudinal axis,
	 *          > 0 when vehicle highest point is rotating right,
	 *          < 0 when vehicle highest point is rotating left
	 *   SWAY  - transition along lateral axis,
	 *          > 0 when vehicle is moving right,
	 *          < 0 when vehicle is moving left
	 *   HEAVE - transition along vertical axis,
	 *          > 0 when vehicle is moving up,
	 *          < 0 when vehicle is moving down
	 *   SURGE - transition along longitudinal axis,
	 *          > 0 when vehicle is moving forward,
	 *          < 0 when vehicle is moving backward
	 *
	 * Please check below link for details description of yaw, pitch and roll:
	 * http://en.wikipedia.org/wiki/Ship_motions
	 */
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FSMI_Telemetry
	{
// Version 2.61
           public byte structSize; // put here sizeof(FSMI_Telemetry)
           public uint mask;       // set here bits to tell motion software which of below fields are provided

/*BIT:1*/  public byte state;     // only single bit is used in current version:
                                  // (state & 0x01) == 0 -> no pause
                                  // (state & 0x01) == 1 -> paused

/*BIT:2*/  public uint rpm;       // engine rpm
/*BIT:3*/  public float speed;    // vehicle speed in m/s, can be < 0 for reverse

/*BIT:4*/  public float yaw;      // yaw in radians
/*BIT:4*/  public float pitch;    // vehicle pitch in radians
/*BIT:4*/  public float roll;     // vehicle roll in radians

/*BIT:5*/  public float yawAcceleration;   // radians/s^2
/*BIT:5*/  public float pitchAcceleration; // radians/s^2
/*BIT:5*/  public float rollAcceleration;  // radians/s^2

/*BIT:6*/  public float yawSpeed;   // radians/s
/*BIT:6*/  public float pitchSpeed; // radians/s
/*BIT:6*/  public float rollSpeed;  // radians/s

/*BIT:7*/  public float swayAcceleration;  // m/s^2
/*BIT:7*/  public float heaveAcceleration; // m/s^2
/*BIT:7*/  public float surgeAcceleration; // m/s^2

/*BIT:8*/  public float swaySpeed;  // m/s
/*BIT:8*/  public float heaveSpeed; // m/s
/*BIT:8*/  public float surgeSpeed; // m/s

/*BIT:9*/  public byte throttlePosition;   // 0 to 100 (in percent)
/*BIT:9*/  public byte brakePedalPosition; // 0 to 100 (in percent)
/*BIT:9*/  public byte clutchPedalPosition; // 0 to 100 (in percent)

/*BIT:10*/ public sbyte gearNumber; // -1 for reverse, 0 for neutral, 1, 2, 3, ...

/*BIT:11*/ public byte leftSideGroundType;    // grass, dirt, gravel, please check FSMI_GroundType
/*BIT:11*/ public byte rightSideGroundType;

/*BIT:12*/ public float collisionForce; // in Newtons (N), zero when there is no collision
/*BIT:12*/ public float collisionYaw;   // yaw, pitch and roll for start point of collision force vector, end point is always in vehicle center
/*BIT:12*/ public float collisionPitch;
/*BIT:12*/ public float collisionRoll;

/*BIT:13*/ public float globalPositionX; // global position, Y is vertical axes
/*BIT:13*/ public float globalPositionY;
/*BIT:13*/ public float globalPositionZ;

/*BIT:14*/ public uint timeMs;   // simulation time in e.g. m/s, can be relative (e.g. 0 means means application has just started)
/*BIT:15*/ public byte triggers; // state of buttons, gun triggers, etc. It is passed directly to the motion platform

/*BIT:16*/ public uint maxRpm;     // engine max rpm used to simulate rev limiter
/*BIT:17*/ public uint flags;      // combination of FSMI_Flags
/*BIT:18*/ public float aux1;
/*BIT:18*/ public float aux2;
/*BIT:18*/ public float aux3;
/*BIT:18*/ public float aux4;
/*BIT:18*/ public float aux5;
/*BIT:18*/ public float aux6;
/*BIT:18*/ public float aux7;
/*BIT:18*/ public float aux8;
	}

	public enum FSMI_GroundType
	{
		UknownGround      = 0,
		TarmacGround      = 1,
		GrassGround       = 2,
		DirtGround        = 3,
		GravelGround      = 4,
		RumbleStripGround = 5
	}

	public struct FSMI_Flags
	{
		public const int ShiftLight       = (1 << 0);
		public const int AbsIsWorking     = (1 << 1);
		public const int EspIsWorking     = (1 << 2);
		public const int FrontWheelDrive  = (1 << 3);
		public const int RearWheelDrive   = (1 << 4);
	}

	public struct FSMI_TEL_BIT
	{
		public const int STATE                         = (1 << 1);
		public const int RPM                           = (1 << 2);
		public const int SPEED                         = (1 << 3);

		public const int YAW_PITCH_ROLL                = (1 << 4);
		public const int YAW_PITCH_ROLL_ACCELERATION   = (1 << 5);
		public const int YAW_PITCH_ROLL_SPEED          = (1 << 6);
		public const int SWAY_HEAVE_SURGE_ACCELERATION = (1 << 7);
		public const int SWAY_HEAVE_SURGE_SPEED        = (1 << 8);

		public const int PEDALS_POSITION               = (1 << 9);
		public const int GEAR_NUMBER                   = (1 << 10);
		public const int GROUND_TYPE                   = (1 << 11);
		public const int COLLISION                     = (1 << 12);

		public const int GLOBAL_POSITION               = (1 << 13);
		public const int TIME                          = (1 << 14);
		public const int TRIGGERS                      = (1 << 15);

		public const int MAX_RPM                       = (1 << 16);
		public const int FLAGS                         = (1 << 17);
		public const int AUX                           = (1 << 18);
	}
}
