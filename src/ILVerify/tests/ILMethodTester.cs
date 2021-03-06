﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Internal.IL;
using Internal.TypeSystem.Ecma;
using Xunit;

namespace ILVerify.Tests
{
    public class ILMethodTester
    {
        [Theory]
        [MemberData(nameof(TestDataLoader.GetMethodsWithValidIL), MemberType = typeof(TestDataLoader))]
        void TestMethodsWithValidIL(ValidILTestCase validILTestCase)
        {
            ILImporter importer = ConstructILImporter(validILTestCase);

            var verifierErrors = new List<VerifierError>();
            importer.ReportVerificationError = new Action<VerificationErrorArgs>((err) =>
            {
                verifierErrors.Add(err.Code);
            });

            importer.Verify();
            Assert.Equal(0, verifierErrors.Count);
        }

        [Theory]
        [MemberData(nameof(TestDataLoader.GetMethodsWithInvalidIL), MemberType = typeof(TestDataLoader))]
        void TestMethodsWithInvalidIL(InvalidILTestCase invalidILTestCase)
        {
            ILImporter importer = ConstructILImporter(invalidILTestCase);

            var verifierErrors = new List<VerifierError>();
            importer.ReportVerificationError = new Action<VerificationErrorArgs>((err) =>
            {
                verifierErrors.Add(err.Code);
            });
            
            try
            {
                importer.Verify();
            }
            catch
            {
                //in some cases ILVerify throws exceptions when things look too wrong to continue
                //currently these are not caught. In tests we just catch these and do the asserts.
                //Once these exceptions are better handled and ILVerify instead of crashing aborts the verification
                //gracefully we can remove this empty catch block.
            }
            finally
            {
                Assert.Equal(invalidILTestCase.ExpectedVerifierErrors.Count, verifierErrors.Count);

                foreach (var item in invalidILTestCase.ExpectedVerifierErrors)
                {
                    Assert.True(verifierErrors.Contains(item));
                }
            }        
        }

        private ILImporter ConstructILImporter(TestCase testCase)
        {
            var module = TestDataLoader.GetModuleForTestAssembly(testCase.ModuleName);
            var method = (EcmaMethod)module.GetMethod(MetadataTokens.EntityHandle(testCase.MetadataToken));
            var methodIL = EcmaMethodIL.Create(method);

            return new ILImporter(method, methodIL);
        }
    }
}
