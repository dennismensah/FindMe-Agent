﻿using FindMe;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FindMe.Services
{
    public class Activation
    {

        public async Task<Response> GetActivations()
        {
            Rest restService = new Rest();
            int id = Application.Current.Properties.ContainsKey("Id") ? (int) Application.Current.Properties["Id"] : 0;
            Response response = await restService.GetAsync($"CardActivations/{id}");
            return response;
        }
    }
}
