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
	/// This structure defines effects for tactile transcuders. Signals of given frequency and amplitude are generates on associated audio outputs.
	///
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct FSMI_TactileFeedbackEffects
	{
		public byte structSize; // put here sizeof(FSMI_TactileFeedbackEffects)

		public uint frequencyCenter; // frequency in Hz
		public uint frequencyEngine;
		public uint frequencyFL;
		public uint frequencyFR;
		public uint frequencyRL;
		public uint frequencyRR;
		public uint frequencyCH;

		public float amplitudeCenter; // signal amplitude from 0 to 1
		public float amplitudeEngine;
		public float amplitudeFL;
		public float amplitudeFR;
		public float amplitudeRL;
		public float amplitudeRR;
		public float amplitudeCH;
	}
}
