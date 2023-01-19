namespace Tools.Tools.Grid
{
    public class Footer
    {
        public string isPrevDisabled { get; set; }
        public string isNextDisabled { get; set; } 
        public int activeBtn { get; set; } 
        public List<int> fRange { get; set; }

        public int lastPagerBtn { get; set; }
        public bool isFulllastPage { get; set; } 
    }
}
