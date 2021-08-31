using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CMSAdmin.Models;
using Nancy.Json;
using Newtonsoft.Json;

namespace CMSAdmin.Business
{
    public class EVoucherRepository
    {
        #region Authenticate
        public ResponseMessage Authenticate(string username, string password)
        {
            ResponseMessage model = new ResponseMessage();

            string url = "https://localhost:44382/api/EVoucher/authenticate" + "?username=" + username + "&password=" + password;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(url).Result;

            string stringData = response.Content.ReadAsStringAsync().Result;

            model = JsonConvert.DeserializeObject<ResponseMessage>(stringData);

            return model;
        }
        #endregion

        #region GetAll
        public List<EVoucher> GetAll(string token)
        {
            List<EVoucher> lst = new List<EVoucher>();

            string url = "https://localhost:44382/api/EVoucher";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            HttpResponseMessage response = client.GetAsync(url).Result;

            string stringData = response.Content.ReadAsStringAsync().Result;

            lst = JsonConvert.DeserializeObject<List<EVoucher>>(stringData);

            return lst;
        }
        #endregion

        #region Save
        public ResponseMessage Save(EVoucher obj,string token)
        {
            ResponseMessage res = new ResponseMessage();

            string url = "https://localhost:44382/api/EVoucher"; 
            var Content = new StringContent(new JavaScriptSerializer().Serialize(obj), UnicodeEncoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer "+token);
            HttpResponseMessage response = client.PostAsync(url, Content).Result;

            string stringData = response.Content.ReadAsStringAsync().Result;

            res = JsonConvert.DeserializeObject<ResponseMessage>(stringData);

            return res;
        }
        #endregion
    }
}
