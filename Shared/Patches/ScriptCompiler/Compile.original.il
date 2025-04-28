// c401f3fb
ldarg.0
ldfld System.Int32 VRage.Scripting.<Compile>d__27::<>1__state
stloc.0
ldarg.0
ldfld VRage.Scripting.MyScriptCompiler VRage.Scripting.<Compile>d__27::<>4__this
stloc.1
[EX_BeginException]
ldloc.0
switch L0, L1, L2, L3, L4, L5, L6
ldarg.0
newobj System.Void VRage.Scripting.<>c__DisplayClass27_0::.ctor()
stfld VRage.Scripting.<>c__DisplayClass27_0 VRage.Scripting.<Compile>d__27::<>8__1
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass27_0 VRage.Scripting.<Compile>d__27::<>8__1
ldarg.0
ldfld System.Boolean VRage.Scripting.<Compile>d__27::trackMemoryUsage
stfld System.Boolean VRage.Scripting.<>c__DisplayClass27_0::trackMemoryUsage
ldarg.0
ldfld System.String VRage.Scripting.<Compile>d__27::friendlyName
brtrue.s L7
ldarg.0
ldstr "<No Name>"
stfld System.String VRage.Scripting.<Compile>d__27::friendlyName
L7:
ldarg.0
ldfld VRage.Scripting.MyApiTarget VRage.Scripting.<Compile>d__27::target
stloc.3
ldloc.3
switch L8, L9, L10
br.s L11
L8:
ldarg.0
ldnull
stfld Microsoft.CodeAnalysis.Diagnostics.DiagnosticAnalyzer VRage.Scripting.<Compile>d__27::<whitelistAnalyzer>5__2
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass27_0 VRage.Scripting.<Compile>d__27::<>8__1
ldnull
stfld System.Func`4<Microsoft.CodeAnalysis.CSharp.CSharpCompilation, Microsoft.CodeAnalysis.SyntaxTree, System.Boolean, Microsoft.CodeAnalysis.SyntaxTree> VRage.Scripting.<>c__DisplayClass27_0::syntaxTreeInjector
br.s L12
L9:
newobj System.Void VRage.Scripting.<>c__DisplayClass27_1::.ctor()
stloc.s 4 (VRage.Scripting.MyScriptCompiler+<>c__DisplayClass27_1)
ldloc.s 4 (VRage.Scripting.MyScriptCompiler+<>c__DisplayClass27_1)
ldarg.0
ldfld System.String VRage.Scripting.<Compile>d__27::friendlyName
call static System.Int32 VRage.Scripting.MyModWatchdog::AllocateModId(System.String modName)
stfld System.Int32 VRage.Scripting.<>c__DisplayClass27_1::modId
ldarg.0
ldloc.1
ldfld VRage.Scripting.Analyzers.WhitelistDiagnosticAnalyzer VRage.Scripting.MyScriptCompiler::m_modApiWhitelistDiagnosticAnalyzer
stfld Microsoft.CodeAnalysis.Diagnostics.DiagnosticAnalyzer VRage.Scripting.<Compile>d__27::<whitelistAnalyzer>5__2
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass27_0 VRage.Scripting.<Compile>d__27::<>8__1
ldloc.s 4 (VRage.Scripting.MyScriptCompiler+<>c__DisplayClass27_1)
ldftn Microsoft.CodeAnalysis.SyntaxTree VRage.Scripting.<>c__DisplayClass27_1::<Compile>b__0(Microsoft.CodeAnalysis.CSharp.CSharpCompilation c, Microsoft.CodeAnalysis.SyntaxTree st, System.Boolean b)
newobj System.Void System.Func`4<Microsoft.CodeAnalysis.CSharp.CSharpCompilation, Microsoft.CodeAnalysis.SyntaxTree, System.Boolean, Microsoft.CodeAnalysis.SyntaxTree>::.ctor(System.Object object, System.IntPtr method)
stfld System.Func`4<Microsoft.CodeAnalysis.CSharp.CSharpCompilation, Microsoft.CodeAnalysis.SyntaxTree, System.Boolean, Microsoft.CodeAnalysis.SyntaxTree> VRage.Scripting.<>c__DisplayClass27_0::syntaxTreeInjector
br.s L13
L10:
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass27_0 VRage.Scripting.<Compile>d__27::<>8__1
ldloc.1
ldftn Microsoft.CodeAnalysis.SyntaxTree VRage.Scripting.MyScriptCompiler::InjectResourceMonitoring(Microsoft.CodeAnalysis.CSharp.CSharpCompilation compilation, Microsoft.CodeAnalysis.SyntaxTree tree, System.Boolean trackMemoryUsage)
newobj System.Void System.Func`4<Microsoft.CodeAnalysis.CSharp.CSharpCompilation, Microsoft.CodeAnalysis.SyntaxTree, System.Boolean, Microsoft.CodeAnalysis.SyntaxTree>::.ctor(System.Object object, System.IntPtr method)
stfld System.Func`4<Microsoft.CodeAnalysis.CSharp.CSharpCompilation, Microsoft.CodeAnalysis.SyntaxTree, System.Boolean, Microsoft.CodeAnalysis.SyntaxTree> VRage.Scripting.<>c__DisplayClass27_0::syntaxTreeInjector
ldarg.0
ldloc.1
ldfld VRage.Scripting.Analyzers.WhitelistDiagnosticAnalyzer VRage.Scripting.MyScriptCompiler::m_inGameWhitelistDiagnosticAnalyzer
stfld Microsoft.CodeAnalysis.Diagnostics.DiagnosticAnalyzer VRage.Scripting.<Compile>d__27::<whitelistAnalyzer>5__2
br.s L14
L11:
ldstr "target"
ldarg.0
ldfld VRage.Scripting.MyApiTarget VRage.Scripting.<Compile>d__27::target
box VRage.Scripting.MyApiTarget
ldstr "Invalid compilation target"
newobj System.Void System.ArgumentOutOfRangeException::.ctor(System.String paramName, System.Object actualValue, System.String message)
throw
L12:
L13:
L14:
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass27_0 VRage.Scripting.<Compile>d__27::<>8__1
ldloc.1
ldarg.0
ldfld System.String VRage.Scripting.<Compile>d__27::assemblyName
ldarg.0
ldfld System.Collections.Generic.IEnumerable`1<VRage.Scripting.Script> VRage.Scripting.<Compile>d__27::scripts
ldarg.0
ldfld System.Boolean VRage.Scripting.<Compile>d__27::enableDebugInformation
call Microsoft.CodeAnalysis.CSharp.CSharpCompilation VRage.Scripting.MyScriptCompiler::CreateCompilation(System.String assemblyFileName, System.Collections.Generic.IEnumerable`1<VRage.Scripting.Script> scripts, System.Boolean enableDebugInformation)
stfld Microsoft.CodeAnalysis.CSharp.CSharpCompilation VRage.Scripting.<>c__DisplayClass27_0::compilation
ldloc.1
ldarg.0
ldfld VRage.Scripting.MyApiTarget VRage.Scripting.<Compile>d__27::target
ldarg.0
ldfld System.String VRage.Scripting.<Compile>d__27::assemblyName
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass27_0 VRage.Scripting.<Compile>d__27::<>8__1
ldfld Microsoft.CodeAnalysis.CSharp.CSharpCompilation VRage.Scripting.<>c__DisplayClass27_0::compilation
callvirt System.Collections.Immutable.ImmutableArray`1<Microsoft.CodeAnalysis.SyntaxTree> Microsoft.CodeAnalysis.CSharp.CSharpCompilation::get_SyntaxTrees()
box System.Collections.Immutable.ImmutableArray`1[Microsoft.CodeAnalysis.SyntaxTree]
ldnull
call System.Threading.Tasks.Task VRage.Scripting.MyScriptCompiler::WriteDiagnostics(VRage.Scripting.MyApiTarget target, System.String assemblyName, System.Collections.Generic.IList`1<Microsoft.CodeAnalysis.SyntaxTree> syntaxTrees, System.String suffix)
ldc.i4.0
callvirt System.Runtime.CompilerServices.ConfiguredTaskAwaitable System.Threading.Tasks.Task::ConfigureAwait(System.Boolean continueOnCapturedContext)
stloc.s 6 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable)
ldloca.s 6 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable)
call System.Runtime.CompilerServices.ConfiguredTaskAwaiter System.Runtime.CompilerServices.ConfiguredTaskAwaitable::GetAwaiter()
stloc.s 5 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter)
ldloca.s 5 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter)
call System.Boolean System.Runtime.CompilerServices.ConfiguredTaskAwaiter::get_IsCompleted()
brtrue.s L15
ldarg.0
ldc.i4.0
dup
stloc.0
stfld System.Int32 VRage.Scripting.<Compile>d__27::<>1__state
ldarg.0
ldloc.s 5 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter)
stfld System.Runtime.CompilerServices.ConfiguredTaskAwaiter VRage.Scripting.<Compile>d__27::<>u__1
ldarg.0
ldflda System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly> VRage.Scripting.<Compile>d__27::<>t__builder
ldloca.s 5 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter)
ldarg.0
call System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly>::AwaitUnsafeOnCompleted(System.Runtime.CompilerServices.ConfiguredTaskAwaiter& awaiter, VRage.Scripting.<Compile>d__27& stateMachine)
leave L16
L0:
ldarg.0
ldfld System.Runtime.CompilerServices.ConfiguredTaskAwaiter VRage.Scripting.<Compile>d__27::<>u__1
stloc.s 5 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter)
ldarg.0
ldflda System.Runtime.CompilerServices.ConfiguredTaskAwaiter VRage.Scripting.<Compile>d__27::<>u__1
initobj System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter
ldarg.0
ldc.i4.m1
dup
stloc.0
stfld System.Int32 VRage.Scripting.<Compile>d__27::<>1__state
L15:
ldloca.s 5 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter)
call System.Void System.Runtime.CompilerServices.ConfiguredTaskAwaiter::GetResult()
ldarg.0
ldc.i4.0
stfld System.Boolean VRage.Scripting.<Compile>d__27::<injectionFailed>5__3
ldarg.0
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass27_0 VRage.Scripting.<Compile>d__27::<>8__1
ldfld Microsoft.CodeAnalysis.CSharp.CSharpCompilation VRage.Scripting.<>c__DisplayClass27_0::compilation
stfld Microsoft.CodeAnalysis.CSharp.CSharpCompilation VRage.Scripting.<Compile>d__27::<compilationWithoutInjection>5__4
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass27_0 VRage.Scripting.<Compile>d__27::<>8__1
ldfld System.Func`4<Microsoft.CodeAnalysis.CSharp.CSharpCompilation, Microsoft.CodeAnalysis.SyntaxTree, System.Boolean, Microsoft.CodeAnalysis.SyntaxTree> VRage.Scripting.<>c__DisplayClass27_0::syntaxTreeInjector
brfalse L17
ldarg.0
ldnull
stfld Microsoft.CodeAnalysis.SyntaxTree[] VRage.Scripting.<Compile>d__27::<newSyntaxTrees>5__6
L1:
nop
[EX_BeginException]
ldloc.0
ldc.i4.1
beq L18
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass27_0 VRage.Scripting.<Compile>d__27::<>8__1
ldfld Microsoft.CodeAnalysis.CSharp.CSharpCompilation VRage.Scripting.<>c__DisplayClass27_0::compilation
callvirt System.Collections.Immutable.ImmutableArray`1<Microsoft.CodeAnalysis.SyntaxTree> Microsoft.CodeAnalysis.CSharp.CSharpCompilation::get_SyntaxTrees()
stloc.s 7 (System.Collections.Immutable.ImmutableArray`1[Microsoft.CodeAnalysis.SyntaxTree])
ldloca.s 7 (System.Collections.Immutable.ImmutableArray`1[Microsoft.CodeAnalysis.SyntaxTree])
call System.Int32 System.Collections.Immutable.ImmutableArray`1<Microsoft.CodeAnalysis.SyntaxTree>::get_Length()
ldc.i4.1
bne.un.s L19
ldarg.0
ldc.i4.1
newarr Microsoft.CodeAnalysis.SyntaxTree
dup
ldc.i4.0
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass27_0 VRage.Scripting.<Compile>d__27::<>8__1
ldfld System.Func`4<Microsoft.CodeAnalysis.CSharp.CSharpCompilation, Microsoft.CodeAnalysis.SyntaxTree, System.Boolean, Microsoft.CodeAnalysis.SyntaxTree> VRage.Scripting.<>c__DisplayClass27_0::syntaxTreeInjector
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass27_0 VRage.Scripting.<Compile>d__27::<>8__1
ldfld Microsoft.CodeAnalysis.CSharp.CSharpCompilation VRage.Scripting.<>c__DisplayClass27_0::compilation
ldloca.s 7 (System.Collections.Immutable.ImmutableArray`1[Microsoft.CodeAnalysis.SyntaxTree])
ldc.i4.0
call Microsoft.CodeAnalysis.SyntaxTree System.Collections.Immutable.ImmutableArray`1<Microsoft.CodeAnalysis.SyntaxTree>::get_Item(System.Int32 index)
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass27_0 VRage.Scripting.<Compile>d__27::<>8__1
ldfld System.Boolean VRage.Scripting.<>c__DisplayClass27_0::trackMemoryUsage
callvirt virtual Microsoft.CodeAnalysis.SyntaxTree System.Func`4<Microsoft.CodeAnalysis.CSharp.CSharpCompilation, Microsoft.CodeAnalysis.SyntaxTree, System.Boolean, Microsoft.CodeAnalysis.SyntaxTree>::Invoke(Microsoft.CodeAnalysis.CSharp.CSharpCompilation arg1, Microsoft.CodeAnalysis.SyntaxTree arg2, System.Boolean arg3)
stelem.ref
stfld Microsoft.CodeAnalysis.SyntaxTree[] VRage.Scripting.<Compile>d__27::<newSyntaxTrees>5__6
br L20
L19:
ldloc.s 7 (System.Collections.Immutable.ImmutableArray`1[Microsoft.CodeAnalysis.SyntaxTree])
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass27_0 VRage.Scripting.<Compile>d__27::<>8__1
ldftn System.Threading.Tasks.Task`1<Microsoft.CodeAnalysis.SyntaxTree> VRage.Scripting.<>c__DisplayClass27_0::<Compile>b__1(Microsoft.CodeAnalysis.SyntaxTree x)
newobj System.Void System.Func`2<Microsoft.CodeAnalysis.SyntaxTree, System.Threading.Tasks.Task`1<Microsoft.CodeAnalysis.SyntaxTree>>::.ctor(System.Object object, System.IntPtr method)
call static System.Collections.Generic.IEnumerable`1<System.Threading.Tasks.Task`1<Microsoft.CodeAnalysis.SyntaxTree>> System.Linq.ImmutableArrayExtensions::Select(System.Collections.Immutable.ImmutableArray`1<Microsoft.CodeAnalysis.SyntaxTree> immutableArray, System.Func`2<Microsoft.CodeAnalysis.SyntaxTree, System.Threading.Tasks.Task`1<Microsoft.CodeAnalysis.SyntaxTree>> selector)
call static System.Threading.Tasks.Task`1<Microsoft.CodeAnalysis.SyntaxTree[]> System.Threading.Tasks.Task::WhenAll(System.Collections.Generic.IEnumerable`1<System.Threading.Tasks.Task`1<Microsoft.CodeAnalysis.SyntaxTree>> tasks)
ldc.i4.0
callvirt System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1<Microsoft.CodeAnalysis.SyntaxTree[]> System.Threading.Tasks.Task`1<Microsoft.CodeAnalysis.SyntaxTree[]>::ConfigureAwait(System.Boolean continueOnCapturedContext)
stloc.s 10 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1[Microsoft.CodeAnalysis.SyntaxTree[]])
ldloca.s 10 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1[Microsoft.CodeAnalysis.SyntaxTree[]])
call System.Runtime.CompilerServices.ConfiguredTaskAwaiter<Microsoft.CodeAnalysis.SyntaxTree[]> System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1<Microsoft.CodeAnalysis.SyntaxTree[]>::GetAwaiter()
stloc.s 9 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[Microsoft.CodeAnalysis.SyntaxTree[]])
ldloca.s 9 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[Microsoft.CodeAnalysis.SyntaxTree[]])
call System.Boolean System.Runtime.CompilerServices.ConfiguredTaskAwaiter<Microsoft.CodeAnalysis.SyntaxTree[]>::get_IsCompleted()
brtrue.s L21
ldarg.0
ldc.i4.1
dup
stloc.0
stfld System.Int32 VRage.Scripting.<Compile>d__27::<>1__state
ldarg.0
ldloc.s 9 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[Microsoft.CodeAnalysis.SyntaxTree[]])
stfld System.Runtime.CompilerServices.ConfiguredTaskAwaiter<Microsoft.CodeAnalysis.SyntaxTree[]> VRage.Scripting.<Compile>d__27::<>u__2
ldarg.0
ldflda System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly> VRage.Scripting.<Compile>d__27::<>t__builder
ldloca.s 9 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[Microsoft.CodeAnalysis.SyntaxTree[]])
ldarg.0
call System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly>::AwaitUnsafeOnCompleted(System.Runtime.CompilerServices.ConfiguredTaskAwaiter& awaiter, VRage.Scripting.<Compile>d__27& stateMachine)
leave L22
L18:
ldarg.0
ldfld System.Runtime.CompilerServices.ConfiguredTaskAwaiter<Microsoft.CodeAnalysis.SyntaxTree[]> VRage.Scripting.<Compile>d__27::<>u__2
stloc.s 9 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[Microsoft.CodeAnalysis.SyntaxTree[]])
ldarg.0
ldflda System.Runtime.CompilerServices.ConfiguredTaskAwaiter<Microsoft.CodeAnalysis.SyntaxTree[]> VRage.Scripting.<Compile>d__27::<>u__2
initobj System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[Microsoft.CodeAnalysis.SyntaxTree[]]
ldarg.0
ldc.i4.m1
dup
stloc.0
stfld System.Int32 VRage.Scripting.<Compile>d__27::<>1__state
L21:
ldloca.s 9 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[Microsoft.CodeAnalysis.SyntaxTree[]])
call Microsoft.CodeAnalysis.SyntaxTree[] System.Runtime.CompilerServices.ConfiguredTaskAwaiter<Microsoft.CodeAnalysis.SyntaxTree[]>::GetResult()
stloc.s 8 (Microsoft.CodeAnalysis.SyntaxTree[])
ldarg.0
ldloc.s 8 (Microsoft.CodeAnalysis.SyntaxTree[])
stfld Microsoft.CodeAnalysis.SyntaxTree[] VRage.Scripting.<Compile>d__27::<newSyntaxTrees>5__6
L20:
leave.s L23
[EX_BeginCatch]
pop
ldarg.0
ldc.i4.1
stfld System.Boolean VRage.Scripting.<Compile>d__27::<injectionFailed>5__3
[EX_EndException]
leave.s L24
L23:
L24:
ldarg.0
ldfld System.Boolean VRage.Scripting.<Compile>d__27::<injectionFailed>5__3
brtrue L25
ldloc.1
ldarg.0
ldfld VRage.Scripting.MyApiTarget VRage.Scripting.<Compile>d__27::target
ldarg.0
ldfld System.String VRage.Scripting.<Compile>d__27::assemblyName
ldarg.0
ldfld Microsoft.CodeAnalysis.SyntaxTree[] VRage.Scripting.<Compile>d__27::<newSyntaxTrees>5__6
ldstr ".injected"
call System.Threading.Tasks.Task VRage.Scripting.MyScriptCompiler::WriteDiagnostics(VRage.Scripting.MyApiTarget target, System.String assemblyName, System.Collections.Generic.IList`1<Microsoft.CodeAnalysis.SyntaxTree> syntaxTrees, System.String suffix)
ldc.i4.0
callvirt System.Runtime.CompilerServices.ConfiguredTaskAwaitable System.Threading.Tasks.Task::ConfigureAwait(System.Boolean continueOnCapturedContext)
stloc.s 6 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable)
ldloca.s 6 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable)
call System.Runtime.CompilerServices.ConfiguredTaskAwaiter System.Runtime.CompilerServices.ConfiguredTaskAwaitable::GetAwaiter()
stloc.s 5 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter)
ldloca.s 5 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter)
call System.Boolean System.Runtime.CompilerServices.ConfiguredTaskAwaiter::get_IsCompleted()
brtrue.s L26
ldarg.0
ldc.i4.2
dup
stloc.0
stfld System.Int32 VRage.Scripting.<Compile>d__27::<>1__state
ldarg.0
ldloc.s 5 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter)
stfld System.Runtime.CompilerServices.ConfiguredTaskAwaiter VRage.Scripting.<Compile>d__27::<>u__1
ldarg.0
ldflda System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly> VRage.Scripting.<Compile>d__27::<>t__builder
ldloca.s 5 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter)
ldarg.0
call System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly>::AwaitUnsafeOnCompleted(System.Runtime.CompilerServices.ConfiguredTaskAwaiter& awaiter, VRage.Scripting.<Compile>d__27& stateMachine)
leave L27
L2:
ldarg.0
ldfld System.Runtime.CompilerServices.ConfiguredTaskAwaiter VRage.Scripting.<Compile>d__27::<>u__1
stloc.s 5 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter)
ldarg.0
ldflda System.Runtime.CompilerServices.ConfiguredTaskAwaiter VRage.Scripting.<Compile>d__27::<>u__1
initobj System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter
ldarg.0
ldc.i4.m1
dup
stloc.0
stfld System.Int32 VRage.Scripting.<Compile>d__27::<>1__state
L26:
ldloca.s 5 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter)
call System.Void System.Runtime.CompilerServices.ConfiguredTaskAwaiter::GetResult()
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass27_0 VRage.Scripting.<Compile>d__27::<>8__1
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass27_0 VRage.Scripting.<Compile>d__27::<>8__1
ldfld Microsoft.CodeAnalysis.CSharp.CSharpCompilation VRage.Scripting.<>c__DisplayClass27_0::compilation
callvirt Microsoft.CodeAnalysis.CSharp.CSharpCompilation Microsoft.CodeAnalysis.CSharp.CSharpCompilation::RemoveAllSyntaxTrees()
ldarg.0
ldfld Microsoft.CodeAnalysis.SyntaxTree[] VRage.Scripting.<Compile>d__27::<newSyntaxTrees>5__6
callvirt Microsoft.CodeAnalysis.CSharp.CSharpCompilation Microsoft.CodeAnalysis.CSharp.CSharpCompilation::AddSyntaxTrees(Microsoft.CodeAnalysis.SyntaxTree[] trees)
stfld Microsoft.CodeAnalysis.CSharp.CSharpCompilation VRage.Scripting.<>c__DisplayClass27_0::compilation
L25:
ldarg.0
ldnull
stfld Microsoft.CodeAnalysis.SyntaxTree[] VRage.Scripting.<Compile>d__27::<newSyntaxTrees>5__6
L17:
ldarg.0
ldnull
stfld Microsoft.CodeAnalysis.Diagnostics.CompilationWithAnalyzers VRage.Scripting.<Compile>d__27::<analyticCompilation>5__5
ldarg.0
ldfld Microsoft.CodeAnalysis.Diagnostics.DiagnosticAnalyzer VRage.Scripting.<Compile>d__27::<whitelistAnalyzer>5__2
brfalse.s L28
ldarg.0
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass27_0 VRage.Scripting.<Compile>d__27::<>8__1
ldfld Microsoft.CodeAnalysis.CSharp.CSharpCompilation VRage.Scripting.<>c__DisplayClass27_0::compilation
ldarg.0
ldfld Microsoft.CodeAnalysis.Diagnostics.DiagnosticAnalyzer VRage.Scripting.<Compile>d__27::<whitelistAnalyzer>5__2
call static System.Collections.Immutable.ImmutableArray`1<Microsoft.CodeAnalysis.Diagnostics.DiagnosticAnalyzer> System.Collections.Immutable.ImmutableArray::Create(Microsoft.CodeAnalysis.Diagnostics.DiagnosticAnalyzer item)
ldnull
ldloca.s 11 (System.Threading.CancellationToken)
initobj System.Threading.CancellationToken
ldloc.s 11 (System.Threading.CancellationToken)
call static Microsoft.CodeAnalysis.Diagnostics.CompilationWithAnalyzers Microsoft.CodeAnalysis.Diagnostics.DiagnosticAnalyzerExtensions::WithAnalyzers(Microsoft.CodeAnalysis.Compilation compilation, System.Collections.Immutable.ImmutableArray`1<Microsoft.CodeAnalysis.Diagnostics.DiagnosticAnalyzer> analyzers, Microsoft.CodeAnalysis.Diagnostics.AnalyzerOptions options, System.Threading.CancellationToken cancellationToken)
stfld Microsoft.CodeAnalysis.Diagnostics.CompilationWithAnalyzers VRage.Scripting.<Compile>d__27::<analyticCompilation>5__5
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass27_0 VRage.Scripting.<Compile>d__27::<>8__1
ldarg.0
ldfld Microsoft.CodeAnalysis.Diagnostics.CompilationWithAnalyzers VRage.Scripting.<Compile>d__27::<analyticCompilation>5__5
callvirt Microsoft.CodeAnalysis.Compilation Microsoft.CodeAnalysis.Diagnostics.CompilationWithAnalyzers::get_Compilation()
castclass Microsoft.CodeAnalysis.CSharp.CSharpCompilation
stfld Microsoft.CodeAnalysis.CSharp.CSharpCompilation VRage.Scripting.<>c__DisplayClass27_0::compilation
L28:
ldarg.0
newobj System.Void System.IO.MemoryStream::.ctor()
stfld System.IO.MemoryStream VRage.Scripting.<Compile>d__27::<pdbStream>5__7
L3:
L4:
L5:
L6:
nop
[EX_BeginException]
ldloc.0
ldc.i4.3
sub
ldc.i4.3
ble.un.s L29
ldarg.0
newobj System.Void System.IO.MemoryStream::.ctor()
stfld System.IO.MemoryStream VRage.Scripting.<Compile>d__27::<assemblyStream>5__8
L29:
nop
[EX_BeginException]
ldloc.0
ldc.i4.3
sub
switch L30, L31, L32, L33
ldarg.0
ldc.i4.0
stfld System.Boolean VRage.Scripting.<Compile>d__27::<loadPDBs>5__9
ldarg.0
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass27_0 VRage.Scripting.<Compile>d__27::<>8__1
ldfld Microsoft.CodeAnalysis.CSharp.CSharpCompilation VRage.Scripting.<>c__DisplayClass27_0::compilation
ldarg.0
ldfld System.IO.MemoryStream VRage.Scripting.<Compile>d__27::<assemblyStream>5__8
ldarg.0
ldfld System.IO.MemoryStream VRage.Scripting.<Compile>d__27::<pdbStream>5__7
ldnull
ldnull
ldnull
ldnull
ldnull
ldnull
ldnull
ldnull
ldloca.s 11 (System.Threading.CancellationToken)
initobj System.Threading.CancellationToken
ldloc.s 11 (System.Threading.CancellationToken)
callvirt Microsoft.CodeAnalysis.Emit.EmitResult Microsoft.CodeAnalysis.Compilation::Emit(System.IO.Stream peStream, System.IO.Stream pdbStream, System.IO.Stream xmlDocumentationStream, System.IO.Stream win32Resources, System.Collections.Generic.IEnumerable`1<Microsoft.CodeAnalysis.ResourceDescription> manifestResources, Microsoft.CodeAnalysis.Emit.EmitOptions options, Microsoft.CodeAnalysis.IMethodSymbol debugEntryPoint, System.IO.Stream sourceLinkStream, System.Collections.Generic.IEnumerable`1<Microsoft.CodeAnalysis.EmbeddedText> embeddedTexts, System.IO.Stream metadataPEStream, System.Threading.CancellationToken cancellationToken)
stfld Microsoft.CodeAnalysis.Emit.EmitResult VRage.Scripting.<Compile>d__27::<result>5__10
ldarg.0
ldarg.0
ldfld Microsoft.CodeAnalysis.Emit.EmitResult VRage.Scripting.<Compile>d__27::<result>5__10
callvirt System.Boolean Microsoft.CodeAnalysis.Emit.EmitResult::get_Success()
stfld System.Boolean VRage.Scripting.<Compile>d__27::<success>5__11
ldarg.0
newobj System.Void VRage.Scripting.MyBlacklistSyntaxVisitor::.ctor()
stfld VRage.Scripting.MyBlacklistSyntaxVisitor VRage.Scripting.<Compile>d__27::<visitor>5__12
ldarg.0
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass27_0 VRage.Scripting.<Compile>d__27::<>8__1
ldfld Microsoft.CodeAnalysis.CSharp.CSharpCompilation VRage.Scripting.<>c__DisplayClass27_0::compilation
callvirt System.Collections.Immutable.ImmutableArray`1<Microsoft.CodeAnalysis.SyntaxTree> Microsoft.CodeAnalysis.CSharp.CSharpCompilation::get_SyntaxTrees()
stloc.s 12 (System.Collections.Immutable.ImmutableArray`1[Microsoft.CodeAnalysis.SyntaxTree])
ldloca.s 12 (System.Collections.Immutable.ImmutableArray`1[Microsoft.CodeAnalysis.SyntaxTree])
call System.Collections.Immutable.Enumerator<Microsoft.CodeAnalysis.SyntaxTree> System.Collections.Immutable.ImmutableArray`1<Microsoft.CodeAnalysis.SyntaxTree>::GetEnumerator()
stfld System.Collections.Immutable.Enumerator<Microsoft.CodeAnalysis.SyntaxTree> VRage.Scripting.<Compile>d__27::<>7__wrap12
br L34
L37:
ldarg.0
ldflda System.Collections.Immutable.Enumerator<Microsoft.CodeAnalysis.SyntaxTree> VRage.Scripting.<Compile>d__27::<>7__wrap12
call Microsoft.CodeAnalysis.SyntaxTree System.Collections.Immutable.Enumerator<Microsoft.CodeAnalysis.SyntaxTree>::get_Current()
stloc.s 13 (Microsoft.CodeAnalysis.SyntaxTree)
ldarg.0
ldfld VRage.Scripting.MyBlacklistSyntaxVisitor VRage.Scripting.<Compile>d__27::<visitor>5__12
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass27_0 VRage.Scripting.<Compile>d__27::<>8__1
ldfld Microsoft.CodeAnalysis.CSharp.CSharpCompilation VRage.Scripting.<>c__DisplayClass27_0::compilation
ldloc.s 13 (Microsoft.CodeAnalysis.SyntaxTree)
ldc.i4.0
callvirt Microsoft.CodeAnalysis.SemanticModel Microsoft.CodeAnalysis.Compilation::GetSemanticModel(Microsoft.CodeAnalysis.SyntaxTree syntaxTree, System.Boolean ignoreAccessibility)
callvirt System.Void VRage.Scripting.MyBlacklistSyntaxVisitor::SetSemanticModel(Microsoft.CodeAnalysis.SemanticModel model)
ldarg.0
ldarg.0
ldfld VRage.Scripting.MyBlacklistSyntaxVisitor VRage.Scripting.<Compile>d__27::<visitor>5__12
stfld VRage.Scripting.MyBlacklistSyntaxVisitor VRage.Scripting.<Compile>d__27::<>7__wrap13
ldloc.s 13 (Microsoft.CodeAnalysis.SyntaxTree)
ldloca.s 11 (System.Threading.CancellationToken)
initobj System.Threading.CancellationToken
ldloc.s 11 (System.Threading.CancellationToken)
callvirt System.Threading.Tasks.Task`1<Microsoft.CodeAnalysis.SyntaxNode> Microsoft.CodeAnalysis.SyntaxTree::GetRootAsync(System.Threading.CancellationToken cancellationToken)
callvirt System.Runtime.CompilerServices.TaskAwaiter`1<Microsoft.CodeAnalysis.SyntaxNode> System.Threading.Tasks.Task`1<Microsoft.CodeAnalysis.SyntaxNode>::GetAwaiter()
stloc.s 15 (System.Runtime.CompilerServices.TaskAwaiter`1[Microsoft.CodeAnalysis.SyntaxNode])
ldloca.s 15 (System.Runtime.CompilerServices.TaskAwaiter`1[Microsoft.CodeAnalysis.SyntaxNode])
call System.Boolean System.Runtime.CompilerServices.TaskAwaiter`1<Microsoft.CodeAnalysis.SyntaxNode>::get_IsCompleted()
brtrue.s L35
ldarg.0
ldc.i4.3
dup
stloc.0
stfld System.Int32 VRage.Scripting.<Compile>d__27::<>1__state
ldarg.0
ldloc.s 15 (System.Runtime.CompilerServices.TaskAwaiter`1[Microsoft.CodeAnalysis.SyntaxNode])
stfld System.Runtime.CompilerServices.TaskAwaiter`1<Microsoft.CodeAnalysis.SyntaxNode> VRage.Scripting.<Compile>d__27::<>u__3
ldarg.0
ldflda System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly> VRage.Scripting.<Compile>d__27::<>t__builder
ldloca.s 15 (System.Runtime.CompilerServices.TaskAwaiter`1[Microsoft.CodeAnalysis.SyntaxNode])
ldarg.0
call System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly>::AwaitUnsafeOnCompleted(System.Runtime.CompilerServices.TaskAwaiter`1& awaiter, VRage.Scripting.<Compile>d__27& stateMachine)
leave L36
L30:
ldarg.0
ldfld System.Runtime.CompilerServices.TaskAwaiter`1<Microsoft.CodeAnalysis.SyntaxNode> VRage.Scripting.<Compile>d__27::<>u__3
stloc.s 15 (System.Runtime.CompilerServices.TaskAwaiter`1[Microsoft.CodeAnalysis.SyntaxNode])
ldarg.0
ldflda System.Runtime.CompilerServices.TaskAwaiter`1<Microsoft.CodeAnalysis.SyntaxNode> VRage.Scripting.<Compile>d__27::<>u__3
initobj System.Runtime.CompilerServices.TaskAwaiter`1[Microsoft.CodeAnalysis.SyntaxNode]
ldarg.0
ldc.i4.m1
dup
stloc.0
stfld System.Int32 VRage.Scripting.<Compile>d__27::<>1__state
L35:
ldloca.s 15 (System.Runtime.CompilerServices.TaskAwaiter`1[Microsoft.CodeAnalysis.SyntaxNode])
call Microsoft.CodeAnalysis.SyntaxNode System.Runtime.CompilerServices.TaskAwaiter`1<Microsoft.CodeAnalysis.SyntaxNode>::GetResult()
stloc.s 14 (Microsoft.CodeAnalysis.SyntaxNode)
ldarg.0
ldfld VRage.Scripting.MyBlacklistSyntaxVisitor VRage.Scripting.<Compile>d__27::<>7__wrap13
ldloc.s 14 (Microsoft.CodeAnalysis.SyntaxNode)
callvirt virtual System.Void Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor::Visit(Microsoft.CodeAnalysis.SyntaxNode node)
ldarg.0
ldnull
stfld VRage.Scripting.MyBlacklistSyntaxVisitor VRage.Scripting.<Compile>d__27::<>7__wrap13
L34:
ldarg.0
ldflda System.Collections.Immutable.Enumerator<Microsoft.CodeAnalysis.SyntaxTree> VRage.Scripting.<Compile>d__27::<>7__wrap12
call System.Boolean System.Collections.Immutable.Enumerator<Microsoft.CodeAnalysis.SyntaxTree>::MoveNext()
brtrue L37
ldarg.0
ldflda System.Collections.Immutable.Enumerator<Microsoft.CodeAnalysis.SyntaxTree> VRage.Scripting.<Compile>d__27::<>7__wrap12
initobj System.Collections.Immutable.ImmutableArray`1+Enumerator[Microsoft.CodeAnalysis.SyntaxTree]
ldarg.0
ldfld VRage.Scripting.MyBlacklistSyntaxVisitor VRage.Scripting.<Compile>d__27::<visitor>5__12
callvirt System.Boolean VRage.Scripting.MyBlacklistSyntaxVisitor::HasAnyResult()
brfalse.s L38
ldarg.0
ldfld VRage.Scripting.MyBlacklistSyntaxVisitor VRage.Scripting.<Compile>d__27::<visitor>5__12
ldarg.0
ldfld System.Collections.Generic.List`1<VRage.Scripting.Message> VRage.Scripting.<Compile>d__27::messages
callvirt System.Void VRage.Scripting.MyBlacklistSyntaxVisitor::GetResultMessages(System.Collections.Generic.List`1<VRage.Scripting.Message> results)
ldnull
stloc.2
leave L39
L38:
ldloc.1
ldarg.0
ldfld Microsoft.CodeAnalysis.Diagnostics.CompilationWithAnalyzers VRage.Scripting.<Compile>d__27::<analyticCompilation>5__5
ldarg.0
ldfld Microsoft.CodeAnalysis.Emit.EmitResult VRage.Scripting.<Compile>d__27::<result>5__10
ldarg.0
ldfld System.Collections.Generic.List`1<VRage.Scripting.Message> VRage.Scripting.<Compile>d__27::messages
ldarg.0
ldfld System.Boolean VRage.Scripting.<Compile>d__27::<success>5__11
call System.Threading.Tasks.Task`1<System.Boolean> VRage.Scripting.MyScriptCompiler::EmitDiagnostics(Microsoft.CodeAnalysis.Diagnostics.CompilationWithAnalyzers analyticCompilation, Microsoft.CodeAnalysis.Emit.EmitResult result, System.Collections.Generic.List`1<VRage.Scripting.Message> messages, System.Boolean success)
ldc.i4.0
callvirt System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1<System.Boolean> System.Threading.Tasks.Task`1<System.Boolean>::ConfigureAwait(System.Boolean continueOnCapturedContext)
stloc.s 18 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1[System.Boolean])
ldloca.s 18 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1[System.Boolean])
call System.Runtime.CompilerServices.ConfiguredTaskAwaiter<System.Boolean> System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1<System.Boolean>::GetAwaiter()
stloc.s 17 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[System.Boolean])
ldloca.s 17 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[System.Boolean])
call System.Boolean System.Runtime.CompilerServices.ConfiguredTaskAwaiter<System.Boolean>::get_IsCompleted()
brtrue.s L40
ldarg.0
ldc.i4.4
dup
stloc.0
stfld System.Int32 VRage.Scripting.<Compile>d__27::<>1__state
ldarg.0
ldloc.s 17 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[System.Boolean])
stfld System.Runtime.CompilerServices.ConfiguredTaskAwaiter<System.Boolean> VRage.Scripting.<Compile>d__27::<>u__4
ldarg.0
ldflda System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly> VRage.Scripting.<Compile>d__27::<>t__builder
ldloca.s 17 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[System.Boolean])
ldarg.0
call System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly>::AwaitUnsafeOnCompleted(System.Runtime.CompilerServices.ConfiguredTaskAwaiter& awaiter, VRage.Scripting.<Compile>d__27& stateMachine)
leave L41
L31:
ldarg.0
ldfld System.Runtime.CompilerServices.ConfiguredTaskAwaiter<System.Boolean> VRage.Scripting.<Compile>d__27::<>u__4
stloc.s 17 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[System.Boolean])
ldarg.0
ldflda System.Runtime.CompilerServices.ConfiguredTaskAwaiter<System.Boolean> VRage.Scripting.<Compile>d__27::<>u__4
initobj System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[System.Boolean]
ldarg.0
ldc.i4.m1
dup
stloc.0
stfld System.Int32 VRage.Scripting.<Compile>d__27::<>1__state
L40:
ldloca.s 17 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[System.Boolean])
call System.Boolean System.Runtime.CompilerServices.ConfiguredTaskAwaiter<System.Boolean>::GetResult()
stloc.s 16 (System.Boolean)
ldarg.0
ldloc.s 16 (System.Boolean)
stfld System.Boolean VRage.Scripting.<Compile>d__27::<success>5__11
ldloc.1
ldarg.0
ldfld VRage.Scripting.MyApiTarget VRage.Scripting.<Compile>d__27::target
ldarg.0
ldfld System.String VRage.Scripting.<Compile>d__27::assemblyName
ldarg.0
ldfld System.Collections.Generic.List`1<VRage.Scripting.Message> VRage.Scripting.<Compile>d__27::messages
ldarg.0
ldfld System.Boolean VRage.Scripting.<Compile>d__27::<success>5__11
call System.Threading.Tasks.Task VRage.Scripting.MyScriptCompiler::WriteDiagnostics(VRage.Scripting.MyApiTarget target, System.String assemblyName, System.Collections.Generic.IEnumerable`1<VRage.Scripting.Message> messages, System.Boolean success)
ldc.i4.0
callvirt System.Runtime.CompilerServices.ConfiguredTaskAwaitable System.Threading.Tasks.Task::ConfigureAwait(System.Boolean continueOnCapturedContext)
stloc.s 6 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable)
ldloca.s 6 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable)
call System.Runtime.CompilerServices.ConfiguredTaskAwaiter System.Runtime.CompilerServices.ConfiguredTaskAwaitable::GetAwaiter()
stloc.s 5 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter)
ldloca.s 5 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter)
call System.Boolean System.Runtime.CompilerServices.ConfiguredTaskAwaiter::get_IsCompleted()
brtrue.s L42
ldarg.0
ldc.i4.5
dup
stloc.0
stfld System.Int32 VRage.Scripting.<Compile>d__27::<>1__state
ldarg.0
ldloc.s 5 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter)
stfld System.Runtime.CompilerServices.ConfiguredTaskAwaiter VRage.Scripting.<Compile>d__27::<>u__1
ldarg.0
ldflda System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly> VRage.Scripting.<Compile>d__27::<>t__builder
ldloca.s 5 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter)
ldarg.0
call System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly>::AwaitUnsafeOnCompleted(System.Runtime.CompilerServices.ConfiguredTaskAwaiter& awaiter, VRage.Scripting.<Compile>d__27& stateMachine)
leave L43
L32:
ldarg.0
ldfld System.Runtime.CompilerServices.ConfiguredTaskAwaiter VRage.Scripting.<Compile>d__27::<>u__1
stloc.s 5 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter)
ldarg.0
ldflda System.Runtime.CompilerServices.ConfiguredTaskAwaiter VRage.Scripting.<Compile>d__27::<>u__1
initobj System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter
ldarg.0
ldc.i4.m1
dup
stloc.0
stfld System.Int32 VRage.Scripting.<Compile>d__27::<>1__state
L42:
ldloca.s 5 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter)
call System.Void System.Runtime.CompilerServices.ConfiguredTaskAwaiter::GetResult()
ldarg.0
ldfld System.IO.MemoryStream VRage.Scripting.<Compile>d__27::<pdbStream>5__7
ldc.i4.0
conv.i8
ldc.i4.0
callvirt abstract virtual System.Int64 System.IO.Stream::Seek(System.Int64 offset, System.IO.SeekOrigin origin)
pop
ldarg.0
ldfld System.IO.MemoryStream VRage.Scripting.<Compile>d__27::<assemblyStream>5__8
ldc.i4.0
conv.i8
ldc.i4.0
callvirt abstract virtual System.Int64 System.IO.Stream::Seek(System.Int64 offset, System.IO.SeekOrigin origin)
pop
ldarg.0
ldfld System.Boolean VRage.Scripting.<Compile>d__27::<injectionFailed>5__3
brtrue L44
ldarg.0
ldfld System.Boolean VRage.Scripting.<Compile>d__27::<success>5__11
brfalse.s L45
ldarg.0
ldfld System.Boolean VRage.Scripting.<Compile>d__27::<loadPDBs>5__9
brtrue.s L46
ldarg.0
ldfld System.IO.MemoryStream VRage.Scripting.<Compile>d__27::<assemblyStream>5__8
callvirt virtual System.Byte[] System.IO.MemoryStream::ToArray()
call static System.Reflection.Assembly System.Reflection.Assembly::Load(System.Byte[] rawAssembly)
br.s L47
L46:
ldarg.0
ldfld System.IO.MemoryStream VRage.Scripting.<Compile>d__27::<assemblyStream>5__8
callvirt virtual System.Byte[] System.IO.MemoryStream::ToArray()
ldarg.0
ldfld System.IO.MemoryStream VRage.Scripting.<Compile>d__27::<pdbStream>5__7
callvirt virtual System.Byte[] System.IO.MemoryStream::ToArray()
call static System.Reflection.Assembly System.Reflection.Assembly::Load(System.Byte[] rawAssembly, System.Byte[] rawSymbolStore)
L47:
stloc.2
leave L48
L45:
ldarg.0
ldarg.0
ldfld Microsoft.CodeAnalysis.CSharp.CSharpCompilation VRage.Scripting.<Compile>d__27::<compilationWithoutInjection>5__4
ldarg.0
ldfld System.IO.MemoryStream VRage.Scripting.<Compile>d__27::<assemblyStream>5__8
ldnull
ldnull
ldnull
ldnull
ldnull
ldnull
ldnull
ldnull
ldnull
ldloca.s 11 (System.Threading.CancellationToken)
initobj System.Threading.CancellationToken
ldloc.s 11 (System.Threading.CancellationToken)
callvirt Microsoft.CodeAnalysis.Emit.EmitResult Microsoft.CodeAnalysis.Compilation::Emit(System.IO.Stream peStream, System.IO.Stream pdbStream, System.IO.Stream xmlDocumentationStream, System.IO.Stream win32Resources, System.Collections.Generic.IEnumerable`1<Microsoft.CodeAnalysis.ResourceDescription> manifestResources, Microsoft.CodeAnalysis.Emit.EmitOptions options, Microsoft.CodeAnalysis.IMethodSymbol debugEntryPoint, System.IO.Stream sourceLinkStream, System.Collections.Generic.IEnumerable`1<Microsoft.CodeAnalysis.EmbeddedText> embeddedTexts, System.IO.Stream metadataPEStream, System.Threading.CancellationToken cancellationToken)
stfld Microsoft.CodeAnalysis.Emit.EmitResult VRage.Scripting.<Compile>d__27::<result>5__10
ldloc.1
ldarg.0
ldfld Microsoft.CodeAnalysis.Diagnostics.CompilationWithAnalyzers VRage.Scripting.<Compile>d__27::<analyticCompilation>5__5
ldarg.0
ldfld Microsoft.CodeAnalysis.Emit.EmitResult VRage.Scripting.<Compile>d__27::<result>5__10
ldarg.0
ldfld System.Collections.Generic.List`1<VRage.Scripting.Message> VRage.Scripting.<Compile>d__27::messages
ldc.i4.0
call System.Threading.Tasks.Task`1<System.Boolean> VRage.Scripting.MyScriptCompiler::EmitDiagnostics(Microsoft.CodeAnalysis.Diagnostics.CompilationWithAnalyzers analyticCompilation, Microsoft.CodeAnalysis.Emit.EmitResult result, System.Collections.Generic.List`1<VRage.Scripting.Message> messages, System.Boolean success)
ldc.i4.0
callvirt System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1<System.Boolean> System.Threading.Tasks.Task`1<System.Boolean>::ConfigureAwait(System.Boolean continueOnCapturedContext)
stloc.s 18 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1[System.Boolean])
ldloca.s 18 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1[System.Boolean])
call System.Runtime.CompilerServices.ConfiguredTaskAwaiter<System.Boolean> System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1<System.Boolean>::GetAwaiter()
stloc.s 17 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[System.Boolean])
ldloca.s 17 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[System.Boolean])
call System.Boolean System.Runtime.CompilerServices.ConfiguredTaskAwaiter<System.Boolean>::get_IsCompleted()
brtrue.s L49
ldarg.0
ldc.i4.6
dup
stloc.0
stfld System.Int32 VRage.Scripting.<Compile>d__27::<>1__state
ldarg.0
ldloc.s 17 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[System.Boolean])
stfld System.Runtime.CompilerServices.ConfiguredTaskAwaiter<System.Boolean> VRage.Scripting.<Compile>d__27::<>u__4
ldarg.0
ldflda System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly> VRage.Scripting.<Compile>d__27::<>t__builder
ldloca.s 17 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[System.Boolean])
ldarg.0
call System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly>::AwaitUnsafeOnCompleted(System.Runtime.CompilerServices.ConfiguredTaskAwaiter& awaiter, VRage.Scripting.<Compile>d__27& stateMachine)
leave L50
L33:
ldarg.0
ldfld System.Runtime.CompilerServices.ConfiguredTaskAwaiter<System.Boolean> VRage.Scripting.<Compile>d__27::<>u__4
stloc.s 17 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[System.Boolean])
ldarg.0
ldflda System.Runtime.CompilerServices.ConfiguredTaskAwaiter<System.Boolean> VRage.Scripting.<Compile>d__27::<>u__4
initobj System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[System.Boolean]
ldarg.0
ldc.i4.m1
dup
stloc.0
stfld System.Int32 VRage.Scripting.<Compile>d__27::<>1__state
L49:
ldloca.s 17 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[System.Boolean])
call System.Boolean System.Runtime.CompilerServices.ConfiguredTaskAwaiter<System.Boolean>::GetResult()
pop
L44:
ldnull
stloc.2
leave.s L51
[EX_BeginFinally]
ldloc.0
ldc.i4.0
bge.s L52
ldarg.0
ldfld System.IO.MemoryStream VRage.Scripting.<Compile>d__27::<assemblyStream>5__8
brfalse.s L53
ldarg.0
ldfld System.IO.MemoryStream VRage.Scripting.<Compile>d__27::<assemblyStream>5__8
callvirt abstract virtual System.Void System.IDisposable::Dispose()
L52:
L53:
[EX_EndException]
endfinally
[EX_BeginFinally]
ldloc.0
ldc.i4.0
bge.s L54
ldarg.0
ldfld System.IO.MemoryStream VRage.Scripting.<Compile>d__27::<pdbStream>5__7
brfalse.s L55
ldarg.0
ldfld System.IO.MemoryStream VRage.Scripting.<Compile>d__27::<pdbStream>5__7
callvirt abstract virtual System.Void System.IDisposable::Dispose()
L54:
L55:
[EX_EndException]
endfinally
[EX_BeginCatch]
stloc.s 19 (System.Exception)
ldarg.0
ldc.i4.s -2
stfld System.Int32 VRage.Scripting.<Compile>d__27::<>1__state
ldarg.0
ldflda System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly> VRage.Scripting.<Compile>d__27::<>t__builder
ldloc.s 19 (System.Exception)
call System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly>::SetException(System.Exception exception)
[EX_EndException]
leave.s L56
L39:
L48:
L51:
ldarg.0
ldc.i4.s -2
stfld System.Int32 VRage.Scripting.<Compile>d__27::<>1__state
ldarg.0
ldflda System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly> VRage.Scripting.<Compile>d__27::<>t__builder
ldloc.2
call System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly>::SetResult(System.Reflection.Assembly result)
L16:
L22:
L27:
L36:
L41:
L43:
L50:
L56:
ret
