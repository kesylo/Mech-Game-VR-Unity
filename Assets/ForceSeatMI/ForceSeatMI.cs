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

using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;

namespace MotionSystems
{
	///
	/// Wrapper for ForceSeatMI native DLL
	///
	public class ForceSeatMI: IDisposable
	{
		public ForceSeatMI()
		{
			LoadAndCreate();
		}

		public void Dispose()
		{
			Close();
		}

		public bool IsLoaded()
		{
			return m_api != IntPtr.Zero;
		}

		///
		/// Call this function when the SIM is ready for sending data to the motion platform.
		///
		public bool BeginMotionControl()
		{
			if (m_api == IntPtr.Zero) return false;
			return m_fsmiBeginMotionControl(m_api) != 0;
		}

		///
		/// Call this function to when the SIM does not want to send any more data to the motion platform.
		///
		public bool EndMotionControl()
		{
			if (m_api == IntPtr.Zero) return false;
			return m_fsmiEndMotionControl(m_api) != 0;
		}

		///
		/// It gets current status of the motion platform. It can be called at any time.
		///
		public bool GetPlatformInfoEx(ref FSMI_PlatformInfo info, uint platformInfoStructSize, uint timeout)
		{
			if (m_api == IntPtr.Zero) return false;
			return m_fsmiGetPlatformInfoEx(m_api, ref info, platformInfoStructSize, timeout) != 0;
		}

		///
		/// It sends updated telemetry information to ForceSeatPM for further processing. 
		/// Make sure that 'mask' and 'state' fields are set correctly.
		/// Make sure to call ForceSeatMI_BeginMotionControl before this function is called.
		///
		public bool SendTelemetry(ref FSMI_Telemetry telemetry)
		{
			if (m_api == IntPtr.Zero) return false;
			return m_fsmiSendTelemetry(m_api, ref telemetry) != 0;
		}

		///
		/// Use this function if you want to specify position of the top table (top frame) in logical units (percent of maximum rotation and translation).
		/// Make sure to call ForceSeatMI_BeginMotionControl before this function is called.
		///
		public bool SendTopTablePosLog(ref FSMI_TopTablePositionLogical position)
		{
			if (m_api == IntPtr.Zero) return false;
			return m_fsmiSendTopTablePosLog(m_api, ref position) != 0;
		}

		///
		/// Use this function if you want to specify position of the top table (top frame) in physical units (radians and milimeters).
		/// Make sure to call ForceSeatMI_BeginMotionControl before this function is called.
		///
		public bool SendTopTablePosPhy(ref FSMI_TopTablePositionPhysical position)
		{
			if (m_api == IntPtr.Zero) return false;
			return m_fsmiSendTopTablePosPhy(m_api, ref position) != 0;
		}

		///
		/// Use this function if you want to specify transformation matrix for the top table (top frame). 
		/// It is recommended only for 6DoF in cases when rotation center is not in default point (0, 0, 0).
		/// Make sure to call ForceSeatMI_BeginMotionControl before this function is called.
		///
		public bool SendTopTableMatrixPhy(ref FSMI_TopTableMatrixPhysical matrix)
		{
			if (m_api == IntPtr.Zero) return false;
			return m_fsmiSendTopTableMatrixPhy(m_api, ref matrix) != 0;
		}

		///
		/// all this function to generate vibrations on connected tactile transcuder.
		///
		public bool SendTactileFeedbackEffects(ref FSMI_TactileFeedbackEffects effects)
		{
			if (m_api == IntPtr.Zero) return false;
			return m_fsmiSendTactileFeedbackEffects(m_api, ref effects) != 0;
		}

		#region Internals
		private IntPtr m_api = IntPtr.Zero;
		private IntPtr m_apiDll = IntPtr.Zero;

		~ForceSeatMI()
		{
			// Just in case it is not deleted
			Close();
		}

		private Delegate LoadFunction<T>(string functionName)
		{
			var addr = GetProcAddress(m_apiDll, functionName);
			if (addr == IntPtr.Zero) 
			{
				return null;
			}
			return Marshal.GetDelegateForFunctionPointer(addr, typeof(T));
		}

		private void LoadAndCreate()
		{
			bool is64Bits = IntPtr.Size > 4;
			
			string registryPath = is64Bits 
				? "HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\MotionSystems\\ForceSeatPM" 
				: "HKEY_LOCAL_MACHINE\\SOFTWARE\\MotionSystems\\ForceSeatPM";
			
			string dllName = is64Bits 
				? "ForceSeatMI64.dll" 
				: "ForceSeatMI32.dll";

			// Let's check if there is ForceSeatPM installed, if yes there is ForceSeatMIxx.dll that can be used
			string installationPath = (string)Registry.GetValue(registryPath, "InstallationPath", null);
			if (installationPath != null)
			{
				m_apiDll = LoadLibrary(installationPath + "\\" + dllName);
			}

			// If there is still not ForceSeatMIxx.dll found, then let's try in standard search path
			if (m_apiDll == IntPtr.Zero)
			{
				m_apiDll = LoadLibrary(dllName);
			}

			if (m_apiDll != IntPtr.Zero) 
			{
				m_fsmiCreate                     = (ForceSeatMI_Create_Delegate)                    LoadFunction<ForceSeatMI_Create_Delegate>                    ("ForceSeatMI_Create");
				m_fsmiDelete                     = (ForceSeatMI_Delete_Delegate)                    LoadFunction<ForceSeatMI_Delete_Delegate>                    ("ForceSeatMI_Delete");
				m_fsmiBeginMotionControl         = (ForceSeatMI_BeginMotionControl_Delegate)        LoadFunction<ForceSeatMI_BeginMotionControl_Delegate>        ("ForceSeatMI_BeginMotionControl");
				m_fsmiEndMotionControl           = (ForceSeatMI_EndMotionControl_Delegate)          LoadFunction<ForceSeatMI_EndMotionControl_Delegate>          ("ForceSeatMI_EndMotionControl");
				m_fsmiGetPlatformInfoEx          = (ForceSeatMI_GetPlatformInfoEx_Delegate)         LoadFunction<ForceSeatMI_GetPlatformInfoEx_Delegate>         ("ForceSeatMI_GetPlatformInfoEx");
				m_fsmiSendTelemetry              = (ForceSeatMI_SendTelemetry_Delegate)             LoadFunction<ForceSeatMI_SendTelemetry_Delegate>             ("ForceSeatMI_SendTelemetry");
				m_fsmiSendTopTablePosLog         = (ForceSeatMI_SendTopTablePosLog_Delegate)        LoadFunction<ForceSeatMI_SendTopTablePosLog_Delegate>        ("ForceSeatMI_SendTopTablePosLog");
				m_fsmiSendTopTablePosPhy         = (ForceSeatMI_SendTopTablePosPhy_Delegate)        LoadFunction<ForceSeatMI_SendTopTablePosPhy_Delegate>        ("ForceSeatMI_SendTopTablePosPhy");
				m_fsmiSendTopTableMatrixPhy      = (ForceSeatMI_SendTopTableMatrixPhy_Delegate)     LoadFunction<ForceSeatMI_SendTopTableMatrixPhy_Delegate>     ("ForceSeatMI_SendTopTableMatrixPhy");
				m_fsmiSendTactileFeedbackEffects = (ForceSeatMI_SendTactileFeedbackEffects_Delegate)LoadFunction<ForceSeatMI_SendTactileFeedbackEffects_Delegate>("ForceSeatMI_SendTactileFeedbackEffects");

				if (m_fsmiCreate                     != null && 
					m_fsmiDelete                     != null && 
					m_fsmiBeginMotionControl         != null && 
					m_fsmiEndMotionControl           != null &&
					m_fsmiGetPlatformInfoEx          != null &&
					m_fsmiSendTelemetry              != null &&
					m_fsmiSendTopTablePosLog         != null &&
					m_fsmiSendTopTablePosPhy         != null &&
					m_fsmiSendTopTableMatrixPhy      != null &&
					m_fsmiSendTactileFeedbackEffects != null)
				{
					m_api = m_fsmiCreate();
				}
			}
		}

		private void Close()
		{
			if (m_api != IntPtr.Zero)
			{
				m_fsmiDelete(m_api);
				m_api = IntPtr.Zero;
			}

			m_fsmiCreate                     = null;
			m_fsmiDelete                     = null;
			m_fsmiBeginMotionControl         = null;
			m_fsmiSendTelemetry              = null;
			m_fsmiEndMotionControl           = null;
			m_fsmiGetPlatformInfoEx          = null;
			m_fsmiSendTopTablePosLog         = null;
			m_fsmiSendTopTablePosPhy         = null;
			m_fsmiSendTopTableMatrixPhy      = null;
			m_fsmiSendTactileFeedbackEffects = null;

			if (m_apiDll != IntPtr.Zero)
			{
				FreeLibrary(m_apiDll);
				m_api = IntPtr.Zero;
			}
		}
		#endregion

		#region DLLImports
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr LoadLibrary(string libname);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern bool FreeLibrary(IntPtr hModule);

		[DllImport("Kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate IntPtr ForceSeatMI_Create_Delegate                  ();

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate void ForceSeatMI_Delete_Delegate                    (IntPtr api);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate byte ForceSeatMI_BeginMotionControl_Delegate        (IntPtr api);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate byte ForceSeatMI_EndMotionControl_Delegate          (IntPtr api);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate byte ForceSeatMI_GetPlatformInfoEx_Delegate         (IntPtr api, ref FSMI_PlatformInfo info, uint platformInfoStructSize, uint timeout);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate byte ForceSeatMI_SendTelemetry_Delegate             (IntPtr api, ref FSMI_Telemetry info);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate byte ForceSeatMI_SendTopTablePosLog_Delegate        (IntPtr api, ref FSMI_TopTablePositionLogical position);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate byte ForceSeatMI_SendTopTablePosPhy_Delegate        (IntPtr api, ref FSMI_TopTablePositionPhysical position);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate byte ForceSeatMI_SendTopTableMatrixPhy_Delegate     (IntPtr api, ref FSMI_TopTableMatrixPhysical matrix);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate byte ForceSeatMI_SendTactileFeedbackEffects_Delegate(IntPtr api, ref FSMI_TactileFeedbackEffects effects);

		private ForceSeatMI_Create_Delegate                     m_fsmiCreate;
		private ForceSeatMI_Delete_Delegate                     m_fsmiDelete;
		private ForceSeatMI_BeginMotionControl_Delegate         m_fsmiBeginMotionControl;
		private ForceSeatMI_EndMotionControl_Delegate           m_fsmiEndMotionControl;
		private ForceSeatMI_GetPlatformInfoEx_Delegate          m_fsmiGetPlatformInfoEx;
		private ForceSeatMI_SendTelemetry_Delegate              m_fsmiSendTelemetry;
		private ForceSeatMI_SendTopTablePosLog_Delegate         m_fsmiSendTopTablePosLog;
		private ForceSeatMI_SendTopTablePosPhy_Delegate         m_fsmiSendTopTablePosPhy;
		private ForceSeatMI_SendTopTableMatrixPhy_Delegate      m_fsmiSendTopTableMatrixPhy;
		private ForceSeatMI_SendTactileFeedbackEffects_Delegate m_fsmiSendTactileFeedbackEffects;
		#endregion
	}
}
