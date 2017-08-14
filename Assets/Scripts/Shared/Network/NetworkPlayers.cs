using System.Collections;
using System.Collections.Generic;

public class NetworkPlayers {
	public static Dictionary<int, NetworkTarget> map = new Dictionary<int, NetworkTarget>();
	public static List<NetworkTarget> list = new List<NetworkTarget>();

	// Add
	public static void Add(NetworkTarget player) {
		map.Add(player.connectionId, player);
		list.Add(player);
	}

	// Remove
	public static void Remove(NetworkTarget player) {
		map.Remove(player.connectionId);
		list.Remove(player);
	}

	// Find
	public static NetworkTarget Find(int connectionId) {
		return map[connectionId];
	}
}