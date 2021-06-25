using System;
using System.IO;
using System.Text;
using Xunit;
using Gps2Yandex.References.Handlers;

namespace Gps2Yandex.References.Test
{
    public class UnitTestRouteLoader
    {
        [Fact(DisplayName = "������� ���������� ������ � ������� � ��������")]
        public void TestParseSuccess()
        {
            const string testData = "103�;103�";
            var route = RouteLoader.Parse(testData);
            if (route.ExternalNumber != "103�" || route.YandexNumber != "103�")
            {
                throw new Exception($"Parse route.");
            }
        }

        [Fact(DisplayName = "������� ������������ ������ � ������� � ��������")]
        public void TestParseFailed()
        {
            const string testData = "103�";
            try
            {
                _ = RouteLoader.Parse(testData);
            }
            catch (FormatException)
            {
                return;
            }
            throw new Exception($"An exception was expected `FormatException`.");
        }

        [Fact(DisplayName = "������� ������ ������")]
        public void TestParseEmptyStringFailed()
        {
            const string testData = "";
            try
            {
                _ = RouteLoader.Parse(testData);
            }
            catch (FormatException)
            {
                return;
            }
            throw new Exception($"An exception was expected `FormatException`.");
        }

        [Fact(DisplayName = "������� stream � ��������� ������ �����")]
        public void TestParseStreamSkipeEmptySuccess()
        {
            const string testData =
 @"102;103
   

102;104
";
            byte[] byteArray = Encoding.ASCII.GetBytes(testData);
            using var ms = new MemoryStream(byteArray);
            using var sr = new StreamReader(ms);
            var routes = RouteLoader.Read(sr);
            if (routes.Count != 2)
            {
                throw new Exception($"2 routes were expected, {routes.Count} were considered.");
            }
        }
    }
}
