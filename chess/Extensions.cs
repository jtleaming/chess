namespace chess
{
    public static class Extensions
    {
        public static char ConvertIntToLetter(this int column)
        {
            switch (column)
            {
                case 2:
                    return 'B';
                case 3:
                    return 'C';
                case 4:
                    return 'D';
                case 5:
                    return 'E';
                case 6:
                    return 'F';
                case 7:
                    return 'G';
                default: return 'A';
            }
        }
    }
}