/*
Policy
<summary>
A simple class which represents a generic insurance policy
</summary>
<remarks>
Implements the singleton design pattern
</remarks>

Copyright 2023 Andrew Todd

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom
the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE
AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonProgram
{
    /// <summary>
    /// A simple class which represents a generic insurance policy
    /// </summary>
    /// <remarks>
    /// Implements the singleton design pattern
    /// </remarks>
    internal class Policy
    {
        private static readonly object _instanceLock = new();
        private readonly ReaderWriterLockSlim _nameLock = new();
        private readonly ReaderWriterLockSlim _IDLock = new();
        //Can use interlock instead
        //private static readonly object _IDLock = new();

        private static Policy? _instance = null;

        //0 for false, 1 for true
        private int _nameSet = 0;
        private string _holderName = "*ERROR UNINITIALIZED POLICY*";
        //0 for false, 1 for true
        private int _IDSet = 0;
        private int _policyID = -1;

        private Policy()
        {}

        /// <summary>
        /// This Property allows callers to get a reference to the instance of the singleton
        /// </summary>
        /// <remarks>
        /// Will instantiate a new instance the first time called
        /// </remarks>
        public static Policy Instance
        {
            get
            {
                if(_instance == null)
                {
                    lock(_instanceLock)
                    {
                        _instance ??= new Policy();
                    }
                }
                return _instance;
            }
        }
        /// <summary>
        /// This Property handles access to the policy holder's name
        /// </summary>
        /// <remarks>
        /// Thread safe. Will mark a token signifying that the underlying field was initialized upon first set call
        /// </remarks>
        public string HolderName 
        { 
            get
            {
                _nameLock.EnterReadLock();
                try
                {
                    return _holderName;
                }
                finally
                {
                    _nameLock.ExitReadLock();
                }
            }
            set
            {
                _nameLock.EnterWriteLock();
                try
                {
                    _holderName = value;
                    Interlocked.Exchange(ref _nameSet, 1);
                }
                finally
                { 
                    _nameLock.ExitWriteLock(); 
                }
            }
        }
        /// <summary>
        /// This Property handles access to the policy's ID
        /// </summary>
        /// <remarks>
        /// Thread safe. Will mark a token signifying that the underlying field was initialized upon first set call
        /// </remarks>
        public int PolicyID
        {
            get
            {
                _IDLock.EnterReadLock();
                try
                {
                    return _policyID;
                }
                finally
                {
                    _IDLock.ExitReadLock();
                }
            }
            set
            {
                if(value >= 0)
                {
                    _IDLock.EnterWriteLock();
                    try
                    {
                        _policyID = value;
                        Interlocked.Exchange(ref _IDSet, 1);
                    }
                    finally
                    { 
                        _IDLock.ExitWriteLock();
                    }
                }
                else
                {
                    throw new ArgumentException("**ID INVALID!!!**\nPolicy ID can not be less than 0");
                }
            }
        }

        /// <returns>The policy holder's name</returns>
        public string GetHolderName()
        {
            return HolderName;
        }
        /// <summary>
        /// Will set the policy holder's name according to the holderName param
        /// </summary>
        /// <param name="holderName"></param>
        public void SetHolderName(string holderName) 
        {
            HolderName = holderName;
        }

        /// <returns>The policy ID</returns>
        public int GetPolicyID() 
        {
            return PolicyID;
        }
        /// <summary>
        /// Sets the policy ID according to the policyID param. Param must be greater than or equal to 0
        /// </summary>
        /// <param name="policyID">Intager value representing the policy ID. Must be greater than or equal to 0</param>
        public void SetPolicyID(int policyID) 
        {
            PolicyID = policyID;
        }

        /// <returns>A string with information about the policy object</returns>
        public string GetPolicyDescription()
        {
            if (Interlocked.Equals(_nameSet, 1) && Interlocked.Equals(_IDSet, 1))
                return $"Policy Holder: {HolderName}\nID: {PolicyID}\nPolicy Description: Expensive, difficult to understand and what you get is not guaranteed.";
            else
                return "This policy has not be correctly initialized. Set both the holder name and ID of the policy";
        }
    }
}
