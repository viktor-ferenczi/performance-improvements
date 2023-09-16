// f0c1c3d6
ldarg.0
ldfld System.Int32 VRage.Scripting.<Compile>d__37::<>1__state
stloc.0
ldarg.0
ldfld VRage.Scripting.MyScriptCompiler VRage.Scripting.<Compile>d__37::<>4__this
stloc.1
ldloc.1
ldarg.0
ldfld VRage.Scripting.MyApiTarget VRage.Scripting.<Compile>d__37::target
ldarg.0
ldflda System.Collections.Generic.IEnumerable`1<VRage.Scripting.Script> VRage.Scripting.<Compile>d__37::scripts
ldarg.0
ldfld System.String VRage.Scripting.<Compile>d__37::friendlyName
call static System.Reflection.Assembly Shared.Patches.MyScriptCompilerPatch::RecallFromCache(VRage.Scripting.MyScriptCompiler myScriptCompiler, VRage.Scripting.MyApiTarget target, System.Collections.Generic.IEnumerable`1& scripts, System.String friendlyName)
dup
stloc.2
brtrue.s L35
[EX_BeginException]
ldloc.0
switch L0, L1, L2, L3, L4, L5
ldarg.0
newobj System.Void VRage.Scripting.<>c__DisplayClass37_0::.ctor()
stfld VRage.Scripting.<>c__DisplayClass37_0 VRage.Scripting.<Compile>d__37::<>8__1
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass37_0 VRage.Scripting.<Compile>d__37::<>8__1
ldarg.0
ldfld VRage.Scripting.MyScriptCompiler VRage.Scripting.<Compile>d__37::<>4__this
stfld VRage.Scripting.MyScriptCompiler VRage.Scripting.<>c__DisplayClass37_0::<>4__this
ldarg.0
ldfld System.String VRage.Scripting.<Compile>d__37::friendlyName
brtrue.s L6
ldarg.0
ldstr "<No Name>"
stfld System.String VRage.Scripting.<Compile>d__37::friendlyName
L6:
ldarg.0
ldfld VRage.Scripting.MyApiTarget VRage.Scripting.<Compile>d__37::target
stloc.3
ldloc.3
switch L7, L8, L9
br L10
L7:
ldarg.0
ldnull
stfld Microsoft.CodeAnalysis.Diagnostics.DiagnosticAnalyzer VRage.Scripting.<Compile>d__37::<whitelistAnalyzer>5__2
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass37_0 VRage.Scripting.<Compile>d__37::<>8__1
ldnull
stfld System.Func`3<Microsoft.CodeAnalysis.CSharp.CSharpCompilation, Microsoft.CodeAnalysis.SyntaxTree, Microsoft.CodeAnalysis.SyntaxTree> VRage.Scripting.<>c__DisplayClass37_0::syntaxTreeInjector
br L11
L8:
newobj System.Void VRage.Scripting.<>c__DisplayClass37_1::.ctor()
stloc.s 4 (VRage.Scripting.MyScriptCompiler+<>c__DisplayClass37_1)
ldloc.s 4 (VRage.Scripting.MyScriptCompiler+<>c__DisplayClass37_1)
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass37_0 VRage.Scripting.<Compile>d__37::<>8__1
stfld VRage.Scripting.<>c__DisplayClass37_0 VRage.Scripting.<>c__DisplayClass37_1::CS$<>8__locals1
ldloc.s 4 (VRage.Scripting.MyScriptCompiler+<>c__DisplayClass37_1)
ldarg.0
ldfld System.String VRage.Scripting.<Compile>d__37::friendlyName
call static System.Int32 VRage.Scripting.MyModWatchdog::AllocateModId(System.String modName)
stfld System.Int32 VRage.Scripting.<>c__DisplayClass37_1::modId
ldarg.0
ldloc.1
ldfld VRage.Scripting.Analyzers.WhitelistDiagnosticAnalyzer VRage.Scripting.MyScriptCompiler::m_modApiWhitelistDiagnosticAnalyzer
stfld Microsoft.CodeAnalysis.Diagnostics.DiagnosticAnalyzer VRage.Scripting.<Compile>d__37::<whitelistAnalyzer>5__2
ldloc.s 4 (VRage.Scripting.MyScriptCompiler+<>c__DisplayClass37_1)
ldfld VRage.Scripting.<>c__DisplayClass37_0 VRage.Scripting.<>c__DisplayClass37_1::CS$<>8__locals1
ldloc.s 4 (VRage.Scripting.MyScriptCompiler+<>c__DisplayClass37_1)
ldftn Microsoft.CodeAnalysis.SyntaxTree VRage.Scripting.<>c__DisplayClass37_1::<Compile>b__0(Microsoft.CodeAnalysis.CSharp.CSharpCompilation c, Microsoft.CodeAnalysis.SyntaxTree st)
newobj System.Void System.Func`3<Microsoft.CodeAnalysis.CSharp.CSharpCompilation, Microsoft.CodeAnalysis.SyntaxTree, Microsoft.CodeAnalysis.SyntaxTree>::.ctor(System.Object object, System.IntPtr method)
stfld System.Func`3<Microsoft.CodeAnalysis.CSharp.CSharpCompilation, Microsoft.CodeAnalysis.SyntaxTree, Microsoft.CodeAnalysis.SyntaxTree> VRage.Scripting.<>c__DisplayClass37_0::syntaxTreeInjector
br.s L12
L9:
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass37_0 VRage.Scripting.<Compile>d__37::<>8__1
ldloc.1
ldftn Microsoft.CodeAnalysis.SyntaxTree VRage.Scripting.MyScriptCompiler::InjectInstructionCounter(Microsoft.CodeAnalysis.CSharp.CSharpCompilation compilation, Microsoft.CodeAnalysis.SyntaxTree tree)
newobj System.Void System.Func`3<Microsoft.CodeAnalysis.CSharp.CSharpCompilation, Microsoft.CodeAnalysis.SyntaxTree, Microsoft.CodeAnalysis.SyntaxTree>::.ctor(System.Object object, System.IntPtr method)
stfld System.Func`3<Microsoft.CodeAnalysis.CSharp.CSharpCompilation, Microsoft.CodeAnalysis.SyntaxTree, Microsoft.CodeAnalysis.SyntaxTree> VRage.Scripting.<>c__DisplayClass37_0::syntaxTreeInjector
ldarg.0
ldloc.1
ldfld VRage.Scripting.Analyzers.WhitelistDiagnosticAnalyzer VRage.Scripting.MyScriptCompiler::m_ingameWhitelistDiagnosticAnalyzer
stfld Microsoft.CodeAnalysis.Diagnostics.DiagnosticAnalyzer VRage.Scripting.<Compile>d__37::<whitelistAnalyzer>5__2
br.s L13
L10:
ldstr "target"
ldarg.0
ldfld VRage.Scripting.MyApiTarget VRage.Scripting.<Compile>d__37::target
box VRage.Scripting.MyApiTarget
ldstr "Invalid compilation target"
newobj System.Void System.ArgumentOutOfRangeException::.ctor(System.String paramName, System.Object actualValue, System.String message)
throw
L11:
L12:
L13:
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass37_0 VRage.Scripting.<Compile>d__37::<>8__1
ldloc.1
ldarg.0
ldfld System.String VRage.Scripting.<Compile>d__37::assemblyName
ldarg.0
ldfld System.Collections.Generic.IEnumerable`1<VRage.Scripting.Script> VRage.Scripting.<Compile>d__37::scripts
ldarg.0
ldfld System.Boolean VRage.Scripting.<Compile>d__37::enableDebugInformation
call Microsoft.CodeAnalysis.CSharp.CSharpCompilation VRage.Scripting.MyScriptCompiler::CreateCompilation(System.String assemblyFileName, System.Collections.Generic.IEnumerable`1<VRage.Scripting.Script> scripts, System.Boolean enableDebugInformation)
stfld Microsoft.CodeAnalysis.CSharp.CSharpCompilation VRage.Scripting.<>c__DisplayClass37_0::compilation
ldloc.1
ldarg.0
ldfld VRage.Scripting.MyApiTarget VRage.Scripting.<Compile>d__37::target
ldarg.0
ldfld System.String VRage.Scripting.<Compile>d__37::assemblyName
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass37_0 VRage.Scripting.<Compile>d__37::<>8__1
ldfld Microsoft.CodeAnalysis.CSharp.CSharpCompilation VRage.Scripting.<>c__DisplayClass37_0::compilation
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
brtrue.s L14
ldarg.0
ldc.i4.0
dup
stloc.0
stfld System.Int32 VRage.Scripting.<Compile>d__37::<>1__state
ldarg.0
ldloc.s 5 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter)
stfld System.Runtime.CompilerServices.ConfiguredTaskAwaiter VRage.Scripting.<Compile>d__37::<>u__1
ldarg.0
ldflda System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly> VRage.Scripting.<Compile>d__37::<>t__builder
ldloca.s 5 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter)
ldarg.0
call System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly>::AwaitUnsafeOnCompleted(System.Runtime.CompilerServices.ConfiguredTaskAwaiter& awaiter, VRage.Scripting.<Compile>d__37& stateMachine)
leave L15
L0:
ldarg.0
ldfld System.Runtime.CompilerServices.ConfiguredTaskAwaiter VRage.Scripting.<Compile>d__37::<>u__1
stloc.s 5 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter)
ldarg.0
ldflda System.Runtime.CompilerServices.ConfiguredTaskAwaiter VRage.Scripting.<Compile>d__37::<>u__1
initobj System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter
ldarg.0
ldc.i4.m1
dup
stloc.0
stfld System.Int32 VRage.Scripting.<Compile>d__37::<>1__state
L14:
ldloca.s 5 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter)
call System.Void System.Runtime.CompilerServices.ConfiguredTaskAwaiter::GetResult()
ldarg.0
ldc.i4.0
stfld System.Boolean VRage.Scripting.<Compile>d__37::<injectionFailed>5__3
ldarg.0
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass37_0 VRage.Scripting.<Compile>d__37::<>8__1
ldfld Microsoft.CodeAnalysis.CSharp.CSharpCompilation VRage.Scripting.<>c__DisplayClass37_0::compilation
stfld Microsoft.CodeAnalysis.CSharp.CSharpCompilation VRage.Scripting.<Compile>d__37::<compilationWithoutInjection>5__4
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass37_0 VRage.Scripting.<Compile>d__37::<>8__1
ldfld System.Func`3<Microsoft.CodeAnalysis.CSharp.CSharpCompilation, Microsoft.CodeAnalysis.SyntaxTree, Microsoft.CodeAnalysis.SyntaxTree> VRage.Scripting.<>c__DisplayClass37_0::syntaxTreeInjector
brfalse L16
ldarg.0
ldnull
stfld Microsoft.CodeAnalysis.SyntaxTree[] VRage.Scripting.<Compile>d__37::<newSyntaxTrees>5__6
L1:
nop
[EX_BeginException]
ldloc.0
ldc.i4.1
beq L17
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass37_0 VRage.Scripting.<Compile>d__37::<>8__1
ldfld Microsoft.CodeAnalysis.CSharp.CSharpCompilation VRage.Scripting.<>c__DisplayClass37_0::compilation
callvirt System.Collections.Immutable.ImmutableArray`1<Microsoft.CodeAnalysis.SyntaxTree> Microsoft.CodeAnalysis.CSharp.CSharpCompilation::get_SyntaxTrees()
stloc.s 7 (System.Collections.Immutable.ImmutableArray`1[Microsoft.CodeAnalysis.SyntaxTree])
ldloca.s 7 (System.Collections.Immutable.ImmutableArray`1[Microsoft.CodeAnalysis.SyntaxTree])
call System.Int32 System.Collections.Immutable.ImmutableArray`1<Microsoft.CodeAnalysis.SyntaxTree>::get_Length()
ldc.i4.1
bne.un.s L18
ldarg.0
ldc.i4.1
newarr Microsoft.CodeAnalysis.SyntaxTree
dup
ldc.i4.0
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass37_0 VRage.Scripting.<Compile>d__37::<>8__1
ldfld System.Func`3<Microsoft.CodeAnalysis.CSharp.CSharpCompilation, Microsoft.CodeAnalysis.SyntaxTree, Microsoft.CodeAnalysis.SyntaxTree> VRage.Scripting.<>c__DisplayClass37_0::syntaxTreeInjector
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass37_0 VRage.Scripting.<Compile>d__37::<>8__1
ldfld Microsoft.CodeAnalysis.CSharp.CSharpCompilation VRage.Scripting.<>c__DisplayClass37_0::compilation
ldloca.s 7 (System.Collections.Immutable.ImmutableArray`1[Microsoft.CodeAnalysis.SyntaxTree])
ldc.i4.0
call Microsoft.CodeAnalysis.SyntaxTree System.Collections.Immutable.ImmutableArray`1<Microsoft.CodeAnalysis.SyntaxTree>::get_Item(System.Int32 index)
callvirt virtual Microsoft.CodeAnalysis.SyntaxTree System.Func`3<Microsoft.CodeAnalysis.CSharp.CSharpCompilation, Microsoft.CodeAnalysis.SyntaxTree, Microsoft.CodeAnalysis.SyntaxTree>::Invoke(Microsoft.CodeAnalysis.CSharp.CSharpCompilation arg1, Microsoft.CodeAnalysis.SyntaxTree arg2)
stelem.ref
stfld Microsoft.CodeAnalysis.SyntaxTree[] VRage.Scripting.<Compile>d__37::<newSyntaxTrees>5__6
br L19
L18:
ldloc.s 7 (System.Collections.Immutable.ImmutableArray`1[Microsoft.CodeAnalysis.SyntaxTree])
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass37_0 VRage.Scripting.<Compile>d__37::<>8__1
ldftn System.Threading.Tasks.Task`1<Microsoft.CodeAnalysis.SyntaxTree> VRage.Scripting.<>c__DisplayClass37_0::<Compile>b__1(Microsoft.CodeAnalysis.SyntaxTree x)
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
brtrue.s L20
ldarg.0
ldc.i4.1
dup
stloc.0
stfld System.Int32 VRage.Scripting.<Compile>d__37::<>1__state
ldarg.0
ldloc.s 9 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[Microsoft.CodeAnalysis.SyntaxTree[]])
stfld System.Runtime.CompilerServices.ConfiguredTaskAwaiter<Microsoft.CodeAnalysis.SyntaxTree[]> VRage.Scripting.<Compile>d__37::<>u__2
ldarg.0
ldflda System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly> VRage.Scripting.<Compile>d__37::<>t__builder
ldloca.s 9 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[Microsoft.CodeAnalysis.SyntaxTree[]])
ldarg.0
call System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly>::AwaitUnsafeOnCompleted(System.Runtime.CompilerServices.ConfiguredTaskAwaiter& awaiter, VRage.Scripting.<Compile>d__37& stateMachine)
leave L21
L17:
ldarg.0
ldfld System.Runtime.CompilerServices.ConfiguredTaskAwaiter<Microsoft.CodeAnalysis.SyntaxTree[]> VRage.Scripting.<Compile>d__37::<>u__2
stloc.s 9 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[Microsoft.CodeAnalysis.SyntaxTree[]])
ldarg.0
ldflda System.Runtime.CompilerServices.ConfiguredTaskAwaiter<Microsoft.CodeAnalysis.SyntaxTree[]> VRage.Scripting.<Compile>d__37::<>u__2
initobj System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[Microsoft.CodeAnalysis.SyntaxTree[]]
ldarg.0
ldc.i4.m1
dup
stloc.0
stfld System.Int32 VRage.Scripting.<Compile>d__37::<>1__state
L20:
ldloca.s 9 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[Microsoft.CodeAnalysis.SyntaxTree[]])
call Microsoft.CodeAnalysis.SyntaxTree[] System.Runtime.CompilerServices.ConfiguredTaskAwaiter<Microsoft.CodeAnalysis.SyntaxTree[]>::GetResult()
stloc.s 8 (Microsoft.CodeAnalysis.SyntaxTree[])
ldarg.0
ldloc.s 8 (Microsoft.CodeAnalysis.SyntaxTree[])
stfld Microsoft.CodeAnalysis.SyntaxTree[] VRage.Scripting.<Compile>d__37::<newSyntaxTrees>5__6
L19:
leave.s L22
[EX_BeginCatch]
pop
ldarg.0
ldc.i4.1
stfld System.Boolean VRage.Scripting.<Compile>d__37::<injectionFailed>5__3
[EX_EndException]
leave.s L23
L22:
L23:
ldarg.0
ldfld System.Boolean VRage.Scripting.<Compile>d__37::<injectionFailed>5__3
brtrue L24
ldloc.1
ldarg.0
ldfld VRage.Scripting.MyApiTarget VRage.Scripting.<Compile>d__37::target
ldarg.0
ldfld System.String VRage.Scripting.<Compile>d__37::assemblyName
ldarg.0
ldfld Microsoft.CodeAnalysis.SyntaxTree[] VRage.Scripting.<Compile>d__37::<newSyntaxTrees>5__6
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
brtrue.s L25
ldarg.0
ldc.i4.2
dup
stloc.0
stfld System.Int32 VRage.Scripting.<Compile>d__37::<>1__state
ldarg.0
ldloc.s 5 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter)
stfld System.Runtime.CompilerServices.ConfiguredTaskAwaiter VRage.Scripting.<Compile>d__37::<>u__1
ldarg.0
ldflda System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly> VRage.Scripting.<Compile>d__37::<>t__builder
ldloca.s 5 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter)
ldarg.0
call System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly>::AwaitUnsafeOnCompleted(System.Runtime.CompilerServices.ConfiguredTaskAwaiter& awaiter, VRage.Scripting.<Compile>d__37& stateMachine)
leave L26
L2:
ldarg.0
ldfld System.Runtime.CompilerServices.ConfiguredTaskAwaiter VRage.Scripting.<Compile>d__37::<>u__1
stloc.s 5 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter)
ldarg.0
ldflda System.Runtime.CompilerServices.ConfiguredTaskAwaiter VRage.Scripting.<Compile>d__37::<>u__1
initobj System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter
ldarg.0
ldc.i4.m1
dup
stloc.0
stfld System.Int32 VRage.Scripting.<Compile>d__37::<>1__state
L25:
ldloca.s 5 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter)
call System.Void System.Runtime.CompilerServices.ConfiguredTaskAwaiter::GetResult()
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass37_0 VRage.Scripting.<Compile>d__37::<>8__1
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass37_0 VRage.Scripting.<Compile>d__37::<>8__1
ldfld Microsoft.CodeAnalysis.CSharp.CSharpCompilation VRage.Scripting.<>c__DisplayClass37_0::compilation
callvirt Microsoft.CodeAnalysis.CSharp.CSharpCompilation Microsoft.CodeAnalysis.CSharp.CSharpCompilation::RemoveAllSyntaxTrees()
ldarg.0
ldfld Microsoft.CodeAnalysis.SyntaxTree[] VRage.Scripting.<Compile>d__37::<newSyntaxTrees>5__6
callvirt Microsoft.CodeAnalysis.CSharp.CSharpCompilation Microsoft.CodeAnalysis.CSharp.CSharpCompilation::AddSyntaxTrees(Microsoft.CodeAnalysis.SyntaxTree[] trees)
stfld Microsoft.CodeAnalysis.CSharp.CSharpCompilation VRage.Scripting.<>c__DisplayClass37_0::compilation
L24:
ldarg.0
ldnull
stfld Microsoft.CodeAnalysis.SyntaxTree[] VRage.Scripting.<Compile>d__37::<newSyntaxTrees>5__6
L16:
ldarg.0
ldnull
stfld Microsoft.CodeAnalysis.Diagnostics.CompilationWithAnalyzers VRage.Scripting.<Compile>d__37::<analyticCompilation>5__5
ldarg.0
ldfld Microsoft.CodeAnalysis.Diagnostics.DiagnosticAnalyzer VRage.Scripting.<Compile>d__37::<whitelistAnalyzer>5__2
brfalse.s L27
ldarg.0
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass37_0 VRage.Scripting.<Compile>d__37::<>8__1
ldfld Microsoft.CodeAnalysis.CSharp.CSharpCompilation VRage.Scripting.<>c__DisplayClass37_0::compilation
ldarg.0
ldfld Microsoft.CodeAnalysis.Diagnostics.DiagnosticAnalyzer VRage.Scripting.<Compile>d__37::<whitelistAnalyzer>5__2
call static System.Collections.Immutable.ImmutableArray`1<Microsoft.CodeAnalysis.Diagnostics.DiagnosticAnalyzer> System.Collections.Immutable.ImmutableArray::Create(Microsoft.CodeAnalysis.Diagnostics.DiagnosticAnalyzer item)
ldnull
ldloca.s 11 (System.Threading.CancellationToken)
initobj System.Threading.CancellationToken
ldloc.s 11 (System.Threading.CancellationToken)
call static Microsoft.CodeAnalysis.Diagnostics.CompilationWithAnalyzers Microsoft.CodeAnalysis.Diagnostics.DiagnosticAnalyzerExtensions::WithAnalyzers(Microsoft.CodeAnalysis.Compilation compilation, System.Collections.Immutable.ImmutableArray`1<Microsoft.CodeAnalysis.Diagnostics.DiagnosticAnalyzer> analyzers, Microsoft.CodeAnalysis.Diagnostics.AnalyzerOptions options, System.Threading.CancellationToken cancellationToken)
stfld Microsoft.CodeAnalysis.Diagnostics.CompilationWithAnalyzers VRage.Scripting.<Compile>d__37::<analyticCompilation>5__5
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass37_0 VRage.Scripting.<Compile>d__37::<>8__1
ldarg.0
ldfld Microsoft.CodeAnalysis.Diagnostics.CompilationWithAnalyzers VRage.Scripting.<Compile>d__37::<analyticCompilation>5__5
callvirt Microsoft.CodeAnalysis.Compilation Microsoft.CodeAnalysis.Diagnostics.CompilationWithAnalyzers::get_Compilation()
castclass Microsoft.CodeAnalysis.CSharp.CSharpCompilation
stfld Microsoft.CodeAnalysis.CSharp.CSharpCompilation VRage.Scripting.<>c__DisplayClass37_0::compilation
L27:
ldarg.0
newobj System.Void System.IO.MemoryStream::.ctor()
stfld System.IO.MemoryStream VRage.Scripting.<Compile>d__37::<pdbStream>5__7
L3:
L4:
L5:
nop
[EX_BeginException]
ldloc.0
ldc.i4.3
sub
ldc.i4.2
ble.un.s L28
ldarg.0
newobj System.Void System.IO.MemoryStream::.ctor()
stfld System.IO.MemoryStream VRage.Scripting.<Compile>d__37::<assemblyStream>5__8
L28:
nop
[EX_BeginException]
ldloc.0
ldc.i4.3
sub
switch L29, L30, L31
ldarg.0
ldc.i4.0
stfld System.Boolean VRage.Scripting.<Compile>d__37::<loadPDBs>5__9
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass37_0 VRage.Scripting.<Compile>d__37::<>8__1
ldfld Microsoft.CodeAnalysis.CSharp.CSharpCompilation VRage.Scripting.<>c__DisplayClass37_0::compilation
ldarg.0
ldfld System.IO.MemoryStream VRage.Scripting.<Compile>d__37::<assemblyStream>5__8
ldarg.0
ldfld System.IO.MemoryStream VRage.Scripting.<Compile>d__37::<pdbStream>5__7
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
stloc.s 12 (Microsoft.CodeAnalysis.Emit.EmitResult)
ldarg.0
ldloc.s 12 (Microsoft.CodeAnalysis.Emit.EmitResult)
callvirt System.Boolean Microsoft.CodeAnalysis.Emit.EmitResult::get_Success()
stfld System.Boolean VRage.Scripting.<Compile>d__37::<success>5__10
newobj System.Void VRage.Scripting.MyBlacklistSyntaxVisitor::.ctor()
stloc.s 13 (VRage.Scripting.MyBlacklistSyntaxVisitor)
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass37_0 VRage.Scripting.<Compile>d__37::<>8__1
ldfld Microsoft.CodeAnalysis.CSharp.CSharpCompilation VRage.Scripting.<>c__DisplayClass37_0::compilation
callvirt System.Collections.Immutable.ImmutableArray`1<Microsoft.CodeAnalysis.SyntaxTree> Microsoft.CodeAnalysis.CSharp.CSharpCompilation::get_SyntaxTrees()
stloc.s 15 (System.Collections.Immutable.ImmutableArray`1[Microsoft.CodeAnalysis.SyntaxTree])
ldloca.s 15 (System.Collections.Immutable.ImmutableArray`1[Microsoft.CodeAnalysis.SyntaxTree])
call System.Collections.Immutable.Enumerator<Microsoft.CodeAnalysis.SyntaxTree> System.Collections.Immutable.ImmutableArray`1<Microsoft.CodeAnalysis.SyntaxTree>::GetEnumerator()
stloc.s 14 (System.Collections.Immutable.ImmutableArray`1+Enumerator[Microsoft.CodeAnalysis.SyntaxTree])
br.s L32
L33:
ldloca.s 14 (System.Collections.Immutable.ImmutableArray`1+Enumerator[Microsoft.CodeAnalysis.SyntaxTree])
call Microsoft.CodeAnalysis.SyntaxTree System.Collections.Immutable.Enumerator<Microsoft.CodeAnalysis.SyntaxTree>::get_Current()
stloc.s 16 (Microsoft.CodeAnalysis.SyntaxTree)
ldloc.s 13 (VRage.Scripting.MyBlacklistSyntaxVisitor)
ldarg.0
ldfld VRage.Scripting.<>c__DisplayClass37_0 VRage.Scripting.<Compile>d__37::<>8__1
ldfld Microsoft.CodeAnalysis.CSharp.CSharpCompilation VRage.Scripting.<>c__DisplayClass37_0::compilation
ldloc.s 16 (Microsoft.CodeAnalysis.SyntaxTree)
ldc.i4.0
callvirt Microsoft.CodeAnalysis.SemanticModel Microsoft.CodeAnalysis.Compilation::GetSemanticModel(Microsoft.CodeAnalysis.SyntaxTree syntaxTree, System.Boolean ignoreAccessibility)
callvirt System.Void VRage.Scripting.MyBlacklistSyntaxVisitor::SetSemanticModel(Microsoft.CodeAnalysis.SemanticModel model)
ldloc.s 13 (VRage.Scripting.MyBlacklistSyntaxVisitor)
ldloc.s 16 (Microsoft.CodeAnalysis.SyntaxTree)
ldloca.s 11 (System.Threading.CancellationToken)
initobj System.Threading.CancellationToken
ldloc.s 11 (System.Threading.CancellationToken)
callvirt Microsoft.CodeAnalysis.SyntaxNode Microsoft.CodeAnalysis.SyntaxTree::GetRoot(System.Threading.CancellationToken cancellationToken)
callvirt virtual System.Void Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor::Visit(Microsoft.CodeAnalysis.SyntaxNode node)
L32:
ldloca.s 14 (System.Collections.Immutable.ImmutableArray`1+Enumerator[Microsoft.CodeAnalysis.SyntaxTree])
call System.Boolean System.Collections.Immutable.Enumerator<Microsoft.CodeAnalysis.SyntaxTree>::MoveNext()
brtrue.s L33
ldloc.s 13 (VRage.Scripting.MyBlacklistSyntaxVisitor)
callvirt System.Boolean VRage.Scripting.MyBlacklistSyntaxVisitor::HasAnyResult()
brfalse.s L34
ldloc.s 13 (VRage.Scripting.MyBlacklistSyntaxVisitor)
ldarg.0
ldfld System.Collections.Generic.List`1<VRage.Scripting.Message> VRage.Scripting.<Compile>d__37::messages
callvirt System.Void VRage.Scripting.MyBlacklistSyntaxVisitor::GetResultMessages(System.Collections.Generic.List`1<VRage.Scripting.Message> results)
ldnull
stloc.2
leave L35
L34:
ldloc.1
ldarg.0
ldfld Microsoft.CodeAnalysis.Diagnostics.CompilationWithAnalyzers VRage.Scripting.<Compile>d__37::<analyticCompilation>5__5
ldloc.s 12 (Microsoft.CodeAnalysis.Emit.EmitResult)
ldarg.0
ldfld System.Collections.Generic.List`1<VRage.Scripting.Message> VRage.Scripting.<Compile>d__37::messages
ldarg.0
ldfld System.Boolean VRage.Scripting.<Compile>d__37::<success>5__10
call System.Threading.Tasks.Task`1<System.Boolean> VRage.Scripting.MyScriptCompiler::EmitDiagnostics(Microsoft.CodeAnalysis.Diagnostics.CompilationWithAnalyzers analyticCompilation, Microsoft.CodeAnalysis.Emit.EmitResult result, System.Collections.Generic.List`1<VRage.Scripting.Message> messages, System.Boolean success)
ldc.i4.0
callvirt System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1<System.Boolean> System.Threading.Tasks.Task`1<System.Boolean>::ConfigureAwait(System.Boolean continueOnCapturedContext)
stloc.s 19 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1[System.Boolean])
ldloca.s 19 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1[System.Boolean])
call System.Runtime.CompilerServices.ConfiguredTaskAwaiter<System.Boolean> System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1<System.Boolean>::GetAwaiter()
stloc.s 18 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[System.Boolean])
ldloca.s 18 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[System.Boolean])
call System.Boolean System.Runtime.CompilerServices.ConfiguredTaskAwaiter<System.Boolean>::get_IsCompleted()
brtrue.s L36
ldarg.0
ldc.i4.3
dup
stloc.0
stfld System.Int32 VRage.Scripting.<Compile>d__37::<>1__state
ldarg.0
ldloc.s 18 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[System.Boolean])
stfld System.Runtime.CompilerServices.ConfiguredTaskAwaiter<System.Boolean> VRage.Scripting.<Compile>d__37::<>u__3
ldarg.0
ldflda System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly> VRage.Scripting.<Compile>d__37::<>t__builder
ldloca.s 18 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[System.Boolean])
ldarg.0
call System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly>::AwaitUnsafeOnCompleted(System.Runtime.CompilerServices.ConfiguredTaskAwaiter& awaiter, VRage.Scripting.<Compile>d__37& stateMachine)
leave L37
L29:
ldarg.0
ldfld System.Runtime.CompilerServices.ConfiguredTaskAwaiter<System.Boolean> VRage.Scripting.<Compile>d__37::<>u__3
stloc.s 18 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[System.Boolean])
ldarg.0
ldflda System.Runtime.CompilerServices.ConfiguredTaskAwaiter<System.Boolean> VRage.Scripting.<Compile>d__37::<>u__3
initobj System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[System.Boolean]
ldarg.0
ldc.i4.m1
dup
stloc.0
stfld System.Int32 VRage.Scripting.<Compile>d__37::<>1__state
L36:
ldloca.s 18 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[System.Boolean])
call System.Boolean System.Runtime.CompilerServices.ConfiguredTaskAwaiter<System.Boolean>::GetResult()
stloc.s 17 (System.Boolean)
ldarg.0
ldloc.s 17 (System.Boolean)
stfld System.Boolean VRage.Scripting.<Compile>d__37::<success>5__10
ldloc.1
ldarg.0
ldfld VRage.Scripting.MyApiTarget VRage.Scripting.<Compile>d__37::target
ldarg.0
ldfld System.String VRage.Scripting.<Compile>d__37::assemblyName
ldarg.0
ldfld System.Collections.Generic.List`1<VRage.Scripting.Message> VRage.Scripting.<Compile>d__37::messages
ldarg.0
ldfld System.Boolean VRage.Scripting.<Compile>d__37::<success>5__10
call System.Threading.Tasks.Task VRage.Scripting.MyScriptCompiler::WriteDiagnostics(VRage.Scripting.MyApiTarget target, System.String assemblyName, System.Collections.Generic.IEnumerable`1<VRage.Scripting.Message> messages, System.Boolean success)
ldc.i4.0
callvirt System.Runtime.CompilerServices.ConfiguredTaskAwaitable System.Threading.Tasks.Task::ConfigureAwait(System.Boolean continueOnCapturedContext)
stloc.s 6 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable)
ldloca.s 6 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable)
call System.Runtime.CompilerServices.ConfiguredTaskAwaiter System.Runtime.CompilerServices.ConfiguredTaskAwaitable::GetAwaiter()
stloc.s 5 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter)
ldloca.s 5 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter)
call System.Boolean System.Runtime.CompilerServices.ConfiguredTaskAwaiter::get_IsCompleted()
brtrue.s L38
ldarg.0
ldc.i4.4
dup
stloc.0
stfld System.Int32 VRage.Scripting.<Compile>d__37::<>1__state
ldarg.0
ldloc.s 5 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter)
stfld System.Runtime.CompilerServices.ConfiguredTaskAwaiter VRage.Scripting.<Compile>d__37::<>u__1
ldarg.0
ldflda System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly> VRage.Scripting.<Compile>d__37::<>t__builder
ldloca.s 5 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter)
ldarg.0
call System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly>::AwaitUnsafeOnCompleted(System.Runtime.CompilerServices.ConfiguredTaskAwaiter& awaiter, VRage.Scripting.<Compile>d__37& stateMachine)
leave L39
L30:
ldarg.0
ldfld System.Runtime.CompilerServices.ConfiguredTaskAwaiter VRage.Scripting.<Compile>d__37::<>u__1
stloc.s 5 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter)
ldarg.0
ldflda System.Runtime.CompilerServices.ConfiguredTaskAwaiter VRage.Scripting.<Compile>d__37::<>u__1
initobj System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter
ldarg.0
ldc.i4.m1
dup
stloc.0
stfld System.Int32 VRage.Scripting.<Compile>d__37::<>1__state
L38:
ldloca.s 5 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter)
call System.Void System.Runtime.CompilerServices.ConfiguredTaskAwaiter::GetResult()
ldarg.0
ldfld System.IO.MemoryStream VRage.Scripting.<Compile>d__37::<pdbStream>5__7
ldc.i4.0
conv.i8
ldc.i4.0
callvirt abstract virtual System.Int64 System.IO.Stream::Seek(System.Int64 offset, System.IO.SeekOrigin origin)
pop
ldarg.0
ldfld System.IO.MemoryStream VRage.Scripting.<Compile>d__37::<assemblyStream>5__8
ldc.i4.0
conv.i8
ldc.i4.0
callvirt abstract virtual System.Int64 System.IO.Stream::Seek(System.Int64 offset, System.IO.SeekOrigin origin)
pop
ldarg.0
ldfld System.Boolean VRage.Scripting.<Compile>d__37::<injectionFailed>5__3
brtrue L40
ldarg.0
ldfld System.Boolean VRage.Scripting.<Compile>d__37::<success>5__10
brfalse.s L41
ldloc.1
ldarg.0
ldfld VRage.Scripting.MyApiTarget VRage.Scripting.<Compile>d__37::target
ldarg.0
ldfld System.Collections.Generic.IEnumerable`1<VRage.Scripting.Script> VRage.Scripting.<Compile>d__37::scripts
ldarg.0
ldfld System.IO.MemoryStream VRage.Scripting.<Compile>d__37::<assemblyStream>5__8
call static System.Void Shared.Patches.MyScriptCompilerPatch::StoreIntoCache(VRage.Scripting.MyScriptCompiler myScriptCompiler, VRage.Scripting.MyApiTarget target, System.Collections.Generic.IEnumerable`1<VRage.Scripting.Script> scripts, System.IO.MemoryStream assemblyStream)
ldarg.0
ldfld System.Boolean VRage.Scripting.<Compile>d__37::<loadPDBs>5__9
brfalse.s L42
ldarg.0
ldfld System.IO.MemoryStream VRage.Scripting.<Compile>d__37::<assemblyStream>5__8
callvirt virtual System.Byte[] System.IO.MemoryStream::ToArray()
ldarg.0
ldfld System.IO.MemoryStream VRage.Scripting.<Compile>d__37::<pdbStream>5__7
callvirt virtual System.Byte[] System.IO.MemoryStream::ToArray()
call static System.Reflection.Assembly System.Reflection.Assembly::Load(System.Byte[] rawAssembly, System.Byte[] rawSymbolStore)
stloc.2
leave L43
L42:
ldarg.0
ldfld System.IO.MemoryStream VRage.Scripting.<Compile>d__37::<assemblyStream>5__8
callvirt virtual System.Byte[] System.IO.MemoryStream::ToArray()
call static System.Reflection.Assembly System.Reflection.Assembly::Load(System.Byte[] rawAssembly)
stloc.2
leave L44
L41:
ldarg.0
ldfld Microsoft.CodeAnalysis.CSharp.CSharpCompilation VRage.Scripting.<Compile>d__37::<compilationWithoutInjection>5__4
ldarg.0
ldfld System.IO.MemoryStream VRage.Scripting.<Compile>d__37::<assemblyStream>5__8
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
stloc.s 12 (Microsoft.CodeAnalysis.Emit.EmitResult)
ldloc.1
ldarg.0
ldfld Microsoft.CodeAnalysis.Diagnostics.CompilationWithAnalyzers VRage.Scripting.<Compile>d__37::<analyticCompilation>5__5
ldloc.s 12 (Microsoft.CodeAnalysis.Emit.EmitResult)
ldarg.0
ldfld System.Collections.Generic.List`1<VRage.Scripting.Message> VRage.Scripting.<Compile>d__37::messages
ldc.i4.0
call System.Threading.Tasks.Task`1<System.Boolean> VRage.Scripting.MyScriptCompiler::EmitDiagnostics(Microsoft.CodeAnalysis.Diagnostics.CompilationWithAnalyzers analyticCompilation, Microsoft.CodeAnalysis.Emit.EmitResult result, System.Collections.Generic.List`1<VRage.Scripting.Message> messages, System.Boolean success)
ldc.i4.0
callvirt System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1<System.Boolean> System.Threading.Tasks.Task`1<System.Boolean>::ConfigureAwait(System.Boolean continueOnCapturedContext)
stloc.s 19 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1[System.Boolean])
ldloca.s 19 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1[System.Boolean])
call System.Runtime.CompilerServices.ConfiguredTaskAwaiter<System.Boolean> System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1<System.Boolean>::GetAwaiter()
stloc.s 18 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[System.Boolean])
ldloca.s 18 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[System.Boolean])
call System.Boolean System.Runtime.CompilerServices.ConfiguredTaskAwaiter<System.Boolean>::get_IsCompleted()
brtrue.s L45
ldarg.0
ldc.i4.5
dup
stloc.0
stfld System.Int32 VRage.Scripting.<Compile>d__37::<>1__state
ldarg.0
ldloc.s 18 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[System.Boolean])
stfld System.Runtime.CompilerServices.ConfiguredTaskAwaiter<System.Boolean> VRage.Scripting.<Compile>d__37::<>u__3
ldarg.0
ldflda System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly> VRage.Scripting.<Compile>d__37::<>t__builder
ldloca.s 18 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[System.Boolean])
ldarg.0
call System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly>::AwaitUnsafeOnCompleted(System.Runtime.CompilerServices.ConfiguredTaskAwaiter& awaiter, VRage.Scripting.<Compile>d__37& stateMachine)
leave L46
L31:
ldarg.0
ldfld System.Runtime.CompilerServices.ConfiguredTaskAwaiter<System.Boolean> VRage.Scripting.<Compile>d__37::<>u__3
stloc.s 18 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[System.Boolean])
ldarg.0
ldflda System.Runtime.CompilerServices.ConfiguredTaskAwaiter<System.Boolean> VRage.Scripting.<Compile>d__37::<>u__3
initobj System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[System.Boolean]
ldarg.0
ldc.i4.m1
dup
stloc.0
stfld System.Int32 VRage.Scripting.<Compile>d__37::<>1__state
L45:
ldloca.s 18 (System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1+ConfiguredTaskAwaiter[System.Boolean])
call System.Boolean System.Runtime.CompilerServices.ConfiguredTaskAwaiter<System.Boolean>::GetResult()
pop
L40:
ldnull
stloc.2
leave.s L47
[EX_BeginFinally]
ldloc.0
ldc.i4.0
bge.s L48
ldarg.0
ldfld System.IO.MemoryStream VRage.Scripting.<Compile>d__37::<assemblyStream>5__8
brfalse.s L49
ldarg.0
ldfld System.IO.MemoryStream VRage.Scripting.<Compile>d__37::<assemblyStream>5__8
callvirt abstract virtual System.Void System.IDisposable::Dispose()
L48:
L49:
[EX_EndException]
endfinally
[EX_BeginFinally]
ldloc.0
ldc.i4.0
bge.s L50
ldarg.0
ldfld System.IO.MemoryStream VRage.Scripting.<Compile>d__37::<pdbStream>5__7
brfalse.s L51
ldarg.0
ldfld System.IO.MemoryStream VRage.Scripting.<Compile>d__37::<pdbStream>5__7
callvirt abstract virtual System.Void System.IDisposable::Dispose()
L50:
L51:
[EX_EndException]
endfinally
[EX_BeginCatch]
stloc.s 20 (System.Exception)
ldarg.0
ldc.i4.s -2
stfld System.Int32 VRage.Scripting.<Compile>d__37::<>1__state
ldarg.0
ldflda System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly> VRage.Scripting.<Compile>d__37::<>t__builder
ldloc.s 20 (System.Exception)
call System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly>::SetException(System.Exception exception)
[EX_EndException]
leave.s L52
L35:
L43:
L44:
L47:
ldarg.0
ldc.i4.s -2
stfld System.Int32 VRage.Scripting.<Compile>d__37::<>1__state
ldarg.0
ldflda System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly> VRage.Scripting.<Compile>d__37::<>t__builder
ldloc.2
call System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder`1<System.Reflection.Assembly>::SetResult(System.Reflection.Assembly result)
L15:
L21:
L26:
L37:
L39:
L46:
L52:
ret
