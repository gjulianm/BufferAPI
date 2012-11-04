using System;
using System.Collections.Generic;
using System.IO;
using Hammock;
using Hammock.Web;
using Newtonsoft.Json;

namespace BufferAPI
{
    public class BufferService
    {
        string token;
        RestClient client;
        JsonSerializer serializer;

        public BufferService(string access_token)
        {
            token = access_token;

            serializer = new JsonSerializer();

            client = new RestClient();
            client.Authority = "https://api.bufferapp.com/";
            client.VersionPath = "/1/";
        }

        void WithHammock<T>(string url, WebMethod method, WebParameterCollection parameters, Action<T, BufferResponse> callback)
        {
            RestRequest request = new RestRequest();
            request.Path = url;
            request.Method = method;

            if (parameters != null)
                foreach (var parameter in parameters)
                    request.AddParameter(parameter.Name, parameter.Value);

            AddAuthentication(request);

            client.BeginRequest(request, ClientCallback<T>, callback);
        }

        void AddAuthentication(RestRequest request)
        {
            request.AddParameter("access_token", token);
        }

        void ClientCallback<T>(RestRequest request, RestResponse response, object userState)
        {
            Action<T, BufferResponse> callback = userState as Action<T, BufferResponse>;
            T deserialized;

            BufferResponse bResponse = new BufferResponse
            {
                Contents = response.Content,
                Request = request,
                StatusCode = response.StatusCode,
                StatusDescription = response.StatusDescription,
                Response = response,
                Sender = this
            };

            if (typeof(T) == typeof(string))
                deserialized = (T)(object)response.Content;
            else
                deserialized = DeserializeObject<T>(response, bResponse);

            if (callback != null)
                callback(deserialized, bResponse);
        }

        private T DeserializeObject<T>(RestResponse response, BufferResponse bResponse)
        {
            T deserialized = default(T);

            using (var textReader = new StringReader(response.Content))
            {
                using (var reader = new JsonTextReader(textReader))
                {
                    try
                    {
                        deserialized = serializer.Deserialize<T>(reader);
                    }
                    catch (Exception e)
                    {
                        bResponse.InternalException = e;
                    }
                }
            }

            return deserialized;
        }

        #region Methods
        public void GetUser(Action<BufferUser, BufferResponse> callback)
        {
            WithHammock<BufferUser>("user.json", WebMethod.Get, null, callback);
        }

        public void GetProfiles(Action<IEnumerable<BufferProfile>, BufferResponse> callback)
        {
            WithHammock<IEnumerable<BufferProfile>>("profiles.json", WebMethod.Get, null, callback);
        }

        public void GetProfile(string id, Action<BufferProfile, BufferResponse> callback)
        {
            WebParameterCollection param = new WebParameterCollection();
            param.Add("id", id);
            WithHammock<BufferProfile>("profiles.json", WebMethod.Get, param, callback);
        }

        public void PostUpdate(string text, IEnumerable<string> profile_ids, Action<BufferUpdateCreation, BufferResponse> callback)
        {
            WebParameterCollection param = new WebParameterCollection();

            foreach (var id in profile_ids)
                param.Add("profile_ids[]", id);

            param.Add("text", text);
            WithHammock<BufferUpdateCreation>("updates/create.json", WebMethod.Post, param, callback);
        }
        #endregion
    }
}
