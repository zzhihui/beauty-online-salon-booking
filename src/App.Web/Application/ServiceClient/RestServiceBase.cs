using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using App.Web.Application.Utility;

namespace App.Web.Application.ServiceClient
{
    public class RestServiceBase
    {
        private const string AuthTokenKey = "BeautyFairyAuthToken";
        private const string AuthTokenAdminKey = "BeautyFairyAdminAuthToken";
        
        protected HttpResponseMessage GetService(string url)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                var httpClient = new HttpClient();
                Trace.WriteLine(url);
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                ApplySecurityToken(request);
                return SendAsyncRequest(httpClient, request);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                throw;
            }
            
        }
        
        protected HttpResponseMessage PostService(string url, HttpContent content)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            ApplySecurityToken(request);
            request.Content = content;

            return SendAsyncRequest(httpClient, request);
        }

        protected HttpResponseMessage PutService(string url, HttpContent content)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            ApplySecurityToken(request);
            request.Content = content;

            return SendAsyncRequest(httpClient, request);
        }

        protected HttpResponseMessage DeleteService(string url)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            ApplySecurityToken(request);

            return SendAsyncRequest(httpClient, request);
        }

        private HttpResponseMessage SendAsyncRequest(HttpClient httpClient, HttpRequestMessage request)
        {
            var response = httpClient.SendAsync(request);
            var reponseMessage = response.Result;
            Trace.WriteLine(reponseMessage);
            if (!reponseMessage.IsSuccessStatusCode)
                throw new HttpResponseException(reponseMessage.StatusCode);

            return response.Result;
        }
        
        public RestResponse<TResponse> Execute<TResponse>(Func<TResponse> action)
        {
            try
            {
                var response = action();
                return new RestResponse<TResponse>()
                {
                    IsSucceed = true,
                    Content = response
                };
            }
            catch (HttpResponseException ex)
            {
                return new RestResponse<TResponse>()
                {
                    IsSucceed = false,
                    ErrorMessage = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new RestResponse<TResponse>()
                {
                    IsSucceed = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public RestResponse Execute(Action action)
        {
            try
            {
                action();
                return new RestResponse()
                {
                    IsSucceed = true
                };
            }
            catch (HttpResponseException ex)
            {
                return new RestResponse()
                {
                    IsSucceed = false,
                    ErrorMessage = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new RestResponse()
                {
                    IsSucceed = false,
                    ErrorMessage = ex.Message
                };
            }
        }
        
        private static void ApplySecurityToken(HttpRequestMessage request)
        {
            request.Headers.Add(AuthTokenKey, Constants.AuthKey1);
            request.Headers.Add(AuthTokenAdminKey, Constants.AuthKey2);
        }
    }
}