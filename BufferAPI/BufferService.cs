using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AncoraMVVM.Rest;
using Newtonsoft.Json;
using System;

namespace BufferAPI
{
    /// <summary>
    /// This class manages a connection and exposes the methods to manage
    /// a Buffer account.
    /// </summary>
    public sealed class BufferService : BaseService
    {
        /// <summary>
        /// Construct the Buffer service.
        /// </summary>
        /// <param name="accessToken">OAuth access token for this user. <see href="https://buffer.com/developers/api/oauth">Buffer API</see>
        /// to see how to retrieve it from Buffer.</param>
        public BufferService(string accessToken)
            : base(new HttpService())
        {
            Authority = "https://api.bufferapp.com/";
            BasePath = "1/";

            PersistentUrlParameters.Add("access_token", accessToken);
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
        /// <param name="profileIds">List with all the social media profile ids.</param>
        /// <returns>HttpResponse with a BufferUpdateCreating object describing the result of the operation.</returns>
        public async Task<HttpResponse<BufferUpdateCreation>> PostUpdate(string text, IEnumerable<string> profileIds)
        {
            ParameterCollection param = new ParameterCollection();

            foreach (var id in profileIds)
                param.Add("profile_ids[]", id);

            param.Add("text", text);
            var req = CreateRequest("updates/create.json", HttpMethod.Post, param);

            return await Execute<BufferUpdateCreation>(req);
        }

        /// <summary>
        /// Posts an update to the given social media profiles.
        /// </summary>
        /// <param name="text">Update text.</param>
        /// <param name="profiles">List with all the social media profiles.</param>
        /// <returns>HttpResponse with a BufferUpdateCreating object describing the result of the operation.</returns>
        public async Task<HttpResponse<BufferUpdateCreation>> PostUpdate(string text,
            IEnumerable<BufferProfile> profiles)
        {
            return await PostUpdate(text, profiles.Select(p => p.Id));
        }

        /// <summary>
        /// Posts an update to the given social media profiles.
        /// </summary>
        /// <param name="update">BufferUpdateCreate object describing the update to create.</param>
        /// <param name="profileIds">List with all the social media profile ids.</param>
        /// <returns>HttpResponse with a BufferUpdateCreating object describing the result of the operation.</returns>
        public async Task<HttpResponse<BufferUpdateCreation>> PostUpdate(BufferUpdateCreate update, IEnumerable<string> profileIds)
        {
            ParameterCollection param = new ParameterCollection();

            foreach (var id in profileIds)
                param.Add("profile_ids[]", id);

            param.Add("text", update.Text);
            param.Add("shorten", update.Shorten.ToString().ToLower());
            param.Add("now", update.Now.ToString().ToLower());
            param.Add("top", update.Top.ToString().ToLower());
            param.Add("attachment", update.Attachment.ToString().ToLower());


            // schedule
            if (update.ScheduledAt != null && update.ScheduledAt != DateTime.MinValue )
                param.Add("scheduled_at", update.ScheduledAtSeconds);

            // media
            if (update.Media.Link != null && update.Media.Link.Trim() != "")
                param.Add("media[link]", update.Media.Link);

            if (update.Media.Description != null && update.Media.Description.Trim()  != "")
                param.Add("media[description]", update.Media.Description);

            if (update.Media.Title != null && update.Media.Title.Trim() != "")
                param.Add("media[title]", update.Media.Title);

            if (update.Media.Picture != null && update.Media.Picture.Trim() != "")
                param.Add("media[picture]", update.Media.Picture);

            if (update.Media.Photo != null && update.Media.Photo.Trim() != "")
                param.Add("media[photo]", update.Media.Photo);

            if (update.Media.Thumbnail != null && update.Media.Thumbnail.Trim() != "")
                param.Add("media[thumbnail]", update.Media.Thumbnail);


            // retweet
            if (update.Retweet.TweetId != null && update.Retweet.TweetId.Trim() != "")
                param.Add("retweet[tweet_id]", update.Retweet.TweetId);

            if (update.Retweet.Comment != null && update.Retweet.Comment.Trim() != "")
                param.Add("retweet[comment]", update.Retweet.Comment);


            var req = CreateRequest("updates/create.json", HttpMethod.Post, param);

            return await Execute<BufferUpdateCreation>(req);
        }

        /// <summary>
        /// Posts an update to the given social media profiles.
        /// </summary>
        /// <param name="update">BufferUpdateCreate object describing the update to create.</param>
        /// <param name="profiles">List with all the social media profiles.</param>
        /// <returns>HttpResponse with a BufferUpdateCreating object describing the result of the operation.</returns>
        public async Task<HttpResponse<BufferUpdateCreation>> PostUpdate(BufferUpdateCreate update, IEnumerable<BufferProfile> profiles)
        {
            return await PostUpdate(update, profiles.Select(p => p.Id));
        }

        #endregion
    }
}
