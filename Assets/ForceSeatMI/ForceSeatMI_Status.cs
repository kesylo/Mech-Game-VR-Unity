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
	///
	/// Actual platform status and motors position
	///
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FSMI_PlatformInfo
	{
		public byte structSize; // check if this equals to sizeof(FSMI_PlatformInfo)
		public ulong timemark; // 64-bits

		public byte isConnected;
		public byte isPaused;

		public ushort actualMotor1_Position;
		public ushort actualMotor2_Position;
		public ushort actualMotor3_Position;
		public ushort actualMotor4_Position;
		public ushort actualMotor5_Position;
		public ushort actualMotor6_Position;
		public int actualMotor1_Speed;
		public int actualMotor2_Speed;
		public int actualMotor3_Speed;
		public int actualMotor4_Speed;
		public int actualMotor5_Speed;
		public int actualMotor6_Speed;

		public byte isThermalProtectionActivated; // global thermal protection status
		public byte worstModuleStatus;            // worst module (actuator or CAN node) status - one of FSMI_ModuleStatus
		public byte worstModuleStatusIndex;       // index of module that above status applies to
		public byte coolingSystemMalfunction;     // global cooling system status

		public byte isKinematicsSupported;       // true if Inverse and Forward Kinematics are supported

		public float ikPrecision1;               // precision of table precise positioning
		public float ikPrecision2;
		public float ikPrecision3;
		public float ikPrecision4;
		public float ikPrecision5;
		public float ikPrecision6;
		public byte  ikRecentStatus; // true if Inverse Kinematics was calculated correctly and given position is withing operating range

		public float fkRoll;         // roll  in rad from Fordward Kinematics
		public float fkPitch;        // pitch in rad from Fordward Kinematics
		public float fkYaw;          // yaw   in rad from Fordward Kinematics
		public float fkHeave;        // heave in mm  from Fordward Kinematics
		public float fkSway;         // sway  in mm  from Fordward Kinematics
		public float fkSurge;        // surge in mm  from Fordward Kinematics
		public byte  fkRecentStatus; // true if Fordward Kinematics was calculated correctly

		// New fields in 2.60
		public ushort requiredMotor1_Position;
		public ushort requiredMotor2_Position;
		public ushort requiredMotor3_Position;
		public ushort requiredMotor4_Position;
		public ushort requiredMotor5_Position;
		public ushort requiredMotor6_Position;

		public float fkRoll_deg;    // roll  in deg from Forward Kinematics
		public float fkPitch_deg;   // pitch in deg from Forward Kinematics
		public float fkYaw_deg;     // yaw   in deg from Forward Kinematics

		public float fkRollSpeed_deg;  // roll  in deg/s from Forward Kinematics
		public float fkPitchSpeed_deg; // pitch in deg/s from Forward Kinematics
		public float fkYawSpeed_deg;   // yaw   in deg/s from Forward Kinematics
		public float fkHeaveSpeed;     // heave in mm/s  from Forward Kinematics
		public float fkSwaySpeed;      // sway  in mm/s  from Forward Kinematics
		public float fkSurgeSpeed;     // surge in mm/s  from Forward Kinematics

		public float fkRollAcc_deg;  // roll  in deg/s2 from Forward Kinematics
		public float fkPitchAcc_deg; // pitch in deg/s2 from Forward Kinematics
		public float fkYawAcc_deg;   // yaw   in deg/s2 from Forward Kinematics
		public float fkHeaveAcc;     // heave in mm/s2  from Forward Kinematics
		public float fkSwayAcc;      // sway  in mm/s2  from Forward Kinematics
		public float fkSurgeAcc;     // surge in mm/s2  from Forward Kinematics
	}
}
