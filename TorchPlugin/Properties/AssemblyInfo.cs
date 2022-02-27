using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("PerformanceImprovements")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("PerformanceImprovements")]
[assembly: AssemblyCopyright("Copyright ©  2022")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("C2CF3ED2-C6AC-4DBD-AB9A-613A1EF67784")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.5.0.0")]
[assembly: AssemblyFileVersion("1.5.0.0")]

// See https://www.strathweb.com/2018/10/no-internalvisibleto-no-problem-bypassing-c-visibility-rules-with-roslyn/
[assembly: IgnoresAccessChecksTo("HavokWrapper")]
[assembly: IgnoresAccessChecksTo("Sandbox.Common")]
[assembly: IgnoresAccessChecksTo("Sandbox.Game")]
[assembly: IgnoresAccessChecksTo("Sandbox.Game.XmlSerializers")]
[assembly: IgnoresAccessChecksTo("Sandbox.Graphics")]
[assembly: IgnoresAccessChecksTo("Sandbox.RenderDirect")]
[assembly: IgnoresAccessChecksTo("SpaceEngineers.Game")]
[assembly: IgnoresAccessChecksTo("SpaceEngineers.ObjectBuilders")]
[assembly: IgnoresAccessChecksTo("SpaceEngineers.ObjectBuilders.XmlSerializers")]
[assembly: IgnoresAccessChecksTo("VRage.Ansel")]
[assembly: IgnoresAccessChecksTo("VRage.Audio")]
[assembly: IgnoresAccessChecksTo("VRage")]
[assembly: IgnoresAccessChecksTo("VRage.Dedicated")]
[assembly: IgnoresAccessChecksTo("VRage.EOS")]
[assembly: IgnoresAccessChecksTo("VRage.EOS.XmlSerializers")]
[assembly: IgnoresAccessChecksTo("VRage.Game")]
[assembly: IgnoresAccessChecksTo("VRage.Game.XmlSerializers")]
[assembly: IgnoresAccessChecksTo("VRage.Input")]
[assembly: IgnoresAccessChecksTo("VRage.Library")]
[assembly: IgnoresAccessChecksTo("VRage.Math")]
[assembly: IgnoresAccessChecksTo("VRage.Math.XmlSerializers")]
[assembly: IgnoresAccessChecksTo("VRage.Mod.Io")]
[assembly: IgnoresAccessChecksTo("VRage.NativeWrapper")]
[assembly: IgnoresAccessChecksTo("VRage.Network")]
[assembly: IgnoresAccessChecksTo("VRage.Platform.Windows")]
[assembly: IgnoresAccessChecksTo("VRage.RemoteClient.Core")]
[assembly: IgnoresAccessChecksTo("VRage.Render")]
[assembly: IgnoresAccessChecksTo("VRage.Render11")]
[assembly: IgnoresAccessChecksTo("VRage.Scripting")]
[assembly: IgnoresAccessChecksTo("VRage.Steam")]
[assembly: IgnoresAccessChecksTo("VRage.UserInterface")]
[assembly: IgnoresAccessChecksTo("VRage.XmlSerializers")]