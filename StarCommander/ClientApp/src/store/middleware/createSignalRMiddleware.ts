import { SignInSuccessAction, SignOutAction } from "store/auth";

type KnownAction = SignInSuccessAction | SignOutAction;

export default (connect: (token: string) => void, disconnect: () => void) => {
	return (store: any) => (next: any) => (action: KnownAction) => {
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
};
