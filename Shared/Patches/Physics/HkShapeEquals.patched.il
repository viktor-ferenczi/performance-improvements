// f8cff3e2
ldarg.1
ldfld System.IntPtr Havok.HkShape::m_handle
call System.Int64 System.IntPtr::ToInt64()
ldarg.2
ldfld System.IntPtr Havok.HkShape::m_handle
call System.Int64 System.IntPtr::ToInt64()
ceq
ret
