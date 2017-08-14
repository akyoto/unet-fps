using System.Collections;
using System.Collections.Generic;

public class NetworkPlayers {
	public static Dictionary<int, NetworkPlayer> map = new Dictionary<int, NetworkPlayer>();
	public static List<NetworkPlayer> list = new List<NetworkPlayer>();

	// Add
	public static void Add(NetworkPlayer player) {
		map.Add(player.connectionId, player);
		list.Add(player);
	}

	// Remove
	public static void Remove(NetworkPlayer player) {
		map.Remove(player.connectionId);
		list.Remove(player);
	}

	// Find
	public static NetworkPlayer Find(int connectionId) {
		return map[connectionId];
	}
}