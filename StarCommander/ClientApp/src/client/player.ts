import { http } from "client/api";

export const signUp = ({
	callSign,
	firstName,
	lastName
}: { callSign?: string; firstName?: string; lastName?: string } = {}) =>
	http.post("api/player/player", { callSign, firstName, lastName });
