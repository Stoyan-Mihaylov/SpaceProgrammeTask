using SpaceProgramme.Core;
using SpaceProgramme.Exceptions;
using SpaceProgramme.Helpers;
using SpaceProgramme.Models;

namespace SpaceProgramme.Tests
{
    [TestFixture]
    public class Tests
    {
        private Engine engine;

        [SetUp]
        public void Setup()
        {
            engine = new Engine();
        }

        [TestCase(new object[] { "1", "2", "3" })]
        [TestCase(new object[] {})]
        [TestCase(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" })]

        public void WhenGivenArgumentsAreDifferentThanFourShouldThrowAnArgumentsException(params string[] data)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                engine.Run(data);
            });
        }

        [TestCase(new object[] { "C:\\Users\\admin\\Desktop\\InputFiles.csv", "stoyan.mihaylov950907@gmail.com", "mvgzampnvbwrsxcf", "stoyan.mihaylov950907@gmail.com" })]
        public void IfGivenFileDoesntExistShouldThrowAnFileNotFoundException(params string[] arguments)
        {
            Assert.Throws<FileIsNotFoundException>(() =>
            {
                engine.Run(arguments);
            });
        }

        [TestCase(new object[] { "C:\\Users\\admin\\Desktop\\TestFile.csv", "stoyan.mihaylov950907@gmail.com", "mvgzampnvbwrsxcf", "stoyan.mihaylov950907@gmail.com" })]
        public void IfBestDayIsNullShouldThrowAnBestDayForLaunchIsNullException(params string[] arguments)
        {
            Assert.Throws<BestDayForLaunchIsNullException>(() =>
            {
                engine.Run(arguments);
            });
        }

        [Test]
        public void IfEmailIsSendMethodTrySendEmailShouldReturnTrue()
        {
            string fromEmail = "stoyan.mihaylov950907@gmail.com";
            string fromPassword = "mvgzampnvbwrsxcf";
            string toEmail = "stoyan.mihaylov950907@gmail.com";
            string attachmentPath = "./WeatherReport.csv";

            Assert.IsTrue(EmailSenderHelper.TrySendEmail(fromEmail, fromPassword, toEmail, "", "", attachmentPath));
        }
    }
}