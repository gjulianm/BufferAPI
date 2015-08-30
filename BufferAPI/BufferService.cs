using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AncoraMVVM.Rest;
using Newtonsoft.Json;

namespace BufferAPI
{
    /// <summary>
    /// This class manages a connection and exposes the methods to manage
    /// a Buffer account.
    /// </summary>
    public class BufferService : BaseService
    {
        string token;
        JsonSerializer serializer;

        /// <summary>
        /// Construct the Buffer service.
        /// </summary>
        /// <param name="access_token">OAuth access token for this user. <see href="https://buffer.com/developers/api/oauth">Buffer API</see>
        /// to see how to retrieve it from Buffer.</param>
        public BufferService(string access_token)
            : base(new HttpService())
        {
            token = access_token;

            serializer = new JsonSerializer();

            Authority = "https://api.bufferapp.com/";
            BasePath = "1/";

            PersistentUrlParameters.Add("access_token", token);
        }

        protected override T Deserialize<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }

        #region Methods
        /// <summary>
        /// Retrieves the details of the current user.
        /// </summary>
        /// <returns>HttpResponse containing the response details and, if no error was present, 
        /// the details of the user in the Content property.</returns>
        public async Task<HttpResponse<BufferUser>> GetUser()
        {
            return await CreateAndExecute<BufferUser>("user.json", HttpMethod.Get);
        }

        /// <summary>
        /// Get the list of all social media profiles associated to this account.
        /// </summary>
        /// <returns>HttpResponse with the response details and, if no error occured, the 
        /// list of profiles in the Content property.</returns>
        public async Task<HttpResponse<IEnumerable<BufferProfile>>> GetProfiles()
        {
            return await CreateAndExecute<IEnumerable<BufferProfile>>("profiles.json", HttpMethod.Get);
        }

        /// <summary>
        /// Get the details of a social media profile with a certain ID.
        /// </summary>
        /// <param name="id">ID of the social media profile.</param>
        /// <returns>HttpResponse with the response details and, if no error occured, the 
        /// profile details in the Content property</returns>
        public async Task<HttpResponse<BufferProfile>> GetProfile(string id)
        {
            var response = await CreateAndExecute<IEnumerable<BufferProfile>>("profiles.json", HttpMethod.Get, "id", id);

            BufferProfile profile = null;

            if (response.Content != null && response.Content.Any())
                profile = response.Content.First();

            return new HttpResponse<BufferProfile>(profile, response);
        }

        /// <summary>
        /// Posts an update to the given social media profiles.
        /// </summary>
        /// <param name="text">Update text.</param>
        /// <param name="profile_ids">List with all the social media profile ids.</param>
        /// <returns>HttpResponse with a BufferUpdateCreating object describing the result of the operation.</returns>
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
