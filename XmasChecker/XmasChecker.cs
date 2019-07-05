using System;

namespace XmasChecker
{
    public class XmasChecker
    {
        public virtual bool IsTodayXmas()
        {
            var today = DateTime.Today;
            if (today.Month == 12 && today.Day == 25)
            {
                return true;
            }

            return false;
        }

        public virtual bool IsTodayXmas(DateTime today)
        {
            throw new NotImplementedException();
        }
    }
}