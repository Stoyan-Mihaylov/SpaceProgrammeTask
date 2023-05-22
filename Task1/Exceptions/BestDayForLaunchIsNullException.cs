using System;

namespace SpaceProgramme.Exceptions
{
    public class BestDayForLaunchIsNullException : Exception
    {
        private const string DefaultMessage =
            "No suitable day for launch found!";

        public BestDayForLaunchIsNullException()
            : base(DefaultMessage)
        {

        }

        public BestDayForLaunchIsNullException(string message)
            : base(message)
        {

        }
    }
}
