using Newtonsoft.Json;
using QuickApp.ViewModels.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QuickApp.Helpers
{
    public class HttpRequestHelper
    {        
        public static async Task<HttpResponseDto> GetRequestAsync(string uri, AuthenticationHeaderValue authenticationHeaderValue,
                                                                  List<KeyValuePair<string, string>> requestBody = null)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Authorization = authenticationHeaderValue;

                HttpResponseMessage httpResponse = new();
                if (requestBody != null)
                {
                    string uriWithQuery = ObjectToQueryString(requestBody);
                    httpResponse = await client.GetAsync($"{uri}?{uriWithQuery}");
                }
                else
                {
                    httpResponse = await client.GetAsync(uri);
                }

                return new HttpResponseDto
                {
                    StatusCode = httpResponse.StatusCode,
                    Content = await httpResponse.Content.ReadAsStringAsync()
                };
            }
        }

        public static async Task<HttpResponseDto> PostRequestAsync(string uri, AuthenticationHeaderValue authenticationHeaderValue,
                                                                  CreateDocumentDto createDocument)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();                
                client.DefaultRequestHeaders.Authorization = authenticationHeaderValue;

                HttpContent stringContent = new StringContent(createDocument.data);

                using (var form = new MultipartFormDataContent())
                {
                    using (var fileContent = new ByteArrayContent(System.Convert.FromBase64String(createDocument.file)))
                    {
                        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

                        // "file" parameter name should be the same as the server side input parameter name
                        form.Add(fileContent, "file", "test.pdf");
                        form.Add(stringContent, "data");
                        HttpResponseMessage response = await client.PostAsync(uri, form);

                        return new HttpResponseDto
                        {
                            StatusCode = response.StatusCode,
                            Content = await response.Content.ReadAsStringAsync()
                        };
                    }
                }
            }
        }

        public static async Task<HttpResponseDto> PostRequestAsyncJSON(string uri, AuthenticationHeaderValue authenticationHeaderValue,
                                                                       object body)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = authenticationHeaderValue;

                HttpResponseMessage requestResponse = new();

                var json = JsonConvert.SerializeObject(body);
                var requestData = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                requestResponse = await client.PostAsync(uri, requestData);

                return new HttpResponseDto
                {
                    StatusCode = requestResponse.StatusCode,
                    Content = await requestResponse.Content.ReadAsStringAsync()
                };
            }
        }

        //public static async Task<HttpResponseDto> PostRequestWithContentAsync(HttpRequestInput httpRequestInput)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", httpRequestInput.DrChronoAcceessToken);

        //        HttpResponseMessage requestResponse = new();
        //        HttpContent content = (HttpContent)httpRequestInput.Body;
        //        requestResponse = await client.PostAsync(httpRequestInput.Uri, content);

        //        return new HttpResponseDto
        //        {
        //            StatusCode = requestResponse.StatusCode,
        //            Content = await requestResponse.Content.ReadAsStringAsync()
        //        };
        //    }
        //}

        //public static async Task<HttpResponseDto> PatchRequestAsync(HttpRequestInput httpRequestInput)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", httpRequestInput.DrChronoAcceessToken);

        //        HttpResponseMessage requestResponse = new();
        //        List<KeyValuePair<string, string>> requestBody = ObjectToKeyValuePair(httpRequestInput.Body);
        //        FormUrlEncodedContent requestData = new FormUrlEncodedContent(requestBody);
        //        requestResponse = await client.PatchAsync(httpRequestInput.Uri, requestData);

        //        return new HttpResponseDto
        //        {
        //            StatusCode = requestResponse.StatusCode,
        //            Content = await requestResponse.Content.ReadAsStringAsync()
        //        };
        //    }
        //}

        //public static async Task<HttpResponseDto> PatchRequestWithContentAsync(HttpRequestInput httpRequestInput)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", httpRequestInput.DrChronoAcceessToken);

        //        HttpResponseMessage requestResponse = new();
        //        HttpContent content = (HttpContent)httpRequestInput.Body;
        //        requestResponse = await client.PatchAsync(httpRequestInput.Uri, content);

        //        return new HttpResponseDto
        //        {
        //            StatusCode = requestResponse.StatusCode,
        //            Content = await requestResponse.Content.ReadAsStringAsync()
        //        };
        //    }
        //}

        private static string ObjectToQueryString<T>(T obj) where T : class
        {
            StringBuilder sb = new();
            Type type = obj.GetType();
            var properties = type.GetProperties();
            foreach (PropertyInfo pi in properties)
            {
                if (pi.CanRead)
                {
                    if (pi.GetValue(obj, null) != null)
                    {
                        sb.Append($"{pi.Name}={pi.GetValue(obj, null).ToString()}&");
                    }
                }
            }

            return sb.ToString().TrimEnd('&');
        }

        private static List<KeyValuePair<string, string>> ObjectToKeyValuePair<T>(T obj) where T : class
        {
            List<KeyValuePair<string, string>> keyValuePairs = new();
            Type type = obj.GetType();
            var properties = type.GetProperties();
            foreach (PropertyInfo pi in properties)
            {
                if (pi.CanRead)
                {
                    keyValuePairs.Add(new KeyValuePair<string, string>(pi.Name, pi.GetValue(obj, null)?.ToString()));
                }
            }

            return keyValuePairs;
        }
    }
}
