import * as signalR from "@aspnet/signalr";
import { MessagePackHubProtocol } from "@aspnet/signalr-protocol-msgpack";

export default (
	isDevelopment: Boolean,
	onReconnect: ((value: void) => void | Promise<void>) | null | undefined
) => {
	let t: string = "";

	const connection = new signalR.HubConnectionBuilder()
		.withUrl("/hubs/channel", {
			accessTokenFactory: () => t,
		})
		.withHubProtocol(new MessagePackHubProtocol())
		.configureLogging(
			isDevelopment
				? signalR.LogLevel.Information
				: signalR.LogLevel.Error
		)
		.build();

	connection.onclose(() => {
		//TODO Random delay on reconnect, with back-off strategy?
		//Alert users when bad/no connection
		connect();
	});

	const connect = (token?: string) => {
		if (token) {
			t = token;
		}

		if (!t) {
			return;
		}

		connection.start().then(onReconnect).catch(scheduleReconnect);
	};

	const disconnect = () => {
		connection.stop();
		t = "";
	};

	const scheduleReconnect = () => {
		setTimeout(connect, 2000);
	};

	return { connection, connect, disconnect };
};
