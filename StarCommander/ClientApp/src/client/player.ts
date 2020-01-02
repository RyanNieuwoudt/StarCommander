import { http } from "client/api";

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
