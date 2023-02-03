namespace Tools.Tools.Grid
{
    public class Footer
    {
        public string isPrevDisabled { get; set; }
        public string isNextDisabled { get; set; }
        public int activeBtn { get; set; } = 1; 
        public int firstBtn { get; set; } = 1;

        public string nextBtnArgs { get; set; }
        public string prevBtnArgs { get; set; }
        public int pagerSize { get; set; }
    }
}
