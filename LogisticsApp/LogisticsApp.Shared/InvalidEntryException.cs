﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsApp.Shared
{
    public class InvalidEntryException : Exception
    {
        public InvalidEntryException(string message) : base(message)
        {

        }
    }
}
