namespace TraqCop.auth.ViewModel
{
    public class DateViewModel
    {
        public DateViewModel()
        {

        }
        public DateViewModel(int Year, int Month, int Day)
        {
            this.Year = Year;
            this.Month = Month;
            this.Day = Day;
        }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
