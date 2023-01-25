namespace Tools.Tools.Grid
{
    public class Footer
    {
        public string isPrevDisabled { get; set; }
        public string isNextDisabled { get; set; }
        public int activeBtn { get; set; } = 1; 
        public List<int> fRange { get; set; }
    }
}
