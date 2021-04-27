using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace UniqueCameras
{
    class Program
    {
        static void Main()
        {
			var json = File.ReadAllText("sites.json");
			var sites = JsonSerializer.Deserialize<IEnumerable<Site>>(json);

			var uniqueCameraSets = sites
				.GroupBy(s => s.cameras, HashSet<string>.CreateSetComparer())
				.Select(s => new
				{
					Cameras = s.Key,
					Count = s.Count()
				})
				.OrderByDescending(s => s.Count)
				.ThenBy(s => string.Join(',', s.Cameras));

			foreach (var uniqueCameraSet in uniqueCameraSets)
			{
				Console.WriteLine($"{string.Join(',', uniqueCameraSet.Cameras)} = {uniqueCameraSet.Count}");
            }
		}
    }
}
