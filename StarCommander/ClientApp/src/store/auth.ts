import { Action, Reducer } from "redux";
import { all, takeLeading } from "redux-saga/effects";
import { signIn, signUp } from "client/player";
import { querySaga } from "store/saga/templates";

export interface AuthState {
	player?: { callSign?: string; firstName?: string; lastName?: string };
	token?: string;
}

export interface SignInAction {
	type: "SIGN_IN";
	payload: {
		callSign: string;
		password: string;
	};
}

export interface SignInSuccessAction {
	type: "SIGN_IN_SUCCESS";
	payload: {
		callSign: string;
		password: string;
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

export type KnownAction =
	| SignInAction
	| SignInSuccessAction
	| SignOutAction
	| SignUpAction
	| SignUpSuccessAction;

export const actionCreators = {
	signIn: (callSign: string, password: string) =>
		({ type: "SIGN_IN", payload: { callSign, password } } as SignInAction),
	signOut: () => ({ type: "SIGN_OUT" }),
	signUp: (
		callSign: string,
		firstName: string,
		lastName: string,
		password: string
	) =>
		({
			type: "SIGN_UP",
			payload: { callSign, firstName, lastName, password }
		} as SignUpAction)
};

export const rootSaga = function* root() {
	yield all([
		yield takeLeading("SIGN_IN", querySaga, signIn),
		yield takeLeading("SIGN_UP", querySaga, signUp)
	]);
};

const defaultState: AuthState = {};

export const reducer: Reducer<AuthState> = (
	state: AuthState | undefined = defaultState,
	incomingAction: Action
): AuthState => {
	const action = incomingAction as KnownAction;
	switch (action.type) {
		case "SIGN_IN_SUCCESS":
		case "SIGN_UP_SUCCESS":
			return action.data;
		case "SIGN_OUT":
			return defaultState;
		default:
			return state;
	}
};
