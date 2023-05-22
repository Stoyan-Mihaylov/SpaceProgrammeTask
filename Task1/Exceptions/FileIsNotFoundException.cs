using System;

namespace SpaceProgramme.Exceptions
{
    public class FileIsNotFoundException : Exception
    {
        private const string DefaultMessage =
            "File is not found";

        public FileIsNotFoundException()
            : base(DefaultMessage)
        {

        }

        public FileIsNotFoundException(string message)
            : base(message)
        {

        }
    }
}
