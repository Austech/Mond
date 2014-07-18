﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Mond.Tests.Expressions
{
    [TestFixture]
    public class ExpressionTests
    {
        [Test]
        public void OrderOfOperations()
        {
            var result = Script.Run(@"
                return 3 + 4 * 2 / (1 - 5);
            ");

            Assert.True(result == 1);
        }

        [Test]
        public void Ternary()
        {
            var state = Script.Load(@"
                test = fun (n) {
                    return n >= 10 ? 3 : 9;
                };
            ");

            var func = state["test"];

            Assert.True(state.Call(func, 15) == 3);

            Assert.True(state.Call(func, 5) == 9);
        }

        [Test]
        public void LogicalOr()
        {
            var result = Script.Run(@"
                var result = '';

                fun test(val, str) {
                    result += str;
                    return val;
                }
            
                if (test(true, 'a') || test(true, 'b'))
                    result += '!';

                if (test(false, 'A') || test(true, 'B'))
                    result += '!';

                return result;
            ");

            Assert.True(result == "a!AB!");
        }

        [Test]
        public void LogicalAnd()
        {
            var result = Script.Run(@"
                var result = '';

                fun test(val, str) {
                    result += str;
                    return val;
                }
            
                if (test(true, 'a') && test(true, 'b'))
                    result += '!';

                if (test(false, 'A') && test(true, 'B'))
                    result += '!';

                return result;
            ");

            Assert.True(result == "ab!A");
        }
    }
}
