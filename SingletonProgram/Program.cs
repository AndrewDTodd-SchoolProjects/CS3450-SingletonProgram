/*
* Project Prolog
* Name: Andrew D. Todd
* CS3450 Section 002
* Project: assignment 5 Singleton Program
* Date: ~03-09-23
* Purpose: To demonstrate a usage of the Singleton Design by the creation and usage of a thread safe singleton object (Policy)
* 
Copyright 2023 Andrew Todd

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom
the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE
AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

namespace SingletonProgram
{
    /// <summary>
    /// Entry point / driver of the program
    /// </summary>
    /// <remarks>
    /// Is used to demonstrate acessing the single instance of the Policy class which implements the singleton design pattern
    /// and utalizing its methods
    /// </remarks>
    internal class Program
    {
        static void Main()
        {
            Policy policy = Policy.Instance;

            policy.HolderName = "John Doe";
            policy.PolicyID = 1;

            Console.WriteLine(policy.GetPolicyDescription());
            Console.WriteLine("Press any Key to Exit the demo");
            Console.ReadKey();
        }
    }
}