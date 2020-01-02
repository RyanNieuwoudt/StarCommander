import { http } from "client/api";

export const signIn = ({
	callSign,
	password
}: { callSign?: string; password?: string } = {}) =>
	http.post("api/player/session", { callSign, password });

export const signUp = ({
	callSign,
	firstName,
	lastName,
	password
}: {
	callSign?: string;
	firstName?: string;
	lastName?: string;
	password?: string;
} = {}) =>
	http.post("api/player/player", { callSign, firstName, lastName, password });

export const updateName = ({
	firstName,
	lastName,
	token
}: { firstName?: string; lastName?: string; token?: string } = {}) =>
	http.post("api/player/name", { firstName, lastName }, token);
