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
    /// This structure defines position of top frame (table) in logical units (percents).
    /// It does not use Inverse Kinematics module so rotation and movements are not always linear.
    ///
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FSMI_TopTablePositionLogical
    {
          public byte structSize; // put here sizeof(FSMI_TopTablePositionLogical)
          public uint mask;       // set here bits to tell motion software which of below fields are provided

/*BIT:1*/ public byte state;      // state flag (bit fields, it is used to PAUSE or UNPAUSE the system)

/*BIT:2*/ public short roll;       // -32k max left,   +32k max right
/*BIT:2*/ public short pitch;      // -32k max rear,   +32k max front
/*BIT:2*/ public short yaw;        // -32k max left,   +32k max right
/*BIT:2*/ public short heave;      // -32k max bottom, +32k max top
/*BIT:2*/ public short sway;       // -32k max left,   +32k max right
/*BIT:2*/ public short surge;      // -32k max rear,   +32k max front

/*BIT:3*/ public ushort maxSpeed;  // 0 to 64k, actual speed is not always equal to max speed due to ramps

/*BIT:4*/ public byte triggers; // state of buttons, gun triggers, etc. It is passed directly to the motion platform

/*BIT:5*/ public float aux1;
/*BIT:5*/ public float aux2;
/*BIT:5*/ public float aux3;
/*BIT:5*/ public float aux4;
/*BIT:5*/ public float aux5;
/*BIT:5*/ public float aux6;
/*BIT:5*/ public float aux7;
/*BIT:5*/ public float aux8;
    }

    ///
    /// This structure defines position of top frame (table) in physical units (rad, mm).
    /// It uses Inverse Kinematics module and it might not be supported by all motion platforms.
    /// By default BestMatch strategy is used.
    ///
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FSMI_TopTablePositionPhysical
    {
          public byte structSize; // put here sizeof(FSMI_TopTablePositionPhysical)
          public uint mask;       // set here bits to tell motion software which of below fields are provided

/*BIT:1*/ public byte state;      // state flag (bit fields, it is used to PAUSE or UNPAUSE the system)

/*BIT:2*/ public float roll;       // in radians, roll  < 0 = left,  roll > 0  = right
/*BIT:2*/ public float pitch;      // in radians, pitch < 0 = front, pitch > 0 = rear
/*BIT:2*/ public float yaw;        // in radians, yaw   < 0 = right, yaw > 0   = left
/*BIT:2*/ public float heave;      // in mm, heave < 0 - down, heave > 0 - top
/*BIT:2*/ public float sway;       // in mm, sway  < 0 - left, sway  > 0 - right
/*BIT:2*/ public float surge;      // in mm, surge < 0 - rear, surge > 0 - front

/*BIT:3*/ public ushort maxSpeed;  // 0 to 64k, actual speed is not always equal to max speed due to ramps

/*BIT:4*/ public byte triggers; // state of buttons, gun triggers, etc. It is passed directly to the motion platform

/*BIT:5*/ public float aux1;
/*BIT:5*/ public float aux2;
/*BIT:5*/ public float aux3;
/*BIT:5*/ public float aux4;
/*BIT:5*/ public float aux5;
/*BIT:5*/ public float aux6;
/*BIT:5*/ public float aux7;
/*BIT:5*/ public float aux8;
    }

    ///
    /// This structure defines position of top frame (table) in physical units (rad, mm) by specifing transformation matrix.
    /// It uses Inverse Kinematics module and it is dedicated for 6DoF motion platforms.
    /// If matrix transformation is specified, the Inverse Kinematics module always uses FullMatch strategy.
    ///
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FSMI_TopTableMatrixPhysical
    {
          public byte structSize; // put here sizeof(FSMI_TopTableMatrixPhysical)
          public uint mask;       // set here bits to tell motion software which of below fields are provided

/*BIT:1*/ public byte state;      // check FSMI_Telemetry::state for details

          /* OFFSET (in mm):
           *  x axis = left-right movement, sway,  x < 0 - left, x > 0 - right
           *  y axis = rear-front movement, surge, y < 0 - rear, y > 0 - front
           *  z axis = down-top movement,   heave, z < 0 - down, z > 0 - top
           *
           * ROTATION (in radians):
           * x axis, pitch = x < 0 = front, x > 0 = rear
           * y axis, roll = y < 0  = left,  y > 0 = right
           * z axis, yaw  = z < 0  = right, z > 0 = left
           * /
/*BIT:2*/ public float m11;        // 3D transformation matrix, 1st row
/*BIT:2*/ public float m12;        // 3D transformation matrix, 1st row
/*BIT:2*/ public float m13;        // 3D transformation matrix, 1st row
/*BIT:2*/ public float m14;        // 3D transformation matrix, 1st row

/*BIT:2*/ public float m21;        // 3D transformation matrix, 2nd row
/*BIT:2*/ public float m22;        // 3D transformation matrix, 2nd row
/*BIT:2*/ public float m23;        // 3D transformation matrix, 2nd row
/*BIT:2*/ public float m24;        // 3D transformation matrix, 2nd row

/*BIT:2*/ public float m31;        // 3D transformation matrix, 3rd row
/*BIT:2*/ public float m32;        // 3D transformation matrix, 3rd row
/*BIT:2*/ public float m33;        // 3D transformation matrix, 3rd row
/*BIT:2*/ public float m34;        // 3D transformation matrix, 3rd row

/*BIT:2*/ public float m41;        // 3D transformation matrix, 4rd row
/*BIT:2*/ public float m42;        // 3D transformation matrix, 4rd row
/*BIT:2*/ public float m43;        // 3D transformation matrix, 4rd row
/*BIT:2*/ public float m44;        // 3D transformation matrix, 4rd row

/*BIT:3*/ public ushort maxSpeed;  // 0 to 64k, actual speed is not always equal to max speed due to ramps

/*BIT:4*/ public byte triggers; // state of buttons, gun triggers, etc. It is passed directly to the motion platform

/*BIT:5*/ public float aux1;
/*BIT:5*/ public float aux2;
/*BIT:5*/ public float aux3;
/*BIT:5*/ public float aux4;
/*BIT:5*/ public float aux5;
/*BIT:5*/ public float aux6;
/*BIT:5*/ public float aux7;
/*BIT:5*/ public float aux8;
    }

    /// Helpers for mask bits
    public struct FSMI_POS_BIT
    {
        public const int STATE     = (1 << 1);
        public const int POSITION  = (1 << 2);
        public const int MATRIX    = (1 << 2);
        public const int MAX_SPEED = (1 << 3);
        public const int TRIGGERS  = (1 << 4);
        public const int AUX       = (1 << 5);
    }
}
