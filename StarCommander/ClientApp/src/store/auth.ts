import { Action, Reducer } from "redux";

export interface AuthState {
	callSign?: string;
	firstName?: string;
	lastName?: string;
	token?: string;
}

export interface SignUpAction {
	type: "SIGN_UP";
	payload: {
		callSign: string;
		firstName: string;
		lastName: string;
	};
}

export type KnownAction = SignUpAction;

export const actionCreators = {
	increment: () => ({ type: "SIGN_UP" } as SignUpAction)
};

const defaultState: AuthState = {};

export const reducer: Reducer<AuthState> = (
	state: AuthState | undefined = defaultState,
	incomingAction: Action
): AuthState => {
	const action = incomingAction as KnownAction;
	switch (action.type) {
		case "SIGN_UP": {
			const token = "token";
			const { callSign, firstName, lastName } = action.payload;
			return { callSign, firstName, lastName, token };
		}
		default:
			return state;
	}
};
