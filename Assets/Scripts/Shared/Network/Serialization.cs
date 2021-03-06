using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class Serialization {
	// Read
	public static object Read(NetworkReader reader, Type type) {
		if(type == typeof(byte))
			return reader.ReadByte();
		
		if(type == typeof(short))
			return reader.ReadInt16();
		
		if(type == typeof(int))
			return reader.ReadInt32();
		
		if(type == typeof(long))
			return reader.ReadInt64();
		
		if(type == typeof(string))
			return reader.ReadString();
		
		if(type == typeof(float))
			return reader.ReadSingle();

		if(type == typeof(double))
			return reader.ReadDouble();
		
		if(type == typeof(Vector2))
			return reader.ReadVector2();
		
		if(type == typeof(Vector3))
			return reader.ReadVector3();
		
		if(type == typeof(Vector4))
			return reader.ReadVector4();
		
		if(type == typeof(Color))
			return reader.ReadColor();

		if(type == typeof(Color32))
			return reader.ReadColor32();

		if(type == typeof(Quaternion))
			return reader.ReadQuaternion();
		
		if(type.IsValueType)
			return Activator.CreateInstance(type);

		return null;
	}

	// Write
	public static void Write(NetworkWriter writer, object obj) {
		var type = obj.GetType();

		if(type == typeof(byte)) {
			writer.Write((byte) obj);
			return;
		}
		
		if(type == typeof(short)) {
			writer.Write((short) obj);
			return;
		}
		
		if(type == typeof(int)) {
			writer.Write((int) obj);
			return;
		}
		
		if(type == typeof(long)) {
			writer.Write((long) obj);
			return;
		}
		
		if(type == typeof(string)) {
			writer.Write((string) obj);
			return;
		}
		
		if(type == typeof(float)) {
			writer.Write((float) obj);
			return;
		}

		if(type == typeof(double)) {
			writer.Write((double) obj);
			return;
		}
		
		if(type == typeof(Vector2)) {
			writer.Write((Vector2) obj);
			return;
		}
		
		if(type == typeof(Vector3)) {
			writer.Write((Vector3) obj);
			return;
		}
		
		if(type == typeof(Vector4)) {
			writer.Write((Vector4) obj);
			return;
		}
		
		if(type == typeof(Color)) {
			writer.Write((Color) obj);
			return;
		}

		if(type == typeof(Color32)) {
			writer.Write((Color32) obj);
			return;
		}

		if(type == typeof(Quaternion)) {
			writer.Write((Quaternion) obj);
			return;
		}
		
		Debug.LogError("Can't serialize unknown object type: " + type);
	}
}