import { SignInSuccess, SignOut } from "store/auth";

type KnownAction = SignInSuccess | SignOut;

export default (connect: (token: string) => void, disconnect: () => void) =>
	() =>
	(next: any) =>
	(action: KnownAction) => {
		switch (action.type) {
			case "SIGN_IN_SUCCESS":
				connect(action.data.token);
				break;
			case "SIGN_OUT":
				disconnect();
				break;
			default:
				break;
		}
		return next(action);
	};
