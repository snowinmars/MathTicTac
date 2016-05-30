using Config;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MathTicTac.PL.Monogame
{
	internal static class MyHttpClient
	{
		internal static async Task<Uri> DeleteAsync<T>(string afterApiUrl)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Configuration.ServerUrl);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				HttpResponseMessage response = await client.DeleteAsync(afterApiUrl).ConfigureAwait(continueOnCapturedContext: false);

				if (response.IsSuccessStatusCode)
				{
					return response.Headers.Location; // TODO return true
				}
			}

			return default(Uri); // TODO return false
		}

		/// <summary>
		/// Rest get method.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="afterApiUrl">If entire url looks like 'google.com/api/Controller/Action?id=2', so afterApiUrl is 'Controller/Action?id=2'</param>
		/// <returns></returns>
		internal static async Task<Uri> GetAsync(string afterApiUrl)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Configuration.ServerUrl);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				HttpResponseMessage response = await client.GetAsync(afterApiUrl).ConfigureAwait(continueOnCapturedContext: false);

				if (response.IsSuccessStatusCode)
				{
					return await response.Content.ReadAsAsync<Uri>().ConfigureAwait(continueOnCapturedContext: false);
				}
			}

			return default(Uri);
		}

		internal static async Task<Uri> PostAsync<T>(T obj, string afterApiUrl)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Configuration.ServerUrl);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				HttpResponseMessage response = await client.PostAsJsonAsync(afterApiUrl, obj).ConfigureAwait(continueOnCapturedContext: false);

				if (response.IsSuccessStatusCode)
				{
					return response.Headers.Location;
				}
			}

			return default(Uri); // TODO return 404
		}

		internal static async Task<Uri> PutAsync<T>(T obj, string afterApiUrl)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Configuration.ServerUrl);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				HttpResponseMessage response = await client.PutAsJsonAsync(afterApiUrl, obj).ConfigureAwait(continueOnCapturedContext: false);

				if (response.IsSuccessStatusCode)
				{
					return response.Headers.Location;
				}
			}

			return default(Uri); // TODO return 404
		}
	}
}