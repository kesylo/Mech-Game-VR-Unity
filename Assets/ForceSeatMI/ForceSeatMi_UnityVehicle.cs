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

using System.Runtime.InteropServices;
using UnityEngine;

namespace MotionSystems
{
	public class ForceSeatMI_UnityVehicle
	{
		const float FSMI_VT_ACC_LOW_PASS_FACTOR = 0.5f;
		const float FSMI_VT_ANGLES_SPEED_LOW_PASS_FACTOR = 0.5f;

		private ForceSeatMI    m_api;
		private FSMI_Telemetry m_telemetry = new FSMI_Telemetry();

		private bool    m_firstCall = true;
		private float   m_prevSurgeSpeed;
		private float   m_prevSwaySpeed;
		private float   m_prevHeaveSpeed;
		private Vector3 m_prevAngles = new Vector3();

		public void Begin()
		{
			m_api = new ForceSeatMI();

			m_telemetry.mask = 0;
			m_telemetry.structSize = (byte)Marshal.SizeOf(m_telemetry);
			m_telemetry.state = FSMI_State.NO_PAUSE;
			m_telemetry.mask = FSMI_TEL_BIT.STATE |
							   FSMI_TEL_BIT.RPM |
							   FSMI_TEL_BIT.MAX_RPM |
							   FSMI_TEL_BIT.SPEED |
							   FSMI_TEL_BIT.YAW_PITCH_ROLL |
							   FSMI_TEL_BIT.YAW_PITCH_ROLL_SPEED |
							   FSMI_TEL_BIT.SWAY_HEAVE_SURGE_ACCELERATION |
							   FSMI_TEL_BIT.SWAY_HEAVE_SURGE_SPEED |
							   FSMI_TEL_BIT.GEAR_NUMBER;

			m_api.BeginMotionControl();
		}

		public void End()
		{
			if (m_api.IsLoaded())
			{
				m_api.EndMotionControl();
				m_api.Dispose();
			}
		}

		public void Tick(Rigidbody body, float deltaTime, bool paused, float rpm, float maxRpm, int gearNumber)
		{
			var velocity      = body.transform.InverseTransformDirection(body.velocity);
			var rotation      = body.transform.rotation;
			var localRotation = body.transform.localRotation;

			m_telemetry.state      = paused ? (byte)FSMI_State.PAUSE : (byte)FSMI_State.NO_PAUSE;
			m_telemetry.rpm        = (uint)rpm;
			m_telemetry.maxRpm     = (uint)maxRpm;
			m_telemetry.gearNumber = (sbyte)gearNumber;
			m_telemetry.speed      = velocity.magnitude * 3.6f; // km/h
			m_telemetry.surgeSpeed = velocity.z;
			m_telemetry.swaySpeed  = velocity.x;
			m_telemetry.heaveSpeed = velocity.y;
			m_telemetry.roll       = -Mathf.Deg2Rad * (localRotation.eulerAngles.z > 180 ? localRotation.eulerAngles.z - 360 : localRotation.eulerAngles.z);
			m_telemetry.pitch      = -Mathf.Deg2Rad * (localRotation.eulerAngles.x > 180 ? localRotation.eulerAngles.x - 360 : localRotation.eulerAngles.x);
			m_telemetry.yaw        =  Mathf.Deg2Rad * (localRotation.eulerAngles.y > 180 ? localRotation.eulerAngles.y - 360 : localRotation.eulerAngles.y);

			if (m_firstCall)
			{
				m_firstCall = false;

				m_telemetry.surgeAcceleration = 0;
				m_telemetry.swayAcceleration = 0;
				m_telemetry.heaveAcceleration = 0;
				m_telemetry.pitchSpeed = 0;
				m_telemetry.rollSpeed = 0;
				m_telemetry.yawSpeed = 0;
			}
			else
			{
				LowPassFilter(ref m_telemetry.surgeAcceleration, (m_telemetry.surgeSpeed - m_prevSurgeSpeed) / Time.deltaTime, FSMI_VT_ACC_LOW_PASS_FACTOR);
				LowPassFilter(ref m_telemetry.swayAcceleration,  (m_telemetry.swaySpeed - m_prevSwaySpeed)   / Time.deltaTime, FSMI_VT_ACC_LOW_PASS_FACTOR);
				LowPassFilter(ref m_telemetry.heaveAcceleration, (m_telemetry.heaveSpeed - m_prevHeaveSpeed) / Time.deltaTime, FSMI_VT_ACC_LOW_PASS_FACTOR);

				var deltaAngles = new Vector3(Mathf.DeltaAngle(body.transform.eulerAngles.x, m_prevAngles.x),
                    Mathf.DeltaAngle(body.transform.eulerAngles.y, m_prevAngles.y),
                    Mathf.DeltaAngle(body.transform.eulerAngles.z, m_prevAngles.z));

				LowPassFilter(ref m_telemetry.rollSpeed,  deltaAngles.z / Time.deltaTime, FSMI_VT_ANGLES_SPEED_LOW_PASS_FACTOR);
				LowPassFilter(ref m_telemetry.pitchSpeed, deltaAngles.x / Time.deltaTime, FSMI_VT_ANGLES_SPEED_LOW_PASS_FACTOR);
				LowPassFilter(ref m_telemetry.yawSpeed,   deltaAngles.y / Time.deltaTime, FSMI_VT_ANGLES_SPEED_LOW_PASS_FACTOR);
			}

			m_prevSurgeSpeed = m_telemetry.surgeSpeed;
			m_prevSwaySpeed  = m_telemetry.swaySpeed;
			m_prevHeaveSpeed = m_telemetry.heaveSpeed;

			m_prevAngles.x = body.transform.eulerAngles.x;
			m_prevAngles.y = body.transform.eulerAngles.y;
			m_prevAngles.z = body.transform.eulerAngles.z;

			m_api.SendTelemetry(ref m_telemetry);
		}

		private void LowPassFilter(ref float stored, float newValue, float factor)
		{
			stored += (newValue - stored) * factor;
		}
	}
}
