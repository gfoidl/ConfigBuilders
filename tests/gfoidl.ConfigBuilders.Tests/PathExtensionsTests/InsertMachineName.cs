using System;
using System.IO;
using NUnit.Framework;

namespace gfoidl.ConfigBuilders.Tests.PathExtensionsTests
{
    [TestFixture]
    public class InsertMachineName
    {
        [Test]
        public void FileName_given([Values("svclog", "log", "txt", "bar")]string extension)
        {
            string path     = Path.ChangeExtension("mylog", extension);
            string expected = Path.ChangeExtension($"mylog_{Environment.MachineName}", extension);

            string actual = path.InsertMachineName();

            Assert.AreEqual(expected, actual);
        }
        //---------------------------------------------------------------------
        [Test]
        public void FileName_with_absolute_path_given([Values("svclog", "log", "txt", "bar")]string extension)
        {
            string path     = Path.ChangeExtension(Path.Combine(@"k:\", "logs", "mylog"), extension);
            string expected = Path.ChangeExtension(Path.Combine(@"k:\", "logs", $"mylog_{Environment.MachineName}"), extension);

            string actual = path.InsertMachineName();

            Assert.AreEqual(expected, actual);
        }
        //---------------------------------------------------------------------
        [Test]
        public void FileName_with_relative_path_given([Values("svclog", "log", "txt", "bar")]string extension)
        {
            string path     = Path.ChangeExtension(Path.Combine(@"..\", "logs", "mylog"), extension);
            string expected = Path.ChangeExtension(Path.Combine(@"..\", "logs", $"mylog_{Environment.MachineName}"), extension);

            string actual = path.InsertMachineName();

            Assert.AreEqual(expected, actual);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Issue_2_FileName_with_dot___Fixed()
        {
            string path     = @"..\gfoidl.Xyz.logs\gfoidl.Xyz.Service.svclog";
            string expected = $@"..\gfoidl.Xyz.logs\gfoidl.Xyz.Service_{Environment.MachineName}.svclog";

            string actual = path.InsertMachineName();

            Assert.AreEqual(expected, actual);
        }
    }
}
