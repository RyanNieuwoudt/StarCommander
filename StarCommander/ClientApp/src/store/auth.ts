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

export interface SignInFailureAction {
	type: "SIGN_IN_FAILURE";
	payload: {
		callSign: string;
		password: string;
	};
	error: string;
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

export interface SignUpFailureAction {
	type: "SIGN_UP_FAILURE";
	payload: {
		callSign: string;
		firstName: string;
		lastName: string;
	};
	error: string;
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

export interface UpdateNameAction {
	type: "UPDATE_NAME";
	payload: {
		firstName: string;
		lastName: string;
	};
}

export interface UpdateNameFailureAction {
	type: "UPDATE_NAME_FAILURE";
	payload: {
		firstName: string;
		lastName: string;
	};
	error: string;
}

export interface UpdateNameSuccessAction {
	type: "UPDATE_NAME_SUCCESS";
	payload: {
		firstName: string;
		lastName: string;
	};
}

export type KnownAction =
	| SignInAction
	| SignInFailureAction
	| SignInSuccessAction
	| SignOutAction
	| SignUpAction
	| SignUpFailureAction
	| SignUpSuccessAction
	| UpdateNameAction
	| UpdateNameFailureAction
	| UpdateNameSuccessAction;

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
		} as SignUpAction),
	updateName: (firstName: string, lastName: string) =>
		({
			type: "UPDATE_NAME",
			payload: { firstName, lastName }
		} as UpdateNameAction)
};

export const rootSaga = function* root() {
	yield all([
		yield takeLeading("SIGN_IN", querySaga, signIn),
		yield takeLeading("SIGN_UP", querySaga, signUp),
		yield takeLeading("UPDATE_NAME", commandSaga, updateName),
		yield takeEvery("SIGN_UP_SUCCESS", forwardSaga, "SIGN_IN_SUCCESS")
	]);
};

const defaultState: AuthState = {};

export const reducer: Reducer<AuthState> = (
	state: AuthState | undefined = defaultState,
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
