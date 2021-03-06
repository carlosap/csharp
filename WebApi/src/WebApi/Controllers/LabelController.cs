﻿using Microsoft.AspNet.Mvc;
using WebApi.Interfaces;
using WebApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Cache;
using System;
using WebApi.TraceInfo;
namespace WebApi.Controllers
{
    /// api/label?name=Account.SignIn.HeaderBig&lang=eng
    /// api/label?name=Account.SignIn.HeaderBig,Account.SignIn.Title&lang=eng
    /// api/label?name=Menu*
    /// api/label?name=*
    [Route("api/[controller]")]
    public class LabelController : Controller
    {
        public ILabel Labels { get; set; }
        public LabelController(ILabel labels){Labels = labels;}
        public async Task<object> Get([FromQuery]string name, string lang, string cache)
        {
            return await Task.Run(async () => {
                try
                {
                    var keyword = name;
                    var language = (string.IsNullOrWhiteSpace(lang)) ? "eng" : lang;
                    var cacheKey = $"label:{name}:{language}";
                    await AddCorOptions();
                    var isCache = (string.IsNullOrWhiteSpace(cache)) ? "yes" : "no";
                    if (isCache == "no") await CacheMemory.Remove(cacheKey);
                    var jsonResults = await CacheMemory.Get<List<DictionaryItem>>(cacheKey);
                    if (jsonResults != null) return jsonResults;
                    List<DictionaryItem> labels = await Labels.Get(keyword, language);
                    await CacheMemory.SetAndExpiresDays(cacheKey, labels, 1);
                    return labels;
                }
                catch (Exception ex)
                {
                    await Tracer.Exception("LabelController:Get", ex);
                    return null;
                }
            });
        }
        private async Task AddCorOptions()
        {
            await Task.Run(() =>
            {
                Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
                Response.Headers.Add("Access-Control-Allow-Headers",
                    new[] { "Origin, X-Requested-With, Content-Type, Accept" });
            });
        }
    }
}
