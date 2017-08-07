using System.Collections.Generic;

public partial class Message
{
	private delegate Google.Protobuf.IMessage MessageCreator(byte[] data, int offset, int length);
	
	// 如果发送MessageType定义不存在, 则重新修改gen_message.gen中的命名, 可参考Proto.cs中关于MessageType的定义
	private static readonly Dictionary<int, MessageType> s_mapMessageType = new Dictionary<int, MessageType>
	{
		{ 0, MessageType.ServerRegister },
		{ 1, MessageType.ServerKeepAlive },
		{ 2, MessageType.PrepareLoginPlatformUniqueId },
		{ 3, MessageType.LoginPlatformUniqueId },
		{ 4, MessageType.LoginPlatformSidToken },
		{ 5, MessageType.ReLogin },
		{ 6, MessageType.Kick },
		{ 7, MessageType.EnterGameWorld },
		{ 8, MessageType.AgentDisConnect },
		{ 9, MessageType.KeepAlive },
	};

	private static readonly Dictionary<int, MessageCreator> s_mapMessageCreator = new Dictionary<int, MessageCreator>
	{
		{ 0, delegate (byte[] data, int offset, int length) { return MsgServerRegister.Parser.ParseFrom(data, offset, length); } },
		{ 1, delegate (byte[] data, int offset, int length) { return MsgServerKeepAlive.Parser.ParseFrom(data, offset, length); } },
		{ 2, delegate (byte[] data, int offset, int length) { return MsgPrepareLoginPlatformUniqueId.Parser.ParseFrom(data, offset, length); } },
		{ 3, delegate (byte[] data, int offset, int length) { return MsgLoginPlatformUniqueId.Parser.ParseFrom(data, offset, length); } },
		{ 4, delegate (byte[] data, int offset, int length) { return MsgLoginPlatformSidToken.Parser.ParseFrom(data, offset, length); } },
		{ 5, delegate (byte[] data, int offset, int length) { return MsgReLogin.Parser.ParseFrom(data, offset, length); } },
		{ 6, delegate (byte[] data, int offset, int length) { return MsgKick.Parser.ParseFrom(data, offset, length); } },
		{ 7, delegate (byte[] data, int offset, int length) { return MsgEnterGameWorld.Parser.ParseFrom(data, offset, length); } },
		{ 8, delegate (byte[] data, int offset, int length) { return MsgAgentDisConnect.Parser.ParseFrom(data, offset, length); } },
		{ 9, delegate (byte[] data, int offset, int length) { return MsgKeepAlive.Parser.ParseFrom(data, offset, length); } },
	};
}
