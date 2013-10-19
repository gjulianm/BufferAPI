using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using AncoraMVVM.Rest;
using System.Linq;

namespace BufferAPI
{
    public class BufferService : BaseService
    {
        string token;
        JsonSerializer serializer;

        public BufferService(string access_token)
            : base(new HttpService())
        {
            token = access_token;

            serializer = new JsonSerializer();

            Authority = "https://api.bufferapp.com/";
            BasePath = "1/";

            PersistentUrlParameters.Add("access_token", token);
        }
        
        private T Deserialize<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }

        #region Methods
        public async Task<HttpResponse<BufferUser>> GetUser()
        {
            return await CreateAndExecute<BufferUser>("user.json", HttpMethod.Get);
        }

        public async Task<HttpResponse<IEnumerable<BufferProfile>>> GetProfiles()
        {
            return await CreateAndExecute<IEnumerable<BufferProfile>>("profiles.json", HttpMethod.Get);
        }

        public async Task<HttpResponse<BufferProfile>> GetProfile(string id, Action<BufferProfile, BufferResponse> callback)
        {
            return await CreateAndExecute<BufferProfile>("profiles.json", HttpMethod.Get, "id", id);
        }

        public async Task<HttpResponse<BufferUpdateCreation>> PostUpdate(string text, IEnumerable<string> profile_ids)
        {
            ParameterCollection param = new ParameterCollection();

            foreach (var id in profile_ids)
                param.Add("profile_ids[]", id);

            param.Add("text", text);
            var req = CreateRequest("updates/create.json", HttpMethod.Post, param);

            return await Execute<BufferUpdateCreation>(req);
        }
        #endregion
    }
}
