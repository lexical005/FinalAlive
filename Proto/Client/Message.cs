
public partial class Message
{
	private delegate Google.Protobuf.IMessage MessageCreator(byte[] data, int offset, int length);
	
	private static readonly Dictionary<MessageType, MessageCreator> s_mapMessageCreator = new Dictionary<MessageType, MessageCreator>
	{
		{ 0, MessageType.ServerRegister },
		{ 1, MessageType.ServerKeepAlive },
		{ 2, MessageType.PrepareLoginPlatformUniqueID },
		{ 3, MessageType.LoginPlatformUniqueID },
		{ 4, MessageType.LoginPlatformSidToken },
		{ 5, MessageType.ReLogin },
		{ 6, MessageType.Kick },
		{ 7, MessageType.EnterGameWorld },
		{ 8, MessageType.AgentDisConnect },
		{ 9, MessageType.KeepAlive },
	};

	private static readonly Dictionary<int, MessageCreator> s_mapMessageCreator = new Dictionary<int, MessageCreator>
	{
		{ 0, delegate (byte[] data, int offset, int length) { return ServerRegister.Parser.ParseFrom(data, offset, length); } },
		{ 1, delegate (byte[] data, int offset, int length) { return ServerKeepAlive.Parser.ParseFrom(data, offset, length); } },
		{ 2, delegate (byte[] data, int offset, int length) { return PrepareLoginPlatformUniqueID.Parser.ParseFrom(data, offset, length); } },
		{ 3, delegate (byte[] data, int offset, int length) { return LoginPlatformUniqueID.Parser.ParseFrom(data, offset, length); } },
		{ 4, delegate (byte[] data, int offset, int length) { return LoginPlatformSidToken.Parser.ParseFrom(data, offset, length); } },
		{ 5, delegate (byte[] data, int offset, int length) { return ReLogin.Parser.ParseFrom(data, offset, length); } },
		{ 6, delegate (byte[] data, int offset, int length) { return Kick.Parser.ParseFrom(data, offset, length); } },
		{ 7, delegate (byte[] data, int offset, int length) { return EnterGameWorld.Parser.ParseFrom(data, offset, length); } },
		{ 8, delegate (byte[] data, int offset, int length) { return AgentDisConnect.Parser.ParseFrom(data, offset, length); } },
		{ 9, delegate (byte[] data, int offset, int length) { return KeepAlive.Parser.ParseFrom(data, offset, length); } },
	};
}
