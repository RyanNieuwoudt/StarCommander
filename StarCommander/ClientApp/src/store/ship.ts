import { Action, Reducer } from "redux";
import { all } from "redux-saga/effects";
import { SignIn, SignOut, SignUp } from "./auth";

export interface ShipState {
	shipId?: string;
}

export interface OnCaptainBoarded {
	type: "ON_CAPTAIN_BOARDED";
	payload: { shipId: string };
}


export const actionCreators = {};
export type KnownAction = OnCaptainBoarded | SignIn | SignOut | SignUp;

export const rootSaga = function* root() {};

const defaultState: ShipState = {};

export const reducer: Reducer<ShipState> = (
	state: ShipState | undefined = defaultState,
	incomingAction: Action
): ShipState => {
	const action = incomingAction as KnownAction;
	switch (action.type) {
		case "SIGN_IN":
		case "SIGN_OUT":
		case "SIGN_UP":
			return defaultState;
		case "ON_CAPTAIN_BOARDED":
			return action.payload;
		default:
			return state;
	}
};
