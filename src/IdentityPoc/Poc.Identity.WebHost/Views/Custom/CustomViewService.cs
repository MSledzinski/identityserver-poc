using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Thinktecture.IdentityServer.Core.Models;
using Thinktecture.IdentityServer.Core.Services;
using Thinktecture.IdentityServer.Core.ViewModels;

namespace Poc.Identity.WebHost.Views.Custom
{
    public class CustomViewService : IViewService
    {
        readonly IClientStore clientStore;

        public CustomViewService(IClientStore clientStore)
        {
            this.clientStore = clientStore;
        }

        public virtual async Task<System.IO.Stream> Login(LoginViewModel model, SignInMessage message)
        {
            // here we know tenant - and we have control over rendering process
            // so we can do whatever we want
            var tenant = message.Tenant;
            return await Render(model, "login", tenant);
        }

        public virtual Task<System.IO.Stream> Logout(LogoutViewModel model)
        {
            return Render(model, "logout");
        }

        public virtual Task<System.IO.Stream> LoggedOut(LoggedOutViewModel model)
        {
            return Render(model, "loggedOut");
        }

        public virtual Task<System.IO.Stream> Consent(ConsentViewModel model)
        {
            return Render(model, "consent");
        }

        public Task<Stream> ClientPermissions(ClientPermissionsViewModel model)
        {
            return Render(model, "permissions");
        }

        public virtual Task<System.IO.Stream> Error(ErrorViewModel model)
        {
            return Render(model, "error");
        }

        protected virtual Task<System.IO.Stream> Render(CommonViewModel model, string page, string tenantName = null)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() });

            string html = LoadHtml(page);
            html = Replace(html, new
            {
                siteName = Microsoft.Security.Application.Encoder.HtmlEncode(model.SiteName),
                model = Microsoft.Security.Application.Encoder.HtmlEncode(json),
                tenant_name = tenantName
            });

            return Task.FromResult(StringToStream(html));
        }

        private string LoadHtml(string name)
        {
            // primitive - but works for POC
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"content\app");
            file = Path.Combine(file, name + ".html");
            return File.ReadAllText(file);
        }

        private string Replace(string value, IDictionary<string, object> values)
        {
            foreach (var key in values.Keys)
            {
                var val = values[key];
                val = val ?? String.Empty;
                if (val != null)
                {
                    value = value.Replace("{" + key + "}", val.ToString());
                }
            }
            return value;
        }

        private string Replace(string value, object values)
        {
            return Replace(value, Map(values));
        }

        private IDictionary<string, object> Map(object values)
        {
            var dictionary = values as IDictionary<string, object>;

            if (dictionary == null)
            {
                dictionary = new Dictionary<string, object>();
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(values))
                {
                    dictionary.Add(descriptor.Name, descriptor.GetValue(values));
                }
            }

            return dictionary;
        }

        private Stream StringToStream(string s)
        {
            using (var ms = new MemoryStream())
            {
                using (var sw = new StreamWriter(ms))
                {
                    sw.Write(s);
                    sw.Flush();
                }

                ms.Seek(0, SeekOrigin.Begin);
                return ms;
            }
        }
    }
}