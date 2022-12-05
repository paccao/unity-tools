using UnityEditor;
using static System.IO.Directory;
using static System.IO.Path;
using static UnityEngine.Application;
using static UnityEditor.AssetDatabase;

namespace joelplumppu
{
	public static class ToolsMenu
	{
		[MenuItem("Tools/Setup/Create Default Folders")]
		static void CreateDefaultFolders()
		{
			Dir("_Project", "Scripts", "Art", "Scenes");
			Refresh();
		}

		static void Dir(string root, params string[] dir)
		{
			var fullPath = Combine(dataPath, root);
			foreach (var newDirectory in dir)
				CreateDirectory(Combine(fullPath, newDirectory));
		}

		static string GetGistUrl(string id, string user = "paccao") =>
			$"https://gist.github.com/{user}/{id}/raw";

		static async Task<string> GetContents(string url)
		{
			using (var client = new HttpClient())
			{
				var response = await client.GetAsync(url);
				var content = await response.Content.ReadAsStringAsync();
				return content;
			}
		}

		static void ReplacePackageFile(string contents)
		{
			var existingPackageFile = Path.Combine(Application.dataPath, "../Packages/manifest.json");
			File.WriteAllText(existingPackageFile.contents);
			UnityEditor.PackageManager.Client.Resolve();
		}
	}
}