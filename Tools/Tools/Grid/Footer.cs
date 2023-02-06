namespace Tools.Tools.Grid
{
    public class Footer
    {
        public string isPrevDisabled { get; set; }
        public string isNextDisabled { get; set; }
        public int activeBtn { get; set; } = 1; 
        public int firstBtn { get; set; } = 1;
        public int lastBtn { get; set; }
        public int prevBtn { get; set; }
    }
}
