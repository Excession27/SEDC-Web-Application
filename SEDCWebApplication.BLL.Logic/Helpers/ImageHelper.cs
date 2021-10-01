using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDCWebApplication.BLL.Logic.Helpers
{
    public class ImageHelper
    {
        private readonly IConfiguration _configuration;
        public ImageHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetFullImagePath(string relativeImagePath)
        {
            return _configuration.GetSection("AppSettings")["ImageRoot"] + relativeImagePath;
        }
    }
}
