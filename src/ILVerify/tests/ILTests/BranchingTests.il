// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

.assembly extern System.Runtime
{
}

.assembly Branching
{
}

.class public auto ansi beforefieldinit BranchingTestsType
       extends [System.Runtime]System.Object
{
    .method static public hidebysig void Branching.NullConditional_Valid() cil managed
    {
        //object o = null;
        //Type t = o != null ? o.GetType() : null;
        //Type.GetTypeCode(t);

        .maxstack 1
        .locals init (
            [0] object o
        )

        IL_0000: ldnull
        IL_0001: stloc.0
        IL_0002: ldloc.0
        IL_0003: brtrue.s  IL_0008

        IL_0005: ldnull
        IL_0006: br.s      IL_000E

        IL_0008: ldloc.0
        IL_0009: callvirt  instance class [System.Runtime]System.Type [System.Runtime]System.Object::GetType()

        IL_000E: call      valuetype [System.Runtime]System.TypeCode [System.Runtime]System.Type::GetTypeCode(class [System.Runtime]System.Type)
        IL_0013: pop
        IL_0014: ret
    }

    .method static public hidebysig void Branching.NullConditional_Invalid_StackUnexpected.PathStackUnexpected() cil managed
    {
        //object o = null;
        //Type t = o != null ? o.GetType() : o;
        //Type.GetTypeCode(t);

        .maxstack 1
        .locals init (
            [0] object o
        )

        IL_0000: ldnull
        IL_0001: stloc.0
        IL_0002: ldloc.0
        IL_0003: brtrue.s  IL_0008

        IL_0005: ldloc.0
        IL_0006: br.s      IL_000E

        IL_0008: ldloc.0
        IL_0009: callvirt  instance class [System.Runtime]System.Type [System.Runtime]System.Object::GetType()

        IL_000E: call      valuetype [System.Runtime]System.TypeCode [System.Runtime]System.Type::GetTypeCode(class [System.Runtime]System.Type)
        IL_0013: pop
        IL_0014: ret
    }

    .method static public hidebysig void Branching.InsideTry_Valid() cil managed
    {
        .maxstack 2

        .try
        {
            ldc.i4.s  10
            ldc.i4.s  5
            bne.un.s  lbl_true

            nop
            br.s      lbl_leave

            lbl_true: nop

            lbl_leave: leave.s   lbl_ret
        }
        catch [System.Runtime]System.Object
        {
            pop
            leave.s   lbl_ret
        }

        lbl_ret: ret
    }

    .method static public hidebysig void Branching.OutOfTry_Invalid_BranchOutOfTry() cil managed
    {
        .maxstack 2

        .try
        {
            ldc.i4.s  10
            ldc.i4.s  5
            bne.un.s  lbl_true

            nop
            br.s      lbl_ret

            lbl_true: nop

            lbl_leave: leave.s   lbl_ret
        }
        catch [System.Runtime]System.Object
        {
            pop
            leave.s   lbl_ret
        }

        lbl_ret: ret
    }

    .method static public hidebysig void Branching.IntoTry_Invalid_BranchIntoTry() cil managed
    {
        .maxstack 2

        ldc.i4.s  10
        ldc.i4.s  5
        bne.un.s  lbl_true

        nop
        br.s      lbl_ret

        .try
        {
            lbl_true: nop

            leave.s   lbl_ret
        }
        catch [System.Runtime]System.Object
        {
            pop
            leave.s   lbl_ret
        }

        lbl_ret: ret
    }
}
