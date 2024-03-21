namespace WebOopPrac_Api.Class
{
    public class IDGenerator
    {
        public static long GenerateID()
        {
            DateTime now = DateTime.Now;
            int year = now.Year % 100;
            int month = now.Month;
            int day = now.Day;
            int minute = now.Minute;
            int second = now.Second;

            long id = day  * 10000 + minute * 100 + second;

            return id;
        }
    }
}
