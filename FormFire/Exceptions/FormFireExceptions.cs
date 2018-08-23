// Copyright (c) 2018 Kadir Çetintaş
// http://github.com/kdrcetintas
// https://github.com/kdrcetintas/FormFire
// All rights reserved
// Please check the github repository for bugs / new fixes
// Version 1.0.0.1
// 2018-08-23
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FormFire.Core.Exceptions
{
    public class FormFireExceptions
    {
        public class FireFormListException : Exception
        {
            public FireFormListException()
            {

            }

            public FireFormListException(string message) : base(message)
            {

            }

            public FireFormListException(string message, Exception inner) : base(message, inner)
            {

            }
        }

        public class CreateFormInstanceException : Exception
        {
            public CreateFormInstanceException()
            {

            }

            public CreateFormInstanceException(string message) : base(message)
            {

            }

            public CreateFormInstanceException(string message, Exception inner) : base(message, inner)
            {

            }
        }

        public class ShowFormInstanceException : Exception
        {
            public ShowFormInstanceException()
            {

            }

            public ShowFormInstanceException(string message)
                : base(message)
            {

            }

            public ShowFormInstanceException(string message, Exception inner)
                : base(message, inner)
            {

            }
        }

        public class FireFormEventException : Exception
        {
            public FireFormEventException()
            {

            }

            public FireFormEventException(string message)
                : base(message)
            {

            }

            public FireFormEventException(string message, Exception inner)
                : base(message, inner)
            {

            }
        }
    }
}
