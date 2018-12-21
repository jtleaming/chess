using System;
using System.Collections.Generic;

namespace ChessEngine.Common
{
    public sealed class Singletons
    {
        private static readonly Lazy<List<string>> rank = new Lazy<List<string>>(() => new List<string> {"1","2","3","4","5","6","7","8"});
        private static readonly Lazy<List<string>> file = new Lazy<List<string>>(() => new List<string>{"a", "b", "c", "d", "e","f","g", "h"});
        private Singletons(){}
        public static List<string> Rank { get => rank.Value;}
        public static List<string> File {get => file.Value;}

    }
}