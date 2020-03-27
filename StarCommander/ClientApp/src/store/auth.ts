import * as R from "ramda";
import { Action, Reducer } from "redux";
import { all, takeEvery, takeLeading } from "redux-saga/effects";
import { signIn, signUp, updateName } from "client/player";
import { commandSaga, querySaga, forwardSaga } from "store/saga/templates";

export interface Player {
	callSign?: string;
	firstName?: string;
	lastName?: string;
}

export interface AuthState {
	message?: string;
	player?: Player;
	token?: string;
}

export interface OnPlayerNameChanged {
	type: "ON_PLAYER_NAME_CHANGED";
	payload: {
		firstName: string;
		lastName: string;
	};
}

export interface SignIn {
	type: "SIGN_IN";
	payload: {
		callSign: string;
		password: string;
	};
}

export interface SignInSuccess {
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

export interface SignInFailure {
	type: "SIGN_IN_FAILURE";
	payload: {
		callSign: string;
		password: string;
	};
	error: string;
}

export interface SignOut {
	type: "SIGN_OUT";
}

export interface SignUp {
	type: "SIGN_UP";
	payload: {
		callSign: string;
		firstName: string;
		lastName: string;
	};
}

export interface SignUpFailure {
	type: "SIGN_UP_FAILURE";
	payload: {
		callSign: string;
		firstName: string;
		lastName: string;
	};
	error: string;
}

export interface SignUpSuccess {
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

export interface UpdateName {
	type: "UPDATE_NAME";
	payload: {
		firstName: string;
		lastName: string;
	};
}

export interface UpdateNameFailure {
	type: "UPDATE_NAME_FAILURE";
	payload: {
		firstName: string;
		lastName: string;
	};
	error: string;
}

export interface UpdateNameSuccess {
	type: "UPDATE_NAME_SUCCESS";
	payload: {
		firstName: string;
		lastName: string;
	};
}

export type KnownAction =
	| OnPlayerNameChanged
	| SignIn
	| SignInFailure
	| SignInSuccess
	| SignOut
	| SignUp
	| SignUpFailure
	| SignUpSuccess
	| UpdateName
	| UpdateNameFailure
	| UpdateNameSuccess;

export const actionCreators = {
	signIn: (callSign: string, password: string) =>
		({ type: "SIGN_IN", payload: { callSign, password } } as SignIn),
	signOut: () => ({ type: "SIGN_OUT" }),
	signUp: (
		callSign: string,
		firstName: string,
		lastName: string,
		password: string
	) =>
		({
			type: "SIGN_UP",
			payload: { callSign, firstName, lastName, password },
		} as SignUp),
	updateName: (firstName: string, lastName: string) =>
		({
			type: "UPDATE_NAME",
			payload: { firstName, lastName },
		} as UpdateName),
};

export const rootSaga = function* root() {
	yield all([
		yield takeLeading("SIGN_IN", querySaga, signIn),
		yield takeLeading("SIGN_UP", querySaga, signUp),
		yield takeLeading("UPDATE_NAME", commandSaga, updateName),
		yield takeEvery("SIGN_UP_SUCCESS", forwardSaga, "SIGN_IN_SUCCESS"),
	]);
};

const defaultState: AuthState = {};

export const reducer: Reducer<AuthState> = (
	state: AuthState = defaultState,
	incomingAction: Action
): AuthState => {
	const action = incomingAction as KnownAction;
	switch (action.type) {
		case "SIGN_IN_FAILURE":
		case "SIGN_UP_FAILURE":
			return { ...state, message: action.error };
		case "SIGN_IN_SUCCESS":
		case "SIGN_UP_SUCCESS":
			return action.data;
		case "SIGN_IN":
		case "SIGN_OUT":
		case "SIGN_UP":
			return defaultState;
		case "ON_PLAYER_NAME_CHANGED":
		case "UPDATE_NAME_SUCCESS": {
			const lens = R.lensPath(["player"]);
			return R.set(
				lens,
				R.merge(R.view(lens, state), action.payload),
				state
			);
		}

		default:
			return state;
	}
};
