// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Runtime.Serialization;

namespace System
{
    public partial class TypeLoadException : SystemException
    {
        public TypeLoadException()
            : base(SR.Arg_TypeLoadException)
        {
            HResult = HResults.COR_E_TYPELOAD;
        }

        public TypeLoadException(string message)
            : base(message)
        {
            HResult = HResults.COR_E_TYPELOAD;
        }

        public TypeLoadException(string message, Exception inner)
            : base(message, inner)
        {
            HResult = HResults.COR_E_TYPELOAD;
        }

        protected TypeLoadException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            throw new PlatformNotSupportedException();
        }

        public override string Message
        {
            get
            {
                if (_message == null)
                    _message = SR.Arg_TypeLoadException;
                return _message;
            }
        }

        public string TypeName
        {
            get
            {
                if (_typeName == null)
                    return string.Empty;
                return _typeName;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        private readonly string _typeName;
    }
}
