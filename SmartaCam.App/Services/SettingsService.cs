using Microsoft.AspNetCore.Mvc;
using static Dropbox.Api.Files.ListRevisionsMode;
using System.Net.Http;
using System.Text.Json;
using SmartaCam.App.Services;
using Newtonsoft.Json;

namespace SmartaCam
{
    public interface ISettingsService
    {
        Task<IActionResult> SetNormalize(bool willNormalize);
        Task<bool> GetNormalize();
        Task<IActionResult> SetUpload(bool willUpload);
        Task<bool> GetUpload();
        Task<IActionResult> SetCopyToUsb(bool willCopy);
        Task<bool> GetCopyToUsb();
        Task<bool> GetNetworkStatus();
        Task<bool> GetDropBoxAuthStatus();
		Task<string> GetDropBoxCode();
        Task<string> GetRemovableDrivePath();
		Task<IActionResult> SetRemovableDrivePath(string removableDrivePath);
		Task<List<string>>? GetRemovableDrivePaths();

		Task<IActionResult> SetDropBoxCode(string dropboxcode);
        Task<IActionResult> UnAuthorizeDropBox();

	}

    public class SettingsService : ISettingsService
    {
        private readonly HttpClient _httpClient;
        public SettingsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> GetNormalize()
        {
            return await System.Text.Json.JsonSerializer.DeserializeAsync<bool>
            // return OK(await _httpClient.GetStreamAsync($"api/getnormalized"))
            (await _httpClient.GetStreamAsync($"api/getnormalize"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<IActionResult> SetNormalize(bool willNormalize)
        {
            return await _httpClient.GetAsync($"api/setnormalize/{willNormalize}") as IActionResult;
        }
        public async Task<bool> GetUpload()
        {
            return await System.Text.Json.JsonSerializer.DeserializeAsync<bool>
            // return OK(await _httpClient.GetStreamAsync($"api/getnormalized"))
            (await _httpClient.GetStreamAsync($"api/getpush"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<IActionResult> SetUpload(bool willUpload)
        {
            return await _httpClient.GetAsync($"api/setpush/{willUpload}") as IActionResult;
        }
        public async Task<string> GetRemovableDrivePath()
         {
            return await _httpClient.GetStringAsync($"api/removablepath");
            //return await System.Text.Json.JsonSerializer.DeserializeAsync<bool>
            // return OK(await _httpClient.GetStreamAsync($"api/getnormalized"))
           // (await _httpClient.GetStreamAsync($"api/getcopy"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
		public async Task<IActionResult> SetRemovableDrivePath(string removableDrivePath)
		{
			return await _httpClient.GetAsync($"api/setremovablepath/{removableDrivePath}") as IActionResult;
		}
		public async Task<List<string>?> GetRemovableDrivePaths()
		{
			return await System.Text.Json.JsonSerializer.DeserializeAsync<List<string>>
			// return OK(await _httpClient.GetStreamAsync($"api/getnormalized"))
			(await _httpClient.GetStreamAsync($"api/removablepaths"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
		}
		public async Task<bool> GetCopyToUsb()
        {
            return await System.Text.Json.JsonSerializer.DeserializeAsync<bool>
            // return OK(await _httpClient.GetStreamAsync($"api/getnormalized"))
            (await _httpClient.GetStreamAsync($"api/getcopy"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<IActionResult> SetCopyToUsb(bool willCopy)
        {
            return await _httpClient.GetAsync($"api/setcopy/{willCopy}") as IActionResult;
        }
        public async Task<bool> GetNetworkStatus()
        {
            return await System.Text.Json.JsonSerializer.DeserializeAsync<bool>
            // return OK(await _httpClient.GetStreamAsync($"api/getnormalized"))
            (await _httpClient.GetStreamAsync($"api/getnetwork"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
		public async Task<bool> GetDropBoxAuthStatus()
		{
			return await System.Text.Json.JsonSerializer.DeserializeAsync<bool>
			// return OK(await _httpClient.GetStreamAsync($"api/getnormalized"))
			(await _httpClient.GetStreamAsync($"api/getdropboxstatus"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
		}
		public async Task<string> GetDropBoxCode()
		{
			return await _httpClient.GetStringAsync($"api/getdropboxcode");
		//	return await System.Text.Json.JsonSerializer.DeserializeAsync<string>
		//	(await _httpClient.GetStringAsync($"api/getdropboxcode"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
		}
		public async Task<IActionResult> SetDropBoxCode(string dropboxcode)
		{
			return await _httpClient.GetAsync($"api/setdropboxcode/{dropboxcode}") as IActionResult;

		}
		public async Task<IActionResult> UnAuthorizeDropBox()
		{
			return await _httpClient.GetAsync($"api/unauthdropbox") as IActionResult;

		}

	}
    }