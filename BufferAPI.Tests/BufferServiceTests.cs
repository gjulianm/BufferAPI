using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using AncoraMVVM.Rest;
using System.Linq;
using System.Collections.Generic;

namespace BufferAPI.Tests
{
    [TestClass]
    public class BufferServiceTests
    {
        public BufferService Service { get { return new BufferService(PrivateData.Token); } }

        private async Task<T> TestEndpoint<T>(Func<Task<HttpResponse<T>>> task)
        {
            var response = await task();

            Assert.IsTrue(response.Succeeded, "Request not suceeded. Error code {0}, inner exception {1}, response {2}", response.StatusCode, response.InnerException, response.StringContents);

            return response.Content;
        }

        [TestMethod]
        public async Task GetUser()
        {
            await TestEndpoint(Service.GetUser);
        }

        [TestMethod]
        public async Task GetProfiles()
        {
            var result = await TestEndpoint(Service.GetProfiles);

            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public async Task GetProfile()
        {
            await TestEndpoint(() => Service.GetProfile(PrivateData.TestProfileId));
        }

        [TestMethod]
        public async Task PostUpdate()
        {
            await TestEndpoint(() => Service.PostUpdate("Test update", new List<string> { PrivateData.TestProfileId }));
        }
    }
}
