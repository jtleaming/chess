namespace ChessEngine.Common
{
    public sealed class Singletons
    {
        private Singletons(){}
        private static RankBacker rank = null;
        private static FileBacker file = null;
        public static RankBacker Rank => rank = rank == null ? RankBacker.GetRank() : rank;
        public static FileBacker File => file = file == null ? FileBacker.GetFile() : file;
    }
}