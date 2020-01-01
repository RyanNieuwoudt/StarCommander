import { Action, Reducer } from "redux";
import { all, takeLeading } from "redux-saga/effects";
import { signUp } from "client/player";
import { querySaga } from "store/saga/templates";

export interface AuthState {
	callSign?: string;
	firstName?: string;
	lastName?: string;
	token?: string;
}

export interface SignOutAction {
	type: "SIGN_OUT";
}

export interface SignUpAction {
	type: "SIGN_UP";
	payload: {
		callSign: string;
		firstName: string;
		lastName: string;
	};
}

export interface SignUpSuccessAction {
	type: "SIGN_UP_SUCCESS";
	payload: {
		callSign: string;
		firstName: string;
		lastName: string;
	};
	data: {
		player: {
			callSign: string;
			firstName: string;
			lastName: string;
		};
		token: string;
	};
}

export type KnownAction = SignOutAction | SignUpAction | SignUpSuccessAction;

export const actionCreators = {
	signOut: () => ({ type: "SIGN_OUT" }),
	signUp: (callSign: string, firstName: string, lastName: string) =>
		({
			type: "SIGN_UP",
			payload: { callSign, firstName, lastName }
		} as SignUpAction)
};

export const rootSaga = function* root() {
	yield all([yield takeLeading("SIGN_UP", querySaga, signUp)]);
};

const defaultState: AuthState = {};

export const reducer: Reducer<AuthState> = (
	state: AuthState | undefined = defaultState,
	incomingAction: Action
): AuthState => {
	const action = incomingAction as KnownAction;
	switch (action.type) {
		case "SIGN_UP_SUCCESS":
			return action.data;
		case "SIGN_OUT":
			return defaultState;
		default:
			return state;
	}
};
