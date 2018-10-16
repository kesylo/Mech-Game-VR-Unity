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
	/// List of possible module status values
	///
	public enum FSMI_ModuleStatus
	{
		Ok                    = 0,
		Overheated            = 1,
		Communication_Error   = 2,
		Config_Error          = 3,
		LimitSwitch_Error     = 4,
		Calibration_Error     = 5,
		General_Error         = 6,
		NotConnected_Error    = 7,
		NoPowerSupply_Error   = 8,
		FanSpeedTooLow_Error  = 9
	};

	public struct FSMI_State
	{
		public const int NO_PAUSE  = (0 << 0);
		public const int PAUSE     = (1 << 0);
	}
}
