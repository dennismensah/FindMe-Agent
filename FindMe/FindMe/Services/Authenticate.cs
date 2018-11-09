using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FindMe.Models;
using Newtonsoft.Json;

namespace FindMe.Services
{
    public class Authenticate
    {
        public async Task<Response> Login(LoginModel model)
        {
            Rest restService = new Rest();
            Response response = await restService.PostAsync("login", model);
            return response;
        }
    }
}
